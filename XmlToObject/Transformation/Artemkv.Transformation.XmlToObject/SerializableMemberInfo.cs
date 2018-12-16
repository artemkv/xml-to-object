#region Copyright
// XmlToObject
// Copyright (C) 2012  Artem Kondratyev

// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
#endregion Copyright

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Caching;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Provides the type info about the serializable type member (property or field).
	/// </summary>
	public class SerializableMemberInfo
	{
		#region Private Members

		private FieldInfo _fieldInfo;
		private PropertyInfo _propertyInfo;
		private ILookup<Type, object> _customAttributesByType;

		#endregion Private Members

		#region Constructors

		/// <summary>
		/// Initializes a new instance of <c>SerializableMemberInfo</c> class.
		/// </summary>
		/// <param name="fieldInfo">The field info.</param>
		public SerializableMemberInfo(FieldInfo fieldInfo)
		{
			if (fieldInfo == null)
				throw new ArgumentNullException("fieldInfo");

			_fieldInfo = fieldInfo;
			_customAttributesByType = _fieldInfo.GetCustomAttributes(inherit: true).ToLookup(x => x.GetType());
		}

		/// <summary>
		/// Initializes a new instance of <c>SerializableMemberInfo</c> class.
		/// </summary>
		/// <param name="propertyInfo">The property info.</param>
		public SerializableMemberInfo(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
				throw new ArgumentNullException("propertyInfo");

			_propertyInfo = propertyInfo;
			_customAttributesByType = _propertyInfo.GetCustomAttributes(inherit: true).ToLookup(x => x.GetType());
		}

		#endregion Constructors

		#region Public Properties

		/// <summary>
		/// Gets the member info.
		/// </summary>
		public MemberInfo MemberInfo
		{
			get
			{
				if (_propertyInfo != null)
				{
					return _propertyInfo;
				}
				return _fieldInfo;
			}
		}

		/// <summary>
		/// Gets the name of the member.
		/// </summary>
		public string Name
		{
			get
			{
				if (_propertyInfo != null)
				{
					return _propertyInfo.Name;
				}
				return _fieldInfo.Name;
			}
		}

		/// <summary>
		/// Gets the type of the member.
		/// </summary>
		public Type MemberType
		{
			get
			{
				if (_propertyInfo != null)
				{
					return _propertyInfo.PropertyType;
				}
				return _fieldInfo.FieldType;
			}
		}

		/// <summary>
		/// Gets the type of the member ('property' or 'field').
		/// </summary>
		public string MemeberTypeString
		{
			get
			{
				if (_propertyInfo != null)
				{
					return "property";
				}
				return "field";
			}
		}

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Returns a single custom attribute of the specified type applied to the member, 
		/// or null if the attribute is not found.
		/// </summary>
		/// <typeparam name="T">The type of the attribute to return.</typeparam>
		/// <returns>A single custom attribute of the specified type applied to the member.</returns>
		/// <exception cref="InvalidOperationException">The custom attribute of the specified type was applied more than once.</exception>
		public T GetSingleCustomAttribute<T>() where T : class
		{
			var customAttributes = _customAttributesByType[typeof(T)];
			if (customAttributes.Count() > 0)
			{
				if (customAttributes.Count() > 1)
				{
					throw new InvalidOperationException(
						String.Format("Attribute '{0}' is defined more than once for the {1} '{2}'", typeof(T).ToString(), MemeberTypeString, Name));
				}
				return customAttributes.First() as T;
			}
			return null;
		}

		/// <summary>
		/// Returns the mapping information for the member.
		/// </summary>
		/// <param name="ownerType">The type that owns the member.</param>
		/// <param name="parentMapping">When serialized as a part of the outer object, 
		/// holds the mapping defined on the outer object property.</param>
		/// <returns>The mapping information.</returns>
		public Mapping GetMapping(Type ownerType, Mapping parentMapping = null)
		{
			var mappingXPathAttribute = GetSingleCustomAttribute<MappingXPathAttribute>();
			var mappingXPathRelativeAttribute = GetSingleCustomAttribute<MappingXPathRelativeToParentAttribute>();

			string elementXPath = String.Empty;
			string attributeName = String.Empty;

			if (mappingXPathAttribute != null)
			{
				elementXPath = mappingXPathAttribute.ElementXPath;
				attributeName = mappingXPathAttribute.AttributeName;
			}
			else
			{
				elementXPath = "/" + ownerType.Name + "/" + Name;
			}

			// When serialized not independently, but as a member of the outer object, 
			// relative mapping taked precedence
			if (parentMapping != null)
			{
				if (mappingXPathRelativeAttribute != null)
				{
					elementXPath = parentMapping.ElementXPath + mappingXPathRelativeAttribute.ElementXPath;
					attributeName = mappingXPathRelativeAttribute.AttributeName;
				}
				else
				{
					// TODO: when validating xpath make sure xpaths always start with '/' and don't end with it
					elementXPath = parentMapping.ElementXPath.TrimEnd(new char[] { '/' }) + elementXPath;
				}
			}

			var asBase64Attribute = GetSingleCustomAttribute<SerializeAsBase64Attribute>();
			var formatAttribute = GetSingleCustomAttribute<SerializationFormatAttribute>();
			var arrayItemElementNameAttribute = GetSingleCustomAttribute<ArrayItemElementNameAttribute>();
			var xmlMandatoryAttribute = GetSingleCustomAttribute<XmlMandatoryAttribute>();
			var asXmlFragmentAttribute = GetSingleCustomAttribute<SerializeAsXmlFragmentAttribute>();

			return new Mapping(
				elementXPath, 
				attributeName,
				formatAttribute, 
				arrayItemElementNameAttribute,
				xmlMandatoryAttribute != null,
				asBase64Attribute != null,
				asXmlFragmentAttribute != null);
		}

		/// <summary>
		/// Sets the value of the member.
		/// </summary>
		/// <param name="obj">The object whose member value will be set.</param>
		/// <param name="value">The new value for this member.</param>
		public void SetValue(object obj, object value)
		{
			if (_propertyInfo != null)
			{
				_propertyInfo.SetValue(obj, value, null);
			}
			else
			{
				_fieldInfo.SetValue(obj, value);
			}
		}

		/// <summary>
		/// Returns the value of the member.
		/// </summary>
		/// <param name="obj">The object whose member value will be returned.</param>
		/// <returns>The member value for the obj parameter.</returns>
		public object GetValue(object obj)
		{
			if (_propertyInfo != null)
			{
				return _propertyInfo.GetValue(obj, null);
			}
			return _fieldInfo.GetValue(obj);
		}

		#endregion Public Methods
	}
}
