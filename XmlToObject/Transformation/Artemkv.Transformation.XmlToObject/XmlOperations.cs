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
using System.Xml;
using System.Runtime.Caching;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Provides helper methods for manipulating Xml.
	/// For internal use.
	/// </summary>
	public static class XmlOperations
	{
		private static MemoryCache Cache = new MemoryCache("MappingPaths");

		/// <summary>
		/// Returns existing or creates a new element with the specified path.
		/// </summary>
		/// <param name="document">Document to search or create an element.</param>
		/// <param name="path">The xpath of the element.</param>
		/// <param name="resolver">The namespace resolver.</param>
		/// <returns>Existing or new element with the specified path.</returns>
		public static XElement GetOrCreateElement(XDocument document, string path, IXmlNamespaceResolver resolver)
		{
			XElement parent = document.Root;
			string[] parts = CachedSplit(path, '/');
			string nextElemenPath = parts.First();

			if (string.IsNullOrEmpty(nextElemenPath))
			{
				return parent;
			}

			XElement nextElement;
			var nextElemenName = XPathHelper.GetElementName(nextElemenPath, resolver);
			if (parent == null)
			{
				parent = new XElement(nextElemenName, XPathHelper.GetAttributes(nextElemenPath, resolver));
				document.Add(parent);
			}
			else
			{
				if (parent.Name != nextElemenName)
				{
					throw new Exception(String.Format(
						"The root element ({0}) of the document does not match the root element of the mapping XPath {1}", parent.Name, nextElemenName));
				}
			}

			for (int i = 1; i < parts.Length; i++)
			{
				nextElemenPath = parts[i];
				nextElemenName = XPathHelper.GetElementName(nextElemenPath, resolver);

				nextElement = parent.Element(nextElemenName);
				if (nextElement == null)
				{
					nextElement = new XElement(nextElemenName, XPathHelper.GetAttributes(nextElemenPath, resolver));
					parent.Add(nextElement);
				}

				parent = nextElement;
			}

			return parent;
		}

		internal static string[] CachedSplit(string inputString, char separator)
		{
			var parts = (string[])Cache.Get(inputString);

			if (parts == null)
			{
				parts = Split((inputString.Trim('/')),separator);
				Cache.AddOrGetExisting(inputString, parts, DateTimeOffset.Now.AddMinutes(InternalConfiguration.CacheExpirationPeriodSeconds));
			}
			return parts;
		}

		private const char Quote = '\'';
		private const char DoubleQuote = '\'';
		private static string[] Split(string inputString, char separator)
		{
			var parts = new List<string>();
			bool quoted = false;
			bool doubleQuoted = false;
			int startIndex = 0;
			StringBuilder sb = new StringBuilder(inputString);
			for (int i = 0; i < sb.Length; i++)
			{
				if (sb[i] == Quote)
				{
					quoted = !quoted;
				}
				if (sb[i] == DoubleQuote)
				{
					doubleQuoted = !doubleQuoted;
				}

				if (sb[i] == separator && !quoted && !doubleQuoted)
				{
					parts.Add(sb.ToString(startIndex, i - startIndex));
					startIndex = i + 1;
				}
			}
			parts.Add(sb.ToString(startIndex, sb.Length - startIndex));
			return parts.ToArray();
		}

		internal static void PutValueConvertedToStringToXml(
			XDocument document,
			string value,
			Type deserializeAs,
			Mapping mapping,
			bool isArrayItem,
			bool emitTypeInfo,
			IXmlNamespaceResolver resolver)
		{
			if (value != null || mapping.IsXmlMandatory || isArrayItem)
			{
				var mappedElement = XmlOperations.GetOrCreateElement(document, mapping.ElementXPath, resolver);
				if (mappedElement == null)
				{
					throw new SerializationException(
						String.Format("Cannot serialize object. Could not find or create element with mapping XPath '{0}'.", mapping.ElementXPath));
				}
				if (value == null)
				{
					value = String.Empty;
				}
				if (String.IsNullOrWhiteSpace(mapping.AttributeName))
				{
					if (mapping.AsXml)
					{
						if (!String.IsNullOrWhiteSpace(value))
						{
							var wrappingElement = XElement.Parse("<root>" + value + "</root>");
							mappedElement.Add(wrappingElement.Elements());
						}
						if (emitTypeInfo)
						{
							if (deserializeAs != null)
							{
								if (mappedElement.Attribute(Constants.TypeInfoAttributeName) != null)
								{
									throw new SerializationException(
										String.Format("Cannot serialize object with type information. Mapping XPath '{0}' is used by more than 1 value.", mapping.ElementXPath));
								}
								mappedElement.Add(new XAttribute(Constants.TypeInfoAttributeName, deserializeAs.AssemblyQualifiedName.ToString()));
							}
							else
							{
								if (mappedElement.Attribute(Constants.NullValueAttributeName) != null)
								{
									throw new SerializationException(
										String.Format("Cannot serialize object with type information. Mapping XPath '{0}' is used by more than 1 value.", mapping.ElementXPath));
								}
								mappedElement.Add(new XAttribute(Constants.NullValueAttributeName, "true"));
							}
						}
					}
					else if (isArrayItem)
					{
						var arrayItemElement = new XElement(mapping.ArrayItemElementName, value);
						if (emitTypeInfo)
						{
							if (deserializeAs != null)
							{
								arrayItemElement.Add(new XAttribute(Constants.TypeInfoAttributeName, deserializeAs.AssemblyQualifiedName.ToString()));
							}
							else
							{
								arrayItemElement.Add(new XAttribute(Constants.NullValueAttributeName, "true"));
							}
						}
						mappedElement.Add(arrayItemElement);
					}
					else
					{
						mappedElement.Value = value;
						if (emitTypeInfo)
						{
							if (deserializeAs != null)
							{
								if (mappedElement.Attribute(Constants.TypeInfoAttributeName) != null)
								{
									throw new SerializationException(
										String.Format("Cannot serialize object with type information. Mapping XPath '{0}' is used by more than 1 value.", mapping.ElementXPath));
								}
								mappedElement.Add(new XAttribute(Constants.TypeInfoAttributeName, deserializeAs.AssemblyQualifiedName.ToString()));
							}
							else
							{
								if (mappedElement.Attribute(Constants.NullValueAttributeName) != null)
								{
									throw new SerializationException(
										String.Format("Cannot serialize object with type information. Mapping XPath '{0}' is used by more than 1 value.", mapping.ElementXPath));
								}
								mappedElement.Add(new XAttribute(Constants.NullValueAttributeName, "true"));
							}
						}
					}
				}
				else
				{
					mappedElement.SetAttributeValue(XPathHelper.ConvertToXName(mapping.AttributeName, resolver), value);
				}
			}
		}

		internal static string ExtractValueAsStringFromXml(Mapping mapping, XElement mappedElement, IXmlNamespaceResolver resolver)
		{
			string propertyValue = null;
			if (String.IsNullOrWhiteSpace(mapping.AttributeName))
			{
				if (mapping.AsXml)
				{
					var reader = mappedElement.CreateReader();
					reader.MoveToContent();
					propertyValue = reader.ReadInnerXml();
				}
				else
				{
					propertyValue = mappedElement.Value;
				}
			}
			else
			{
				var mappedAttribute = mappedElement.Attribute(XPathHelper.ConvertToXName(mapping.AttributeName, resolver));
				if (mappedAttribute != null)
				{
					propertyValue = mappedAttribute.Value;
				}
			}
			return propertyValue;
		}
	}
}
