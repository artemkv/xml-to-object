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
using System.Collections;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Reflection;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Converts List of T to Xml string and back.
	/// </summary>
	public class ListOfTValueConverter : ValueConverterBase
	{
		private class SerializableWrapper : IXPathSerializable
		{
			[MappingXPath("/A/B")]
			[XmlMandatory]
			public object SerializedValue;
		}

		/// <summary>
		/// Converts an Xml string value to a generic list.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="context">The serialization context.</param>
		/// <param name="convertedValue">The converted value.</param>
		/// <returns>True, if conversion was successful; otherwise, False.</returns>
		public override bool TryConvert(string value, SerializationContext context, out object convertedValue)
		{
			convertedValue = null;

			if (!context.DeserializeAs.IsGenericType)
			{
				return false;
			}

			var itemType = context.DeserializeAs.GetGenericArguments()[0];
			var arrayOfRequiredType = Array.CreateInstance(itemType, 0);

			string xml = String.Format(@"<A><B x:__type='{0}' xmlns:x='http://xmltoobject.codeplex.com'>{1}</B></A>", 
				arrayOfRequiredType.GetType().AssemblyQualifiedName, value);
			var wrapper = XmlSerialization.LoadFromXml<SerializableWrapper>(xml, version: context.Version, provider: context.FormatProvider);

			var convertToList =
				typeof(ListOfTValueConverter).GetMethod("ConvertToList", BindingFlags.Static | BindingFlags.NonPublic)
				.MakeGenericMethod(new Type[] { itemType });
			convertedValue = convertToList.Invoke(null, new object[] { wrapper.SerializedValue });

			return true;
		}

		/// <summary>
		/// Tries to perform the backward conversion of a generic list to Xml string.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="context">The serialization context.</param>
		/// <param name="convertedValue">The converted value.</param>
		/// <returns>True, if conversion was successful; otherwise, False.</returns>
		public override bool TryConvertBack(object value, SerializationContext context, out string convertedValue)
		{
			convertedValue = null;

			if (value == null)
			{
				return true;
			}

			var toArray = value.GetType().GetMethod("ToArray");
			if (toArray == null)
			{
				return false;
			}

			try
			{
				var array = toArray.Invoke(value, null);

				var wrapper = new SerializableWrapper()
				{
					SerializedValue = array
				};
				var arrayXml = wrapper.ToXmlDocument(emitTypeInfo: context.EmitTypeInfo, version: context.Version, provider: context.FormatProvider);

				var reader = arrayXml.XPathSelectElement("/A/B").CreateReader();
				reader.MoveToContent();

				convertedValue = reader.ReadInnerXml();
				return true;
			}
			catch(SerializationException)
			{
				return false;
			}
		}

		private static List<T> ConvertToList<T>(object obj)
		{
			return ((T[])obj).ToList<T>();
		}
	}
}
