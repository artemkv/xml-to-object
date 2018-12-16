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
using System.Xml.Linq;
using System.Xml.XPath;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using System.Globalization;
using System.Xml;
using System.Diagnostics;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Provides the extension methods for object serialization.
	/// </summary>
	public static class XmlSerialization
	{
		#region Public Methods

		/// <summary>
		/// Serializes an object to xml.
		/// </summary>
		/// <param name="serializable">The object to serialize.</param>
		/// <param name="emitTypeInfo">Specifies whether the type info should be emitted when serializing the object.</param>
		/// <param name="version">The version of the object that should be used when serializing the object.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The xml string with the serialized object.</returns>
		/// <exception cref="SerializationException">Is thrown if the serialization cannot be performed.</exception>
		public static string ToXml(
			this IXPathSerializable serializable,
			bool emitTypeInfo = false,
			int version = 0,
			IFormatProvider provider = null)
		{
			return ToXmlInternal(
				new XDocument(),
				serializable, 
				objectsAlreadySerialized: new HashSet<object>(),
 				useMembers: null,
				parentMapping: null,
				emitTypeInfo: emitTypeInfo, 
				version: version,
				provider: provider,
				resolver: null).ToString();
		}

		/// <summary>
		/// Serializes an object to xml.
		/// </summary>
		/// <param name="serializable">The object to serialize.</param>
		/// <param name="filter">The member filter.</param>
		/// <param name="emitTypeInfo">Specifies whether the type info should be emitted when serializing the object.</param>
		/// <param name="version">The version of the object that should be used when serializing the object.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The xml string with the serialized object.</returns>
		/// <exception cref="SerializationException">Is thrown if the serialization cannot be performed.</exception>
		public static string ToXml<TFilter>(
			this IXPathSerializable serializable,
			MemberFilter<TFilter> filter = null,
			bool emitTypeInfo = false,
			int version = 0,
			IFormatProvider provider = null)
		{
			var useMembers = GetFilterMembers<TFilter>(serializable, filter);
			return ToXmlInternal(
				new XDocument(),
				serializable, 
				objectsAlreadySerialized: new HashSet<object>(),
				useMembers: useMembers, 
				parentMapping: null,
				emitTypeInfo: emitTypeInfo,
				version: version,
				provider: provider,
				resolver: null).ToString();
		}

		/// <summary>
		/// Serializes an object to xml.
		/// </summary>
		/// <param name="serializable">The object to serialize.</param>
		/// <param name="emitTypeInfo">Specifies whether the type info should be emitted when serializing the object.</param>
		/// <param name="version">The version of the object that should be used when serializing the object.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The xml document with the serialized object.</returns>
		/// <exception cref="SerializationException">Is thrown if the serialization cannot be performed.</exception>
		public static XDocument ToXmlDocument(
			this IXPathSerializable serializable,
			bool emitTypeInfo = false,
			int version = 0,
			IFormatProvider provider = null)
		{
			return ToXmlInternal(
				new XDocument(),
				serializable,
				useMembers: null,
				parentMapping: null,
				objectsAlreadySerialized: new HashSet<object>(),
				emitTypeInfo: emitTypeInfo,
				version: version,
				provider: provider,
				resolver: null);
		}

		/// <summary>
		/// Serializes an object to xml.
		/// </summary>
		/// <param name="serializable">The object to serialize.</param>
		/// <param name="filter">The member filter.</param>
		/// <param name="emitTypeInfo">Specifies whether the type info should be emitted when serializing the object.</param>
		/// <param name="version">The version of the object that should be used when serializing the object.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The xml document with the serialized object.</returns>
		/// <exception cref="SerializationException">Is thrown if the serialization cannot be performed.</exception>
		public static XDocument ToXmlDocument<TFilter>(
			this IXPathSerializable serializable,
			MemberFilter<TFilter> filter = null,
			bool emitTypeInfo = false,
			int version = 0,
			IFormatProvider provider = null)
		{
			var useMembers = GetFilterMembers<TFilter>(serializable, filter);
			return ToXmlInternal(
				new XDocument(),
				serializable,
				objectsAlreadySerialized: new HashSet<object>(),
				useMembers: useMembers,
				parentMapping: null,
				emitTypeInfo: emitTypeInfo,
				version: version,
				provider: provider,
				resolver: null);
		}

		/// <summary>
		/// Loads an object from xml string.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize.</typeparam>
		/// <param name="xmlString">The xml string with the serialized object.</param>
		/// <param name="version">The version of the object that should be used when deserializing the object.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The deserialized object.</returns>
		/// <exception cref="ArgumentNullException">Is thrown if the xmlString parameter is null.</exception>
		/// <exception cref="SerializationException">Is thrown if the serialization cannot be performed.</exception>
		public static T LoadFromXml<T>(
			string xmlString,
			int version = 0,
			IFormatProvider provider = null)
			where T : IXPathSerializable, new()
		{
			if (String.IsNullOrWhiteSpace(xmlString))
				throw new ArgumentNullException("xmlString");

			IXPathSerializable serializable = new T();

			try
			{
				var document = XDocument.Parse(xmlString);
				LoadFromXmlInternal(
					document, 
					serializable, 
					useMembers: null, 
					parentMapping: null, 
					version: version, 
					provider: provider,
					resolver: null);

				return (T)serializable;
			}
			catch (XmlException ex)
			{
				throw new SerializationException(
					String.Format("Cannot deserialize object of type '{0}' from string '{1}'. Please refer to inner exception for details",
					serializable.GetType(),
					xmlString), ex);
			}
		}

		/// <summary>
		/// Loads an object from xml document.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize.</typeparam>
		/// <param name="document">The xml document with the serialized object.</param>
		/// <param name="version">The version of the object that should be used when deserializing the object.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The deserialized object.</returns>
		/// <exception cref="ArgumentNullException">Is thrown if the document parameter is null.</exception>
		/// <exception cref="SerializationException">Is thrown if the serialization cannot be performed.</exception>
		public static T LoadFromXmlDocument<T>(
			XDocument document,
			int version = 0,
			IFormatProvider provider = null)
			where T : IXPathSerializable, new()
		{
			if (document == null)
				throw new ArgumentNullException("document");

			IXPathSerializable serializable = new T();

			LoadFromXmlInternal(
				document, 
				serializable,
				useMembers: null,
				parentMapping: null,
				version: version,
				provider: provider,
				resolver: null);

			return (T)serializable;
		}

		/// <summary>
		/// Loads an object from xml string.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize.</typeparam>
		/// <typeparam name="TFilter">The type of the object that is used as a member filter.</typeparam>
		/// <param name="xmlString">The xml string with the serialized object.</param>
		/// <param name="filter">The member filter.</param>
		/// <param name="version">The version of the object that should be used when deserializing the object.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The deserialized object.</returns>
		/// <exception cref="ArgumentNullException">Is thrown if the xmlString parameter is null.</exception>
		/// <exception cref="SerializationException">Is thrown if the serialization cannot be performed.</exception>
		public static T LoadFromXml<T, TFilter>(
			string xmlString,
			MemberFilter<TFilter> filter = null,
			int version = 0,
			IFormatProvider provider = null)
			where T : IXPathSerializable, new()
		{
			if (String.IsNullOrWhiteSpace(xmlString))
				throw new ArgumentNullException("xmlString");

			IXPathSerializable serializable = new T();

			try
			{
				var document = XDocument.Parse(xmlString);
				var useMembers = GetFilterMembers<TFilter>(serializable, filter);
				LoadFromXmlInternal(
					document, 
					serializable, 
					useMembers, 
					parentMapping: null,
					version: version,
					provider: provider,
					resolver: null);

				return (T)serializable;
			}
			catch (XmlException ex)
			{
				throw new SerializationException(
					String.Format("Cannot deserialize object of type '{0}' from string '{1}'. Please refer to inner exception for details",
					serializable.GetType(),
					xmlString), ex);
			}
		}

		/// <summary>
		/// Loads an object from xml document.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize.</typeparam>
		/// <typeparam name="TFilter">The type of the object that is used as a member filter.</typeparam>
		/// <param name="document">The xml document with the serialized object.</param>
		/// <param name="filter">The member filter.</param>
		/// <param name="version">The version of the object that should be used when deserializing the object.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The deserialized object.</returns>
		/// <exception cref="ArgumentNullException">Is thrown if the document parameter is null.</exception>
		/// <exception cref="SerializationException">Is thrown if the serialization cannot be performed.</exception>
		public static T LoadFromXmlDocument<T, TFilter>(
			XDocument document,
			MemberFilter<TFilter> filter = null,
			int version = 0,
			IFormatProvider provider = null)
			where T : IXPathSerializable, new()
		{
			if (document == null)
				throw new ArgumentNullException("document");

			IXPathSerializable serializable = new T();

			var useMembers = GetFilterMembers<TFilter>(serializable, filter);
			LoadFromXmlInternal(
				document, 
				serializable, 
				useMembers,
				parentMapping: null,
				version: version,
				provider: provider,
				resolver: null);

			return (T)serializable;
		}

		#endregion Public Methods

		#region Private Methods

		#region ToXml

		private static XDocument ToXmlInternal(
			XDocument document,
			IXPathSerializable serializable,
			HashSet<object> objectsAlreadySerialized,
			MemberInfo[] useMembers,
			Mapping parentMapping,
			bool emitTypeInfo,
			int version,
			IFormatProvider provider,
			XmlNamespaceManager resolver)
		{
			// Take care of circular references
			if (objectsAlreadySerialized.Contains(serializable))
			{
				throw new SerializationException(
					String.Format("A circular reference was detected while serializing an object of type '{0}'", serializable.GetType()));
			}
			else
			{
				objectsAlreadySerialized.Add(serializable);
			}

			if (document == null)
			{
				document = new XDocument();
			}

			// Initialize namespace resolver from class attributes
			if (resolver == null)
			{
				resolver = new XmlNamespaceManager(new NameTable());
			}
			var namespaceAttributes = serializable.GetType().GetCustomAttributes(typeof(NamespacePrefixAttribute), true);
			foreach (NamespacePrefixAttribute namespaceAttribute in namespaceAttributes)
			{
				resolver.AddNamespace(namespaceAttribute.Prefix, namespaceAttribute.Uri);
			}

			// Ensure minimal xml structure
			var minimalXmlAttributes = serializable.GetType().GetCustomAttributes(typeof(MinimalXmlStructureAttribute), true);
			if (minimalXmlAttributes != null)
			{
				foreach (MinimalXmlStructureAttribute minimalXmlAttribute in minimalXmlAttributes)
				{
					string xPath = minimalXmlAttribute.ElementXPath;
					if (String.IsNullOrWhiteSpace(xPath))
					{
						xPath = XmlConvert.EncodeName(serializable.GetType().Name);
					}
					XmlOperations.GetOrCreateElement(document, xPath, resolver);
				}
			}

			var typeInfo = SerializableTypeInfo.GetTypeInfo(serializable.GetType());

			var membersToSerialize = typeInfo.Members.Where(x => useMembers == null || useMembers.Length == 0 || useMembers.Contains(x.MemberInfo));
			SerializeMembers(
				document, 
				serializable, 
				objectsAlreadySerialized,
				membersToSerialize.ToArray(),
				useMembers,
				parentMapping,
				emitTypeInfo, 
				version, 
				provider, 
				resolver);

			// Return resulting document
			return document;
		}

		private static void SerializeMembers(
			XDocument document,
			IXPathSerializable serializable,
			HashSet<object> objectsAlreadySerialized,
			SerializableMemberInfo[] membersToSerialize,
			MemberInfo[] useMembers = null,
			Mapping parentMapping = null,
			bool emitTypeInfo = false,
			int version = 0,
			IFormatProvider provider = null,
			XmlNamespaceManager resolver = null)
		{
			foreach (var member in membersToSerialize)
			{
				// Skip non-serializable members
				if (member.GetSingleCustomAttribute<NotForSerializationAttribute>() != null)
				{
					continue;
				}

				// Check if the member is serializable in the current version
				int serializedFromVersion = 0;
				var versionAttribute = member.GetSingleCustomAttribute<SerializedFromVersionAttribute>();
				if (versionAttribute != null)
				{
					serializedFromVersion = versionAttribute.Version;
				}
				if (version < serializedFromVersion)
				{
					continue;
				}

				var mapping = member.GetMapping(serializable.GetType(), parentMapping);
				
				object value = member.GetValue(serializable);

				// Remember the type in case we need to emit it into the Xml
				Type deserializeAs = null;
				if (value != null && emitTypeInfo)
				{
					deserializeAs = value.GetType();
				}

				var context = new SerializationContext(serializable, member, mapping, useMembers, deserializeAs, emitTypeInfo, version, provider, resolver);

				// TODO: what if mandatory?
				if (value != null && value.GetType().IsArray && !mapping.AsBase64)
				{
					SerializeArrayMember(document, value, context, objectsAlreadySerialized);
				}
				else
				{
					if (value != null && typeof(IXPathSerializable).IsAssignableFrom(value.GetType()))
					{
						SerializeXPathSerializableMember(document, value, context, objectsAlreadySerialized);
					}
					else
					{
						SerializeSimpleMember(document, value, context);
					}
				}
			}
		}

		private static void SerializeArrayMember(
			XDocument document, 
			object array, 
			SerializationContext context,
			HashSet<object> objectsAlreadySerialized)
		{
			Debug.Assert(array != null);

			var mappedElement = XmlOperations.GetOrCreateElement(document, context.Mapping.ElementXPath, context.NamespaceResolver);
			
			// Emit type info if necessary
			if (context.EmitTypeInfo)
			{
				if (mappedElement.Attribute(Constants.TypeInfoAttributeName) != null)
				{
					throw new SerializationException(
						String.Format(
							"Cannot serialize object with type information. Mapping XPath '{0}' is used by more than 1 value.", 
							context.Mapping.ElementXPath));
				}
				mappedElement.Add(new XAttribute(Constants.TypeInfoAttributeName, context.DeserializeAs.AssemblyQualifiedName.ToString()));
			}

			foreach (var item in (IEnumerable)array)
			{
				var serializableItem = item as IXPathSerializable;
				if (serializableItem != null)
				{
					var objectElement = ToXmlInternal(
						new XDocument(),
						serializableItem, 							
						objectsAlreadySerialized,
						useMembers: null,
						parentMapping: null,
						emitTypeInfo: context.EmitTypeInfo,
						version: context.Version,
						provider: context.FormatProvider,
						resolver: context.NamespaceResolver).Root; // TODO: pass along every argument from outer ToXml
					if (context.EmitTypeInfo)
					{
						objectElement.Add(new XAttribute(Constants.TypeInfoAttributeName, serializableItem.GetType().AssemblyQualifiedName.ToString()));
					}
					mappedElement.Add(objectElement);
				}
				else
				{
					string valueString = XmlMemberSerialization.GetMemberXmlValue(item, context);
					Type deserializeItemAs = null;
					if (item != null && context.EmitTypeInfo)
					{
						deserializeItemAs = item.GetType();
					}
					XmlOperations.PutValueConvertedToStringToXml(
						document, 
						valueString, 
						deserializeItemAs, 
						context.Mapping, 
						true, 
						context.EmitTypeInfo,
						context.NamespaceResolver);
				}
			}
		}

		private static void SerializeXPathSerializableMember(
			XDocument document, 
			object value, 
			SerializationContext context, 
			HashSet<object> objectsAlreadySerialized)
		{
			Debug.Assert(value != null);

			var mappedElement = XmlOperations.GetOrCreateElement(document, context.Mapping.ElementXPath, context.NamespaceResolver);

			// Emit type info if necessary
			if (context.EmitTypeInfo)
			{
				if (mappedElement.Attribute(Constants.TypeInfoAttributeName) != null)
				{
					throw new SerializationException(
						String.Format(
							"Cannot serialize object with type information. Mapping XPath '{0}' is used by more than 1 value.", 
							context.Mapping.ElementXPath));
				}
				mappedElement.Add(new XAttribute(Constants.TypeInfoAttributeName, context.DeserializeAs.AssemblyQualifiedName.ToString()));
			}

			var innnerObjectXml = ToXmlInternal(
				document,
				((IXPathSerializable)value),
				objectsAlreadySerialized,
				useMembers: context.UseMembers,
				parentMapping: context.Mapping,
				emitTypeInfo: context.EmitTypeInfo,
				version: context.Version,
				provider: context.FormatProvider,
				resolver: context.NamespaceResolver); // TODO: property filter - will not work (and btw now not even passed)
		}

		private static void SerializeSimpleMember(
			XDocument document, 
			object value, 
			SerializationContext context)
		{
			string valueString = XmlMemberSerialization.GetMemberXmlValue(value, context);
			XmlOperations.PutValueConvertedToStringToXml(
				document, 
				valueString, 
				context.DeserializeAs, 
				context.Mapping, 
				false, 
				context.EmitTypeInfo,
				context.NamespaceResolver);
		}

		#endregion ToXml

		#region FromXml

		private static IXPathSerializable LoadFromXmlInternal(
			XDocument document,
			IXPathSerializable serializable,
			MemberInfo[] useMembers,
			Mapping parentMapping,
			int version,
			IFormatProvider provider,
			XmlNamespaceManager resolver)
		{
			// Initialize namespace resolver from class attributes
			if (resolver == null)
			{
				resolver = new XmlNamespaceManager(new NameTable());
			}
			var namespaceAttributes = serializable.GetType().GetCustomAttributes(typeof(NamespacePrefixAttribute), true);
			foreach (NamespacePrefixAttribute namespaceAttribute in namespaceAttributes)
			{
				resolver.AddNamespace(namespaceAttribute.Prefix, namespaceAttribute.Uri);
			}

			var typeInfo = SerializableTypeInfo.GetTypeInfo(serializable.GetType());

			var membersToLoad = typeInfo.Members.Where(x => useMembers == null || useMembers.Length == 0 || useMembers.Contains(x.MemberInfo));
			LoadMembersFromXml(
				document, 
				serializable, 
				membersToLoad.ToArray(), 
				useMembers,
				parentMapping, 
				version, 
				provider, 
				resolver);

			return serializable;
		}

		private static void LoadMembersFromXml(
			XDocument document,
			IXPathSerializable serializable,
			SerializableMemberInfo[] membersToLoad,
			MemberInfo[] useMembers,
			Mapping parentMapping,
			int version,
			IFormatProvider provider,
			XmlNamespaceManager resolver)
		{
			foreach (var member in membersToLoad)
			{
				// Skip non-serializable members
				if (member.GetSingleCustomAttribute<NotForSerializationAttribute>() != null)
				{
					continue;
				}

				// Check if the member is serializable in the current version
				int serializedFromVersion = 0;
				var versionAttribute = member.GetSingleCustomAttribute<SerializedFromVersionAttribute>();
				if (versionAttribute != null)
				{
					serializedFromVersion = versionAttribute.Version;
				}
				if (version < serializedFromVersion)
				{
					continue;
				}

				var mapping = member.GetMapping(serializable.GetType(), parentMapping);

				var mappedElement = document.Root.XPathSelectElement(mapping.ElementXPath, resolver);

				bool mappedToAttributeWhichIsNotFound = 
					!String.IsNullOrWhiteSpace(mapping.AttributeName) &&
					(mappedElement == null || mappedElement.Attribute(XPathHelper.ConvertToXName(mapping.AttributeName, resolver)) == null);

				// Ensure mandatory xml elements/attributes are present
				if (mapping.IsXmlMandatory)
				{
					if (mappedElement == null)
					{
						throw new SerializationException(
							String.Format(
								"Cannot deserialize {0} '{1}', type '{2}'. Could not find the element with path '{3}'",
								member.MemeberTypeString,
								member.Name,
								serializable.GetType(),
								mapping.ElementXPath));
					} 
					else if (mappedToAttributeWhichIsNotFound)
					{
						throw new SerializationException(
							String.Format(
								"Cannot deserialize {0} '{1}', type '{2}'. Could not find the attribute '{3}' of the element with path '{4}'",
								member.MemeberTypeString,
								member.Name,
								serializable.GetType(),
								mapping.AttributeName,
								mapping.ElementXPath));
					}
				}

				// Short-circuit for nulls
				if (mappedElement == null || mappedToAttributeWhichIsNotFound)
				{
					member.SetValue(serializable, null);
					continue;
				}
				if (string.IsNullOrWhiteSpace(mapping.AttributeName))
				{
					var isNullAttribute = mappedElement.Attribute(Constants.NullValueAttributeName);
					if (isNullAttribute != null)
					{
						member.SetValue(serializable, null);
						continue;
					}
				}

				// Determine the type of the deserialized value
				Type emittedType = null;
				if (mappedElement != null && string.IsNullOrWhiteSpace(mapping.AttributeName))
				{
					var typeAttribute = mappedElement.Attribute(Constants.TypeInfoAttributeName);
					if (typeAttribute != null)
					{
						emittedType = Type.GetType(typeAttribute.Value); // TODO: handle all kind of exceptions
					}
				}
				Type deserializeAs;
				if (emittedType != null)
				{
					deserializeAs = emittedType;
				}
				else
				{
					deserializeAs = member.MemberType;
				}

				var context = new SerializationContext(
					serializable, 
					member, 
					mapping,
 					useMembers,
					deserializeAs, 
					version: version, 
					formatProvider: provider, 
					resolver: resolver);

				if (deserializeAs.IsArray && !mapping.AsBase64)
				{
					LoadArrayMemberFromXml(mappedElement, context);
				}
				else if (typeof(IXPathSerializable).IsAssignableFrom(deserializeAs))
				{
					LoadXPathSerializableMemberFromXml(document, context);
				}
				else
				{
					LoadSimpleMemberFromXml(mappedElement, context);
				}
			}
		}

		private static void LoadXPathSerializableMemberFromXml(XDocument document, SerializationContext context)
		{
			object value = null;
			if (document != null)
			{
				MethodInfo LoadObjectFromXmlDocument = null;
				try
				{
					LoadObjectFromXmlDocument =
						typeof(XmlSerialization).GetMethod("LoadObjectFromXmlDocument", BindingFlags.Static | BindingFlags.NonPublic)
						.MakeGenericMethod(new Type[] { context.DeserializeAs });
				}
				catch (ArgumentException)
				{
					throw new SerializationException(
						String.Format(
							"Cannot deserialize {0} '{1}', type '{2}'. Type '{3}' does not have a default constructor.",
							context.Member.MemeberTypeString,
							context.Member.Name,
							context.Serializable.GetType(),
							context.DeserializeAs));
				}
				try
				{
					value = LoadObjectFromXmlDocument.Invoke(
						null, 
						new object[] 
						{ 
							document, 
							context.UseMembers, 
							context.Mapping, 
							context.Version, 
							context.FormatProvider,
							context.NamespaceResolver
						});
				}
				catch (TargetInvocationException ex)
				{
					throw new SerializationException(
						String.Format(
							"Cannot deserialize {0} '{1}', type '{2}'. Please refer to the inner exception for details.",
							context.Member.MemeberTypeString,
							context.Member.Name,
							context.Serializable.GetType()), ex);
				}
			}
			context.Member.SetValue(context.Serializable, value);
		}

		private static void LoadArrayMemberFromXml(XElement mappedElement, SerializationContext context)
		{
			Array value = null;
			if (mappedElement != null)
			{
				XElement[] elements = new XElement[0];
				var arrayItemElements = mappedElement.Elements();
				if (arrayItemElements != null)
				{
					elements = arrayItemElements.ToArray();
				}

				var arrayElementType = context.DeserializeAs.GetElementType();
				value = Array.CreateInstance(arrayElementType, elements.Length);
				for (int i = 0; i < elements.Length; i++)
				{
					Type elementType = null;

					// Short-circuit for nulls
					var isNullAttribute = elements[i].Attribute(Constants.NullValueAttributeName);
					if (isNullAttribute != null)
					{
						value.SetValue(null, i);
						continue;
					}

					var typeAttribute = elements[i].Attribute(Constants.TypeInfoAttributeName);
					if (typeAttribute != null)
					{
						elementType = Type.GetType(typeAttribute.Value); // TODO: handle all kind of exceptions
					}
					else
					{
						elementType = arrayElementType;
					}

					object convertedValue = null;
					if (typeof(IXPathSerializable).IsAssignableFrom(elementType))
					{
						MethodInfo loadObjectFromXml = null;
						try
						{
							loadObjectFromXml = typeof(XmlSerialization).GetMethod("LoadObjectFromXml", BindingFlags.Static | BindingFlags.NonPublic)
								.MakeGenericMethod(new Type[] { elementType });
						}
						catch (ArgumentException)
						{
							throw new SerializationException(
								String.Format(
									"Cannot deserialize {0} '{1}', type '{2}'. Type '{3}' does not have a default constructor.",
									context.Member.MemeberTypeString,
									context.Member.Name,
									context.Serializable.GetType(),
									elementType));
						}
						try
						{
							var itemDoc = new XDocument(elements[i]);
							convertedValue = loadObjectFromXml.Invoke(
								null, 
								new object[] 
								{ 
									itemDoc, 
									context.Version, 
									context.FormatProvider,
									context.NamespaceResolver
								});
						}
						catch (TargetInvocationException ex)
						{
							throw new SerializationException(
								String.Format(
									"Cannot deserialize {0} '{1}', type '{2}'. Please refer to the inner exception for details.",
									context.Member.MemeberTypeString,
									context.Member.Name,
									context.Serializable.GetType()), ex);
						}
					}
					else
					{
						convertedValue = XmlMemberSerialization.GetMemberActualValue(elementType, elements[i].Value, context);
					}
					value.SetValue(convertedValue, i);
				}
			}
			context.Member.SetValue(context.Serializable, value);
		}

		private static void LoadSimpleMemberFromXml(XElement mappedElement, SerializationContext context)
		{
			string value = null;
			if (mappedElement != null)
			{
				value = XmlOperations.ExtractValueAsStringFromXml(context.Mapping, mappedElement, context.NamespaceResolver);
			}

			var convertedValue = XmlMemberSerialization.GetMemberActualValue(context.DeserializeAs, value, context);
			context.Member.SetValue(context.Serializable, convertedValue);
		}

		#endregion FromXml

		#region Various Helpers

		private static MemberInfo[] GetFilterMembers<TFilter>(IXPathSerializable serializable, MemberFilter<TFilter> filter)
		{
			var members = new List<MemberInfo>();
			if (filter == null)
			{
				return members.ToArray();
			}

			foreach (var expression in filter)
			{
				MemberExpression propertyExpression;

				var unary = expression.Body as UnaryExpression;
				if (unary != null)
				{
					// In this case the return type of the property was not object,
					// so .Net wrapped the expression inside of a unary Convert() expression that casts it to type object. 
					// In this case, the Operand of the Convert expression has the original expression.
					propertyExpression = unary.Operand as MemberExpression;
				}
				else
				{
					propertyExpression = expression.Body as MemberExpression;
				}

				if (propertyExpression != null)
				{
					var propertyName = propertyExpression.Member.Name;

					var field = propertyExpression.Member.DeclaringType.GetField(propertyName);
					var property = propertyExpression.Member.DeclaringType.GetProperty(propertyName);

					if (field != null)
					{
						members.Add(field);
					}
					if (property != null)
					{
						members.Add(property);
					}
				}
			}
			return members.ToArray();
		}

		private static object LoadObjectFromXml<T>(XDocument document, int version, IFormatProvider provider, XmlNamespaceManager resolver)
			where T : IXPathSerializable, new()
		{
			var obj = new T();
			obj = (T)LoadFromXmlInternal(document, obj, null, null, version, provider, resolver);
			return obj;
		}

		private static object LoadObjectFromXmlDocument<T>(
			XDocument document, MemberInfo[] useMembers, Mapping parentMapping, int version, IFormatProvider provider, XmlNamespaceManager resolver)
			where T : IXPathSerializable, new()
		{
			var obj = new T();
			obj = (T)LoadFromXmlInternal(document, obj, useMembers, parentMapping, version, provider, resolver);
			return obj;
		}

		#endregion Helpers

		#endregion Private Methods
	}
}
