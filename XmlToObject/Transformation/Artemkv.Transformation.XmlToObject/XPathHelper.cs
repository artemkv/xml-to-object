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
using System.Xml;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Contains method for xpath string parsing.
	/// </summary>
	internal static class XPathHelper
	{
		public static XName GetElementName(string nextElemenPath, IXmlNamespaceResolver resolver)
		{
			if (String.IsNullOrWhiteSpace(nextElemenPath))
			{
				return String.Empty;
			}

			int indexOfBraces = nextElemenPath.IndexOf("[");
			if (indexOfBraces > 0)
			{
				nextElemenPath = nextElemenPath.Substring(0, indexOfBraces);
			}
			nextElemenPath = nextElemenPath.Replace(Environment.NewLine, String.Empty).Trim();

			return ConvertToXName(nextElemenPath, resolver);
		}

		public static List<XAttribute> GetAttributes(string nextElemenPath, IXmlNamespaceResolver resolver)
		{
			var attributes = new List<XAttribute>();
			if (String.IsNullOrWhiteSpace(nextElemenPath))
			{
				return attributes;
			}

			int indexOfBraces = nextElemenPath.IndexOf("[");
			if (indexOfBraces > 0)
			{
				var attributesString = nextElemenPath.Substring(indexOfBraces, nextElemenPath.Length - indexOfBraces);
				string[] parts = attributesString.Replace("@", "")
					.Replace(Environment.NewLine, String.Empty)
					.Trim()
					.Trim(new char[] { '[', ']' })
					.Split(new string[] { "and" }, StringSplitOptions.None);

				foreach (var part in parts)
				{
					var attributeParts = part.Split('=');
					if (attributeParts.Length > 0)
					{
						XName attributeName = ConvertToXName(attributeParts[0].Trim(), resolver);

						string attributeValue = String.Empty;
						if (attributeParts.Length > 1)
						{
							attributeValue = attributeParts[1].Trim(new char[] { ' ', '\'', '\"' });
						}

						var attribute = new XAttribute(attributeName, attributeValue);
						attributes.Add(attribute);
					}
				}
			}

			return attributes;
		}

		public static XName ConvertToXName(string name, IXmlNamespaceResolver resolver)
		{
			XName xname = null;
			if (name.Contains(":"))
			{
				var parts = name.Split(new char[] { ':' }, StringSplitOptions.None);
				XNamespace ns = resolver.LookupNamespace(parts[0]);

				if (ns == null)
				{
					throw new SerializationException(
						String.Format(
						"Cannot create XName from '{0}'. Namespace prefix '{1}' is unknown. Please make sure that namespace prefix is provided using NamespacePrefixAttribute.",
						name ?? String.Empty,
						parts[0]));
				}

				xname = ns + parts[1];
			}
			else
			{
				xname = name;
			}
			return xname;
		}
	}
}
