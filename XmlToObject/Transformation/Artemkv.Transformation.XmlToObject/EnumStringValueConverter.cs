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

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Converts enum values to Xml string and back.
	/// </summary>
	public class EnumStringValueConverter : ValueConverterBase
	{
		/// <summary>
		/// Converts an Xml string value to an enum value of the specified type.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="context">The serialization context.</param>
		/// <param name="convertedValue">The converted value.</param>
		/// <returns>True, if conversion was successful; otherwise, False.</returns>
		public override bool TryConvert(string value, SerializationContext context, out object convertedValue)
		{
			convertedValue = null;

			Type valueType = null;
			if (!context.DeserializeAs.IsArray)
			{
				valueType = context.DeserializeAs;
			}
			else
			{
				valueType = context.DeserializeAs.GetElementType();
			}

			if (valueType.IsNullableEnum())
			{
				valueType = Nullable.GetUnderlyingType(valueType);
			}
			if (!valueType.IsEnum)
			{
				return false;
			}

			try
			{
				convertedValue = Enum.Parse(valueType, value, true);
				return true;
			}
			catch (ArgumentNullException)
			{
				return false;
			}
			catch (ArgumentException)
			{
				return false;
			}
			catch (OverflowException)
			{
				return false;
			}
		}

		/// <summary>
		/// Tries to perform the backward conversion of an enum value to Xml string.
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
				return false;
			}
			var valueType = value.GetType();
			if (!valueType.IsEnum && !valueType.IsNullableEnum())
			{
				return false;
			}

			convertedValue = value.ToString();
			return true;
		}
	}
}
