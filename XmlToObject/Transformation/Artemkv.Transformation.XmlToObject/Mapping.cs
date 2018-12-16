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
using System.Globalization;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Holds the mapping information used when serializing/deserializing objects.
	/// </summary>
	public class Mapping
	{
		private string _format;

		/// <summary>
		/// Initializes a new instance of <c>Mapping</c> class.
		/// </summary>
		/// <param name="elementXPath">XPath of the element.</param>
		/// <param name="attributeName">The attribute name.</param>
		/// <param name="serializationFormatAttribute">Instance of <c>SerializationFormatAttribute</c>.</param>
		/// <param name="arrayItemElementNameAttribute">Instance of <c>ArrayItemElementNameAttribute</c>.</param>
		/// <param name="isXmlMandatory">Specifies whether the element/attribute is required by Xml schema.</param>
		/// <param name="asBase64">Specifies whether the values should be serialized as a base64 string.</param>
		/// <param name="asXml">Specifies whether the values should be serialized as an Xml fragment.</param>
		public Mapping(
			string elementXPath = "",
			string attributeName = "",
			SerializationFormatAttribute serializationFormatAttribute = null,
			ArrayItemElementNameAttribute arrayItemElementNameAttribute = null,
			bool isXmlMandatory = false,
			bool asBase64 = false,
			bool asXml = false)
		{
			this.ElementXPath = elementXPath;
			this.AttributeName = attributeName;

			if (serializationFormatAttribute != null)
			{
				this._format = serializationFormatAttribute.Format;
				this.NumberStyle = serializationFormatAttribute.NumberStyle;
				this.DateTimeStyle = serializationFormatAttribute.DateTimeStyle;
			}
			else
			{
				this.NumberStyle = NumberStyles.Any;
				this.DateTimeStyle = DateTimeStyles.RoundtripKind;
			}

			if (arrayItemElementNameAttribute != null)
			{
				this.ArrayItemElementName = arrayItemElementNameAttribute.ElementName;
			}
			else
			{
				this.ArrayItemElementName = "Item";
			}

			this.IsXmlMandatory = isXmlMandatory;
			this.AsBase64 = asBase64;
			this.AsXml = asXml;
		}

		/// <summary>
		/// Gets or sets the XPath of the element.
		/// </summary>
		public string ElementXPath { get; set; }

		/// <summary>
		/// Gets or sets the attribute name.
		/// </summary>
		public string AttributeName { get; set; }

		/// <summary>
		/// Gets or sets the name of the element created for the array item.
		/// </summary>
		public string ArrayItemElementName { get; set; }

		/// <summary>
		/// Gets or sets the value that specifies whether the xml element/attribute is mandatory.
		/// </summary>
		public bool IsXmlMandatory { get; set; }

		/// <summary>
		/// Gets or sets the number style.
		/// </summary>
		public NumberStyles NumberStyle { get; set; }

		/// <summary>
		/// Gets or sets the datetime style.
		/// </summary>
		public DateTimeStyles DateTimeStyle { get; set; }

		/// <summary>
		/// Gets or sets the value that specifies whether the field should be serialized/deserializing as an xml fragment.
		/// </summary>
		public bool AsXml { get; set; }

		/// <summary>
		/// Gets or sets the value that specifies whether the field should be serialized/deserializing as a base64 encoded string.
		/// </summary>
		public bool AsBase64 { get; set; }

		/// <summary>
		/// Returns the format to be used to convert the value of the specified type to string. 
		/// If format was not specified explicitely through the SerializationFormatAttribute, returns the default one.
		/// The default format is determined by the serialized type and favors the round-trip possibility.
		/// </summary>
		/// <param name="type">The type of the value that is serialized.</param>
		/// <returns>The format to be used to convert the value to string.</returns>
		public string GetFormat(Type type)
		{
			if (_format != null)
			{
				return _format;
			}

			if (type == typeof(Byte) || type == typeof(Byte?))
			{
				return "D";
			}
			if (type == typeof(SByte) || type == typeof(SByte?))
			{
				return "D";
			}
			if (type == typeof(Int16) || type == typeof(Int16?))
			{
				return "D";
			}
			if (type == typeof(UInt16) || type == typeof(UInt16?))
			{
				return "D";
			}
			if (type == typeof(Int32) || type == typeof(Int32?))
			{
				return "D";
			}
			if (type == typeof(UInt32) || type == typeof(UInt32?))
			{
				return "D";
			}
			if (type == typeof(Int64) || type == typeof(Int64?))
			{
				return "D";
			}
			if (type == typeof(UInt64) || type == typeof(UInt64?))
			{
				return "D";
			}
			if (type == typeof(Single) || type == typeof(Single?))
			{
				return "R";
			}
			if (type == typeof(Double) || type == typeof(Double?))
			{
				return "R";
			}
			if (type == typeof(Decimal) || type == typeof(Decimal?))
			{
				return "F";
			}
			if (type == typeof(DateTime) || type == typeof(DateTime?))
			{
				return "o";
			}
			if (type == typeof(Guid) || type == typeof(Guid?))
			{
				return "B";
			}

			return "";
		}
	}
}
