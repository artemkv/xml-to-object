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
	/// Base implementation of the IValueConvertor.
	/// </summary>
	public abstract class ValueConverterBase : IValueConvertor
	{
		/// <summary>
		/// Performs the conversion of a string to a value of member type.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="context">The serialization context.</param>
		/// <returns>The converted value.</returns>
		/// <exception cref="SerializationException">Is thrown if the conversion cannot be performed.</exception>
		public object Convert(string value, SerializationContext context)
		{
			object convertedValue;
			bool parsed = TryConvert(value, context, out convertedValue);
			if (!parsed)
			{
				throw new SerializationException(String.Format("Unknown value: '{0}'", value));
			}
			return convertedValue;
		}

		/// <summary>
		/// Performs the backward conversion of the value of member type to a string.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="context">The serialization context.</param>
		/// <returns>The converted value.</returns>
		/// <exception cref="SerializationException">Is thrown if the conversion cannot be performed.</exception>
		public string ConvertBack(object value, SerializationContext context)
		{
			string convertedValue;
			bool parsed = TryConvertBack(value, context, out convertedValue);
			if (!parsed)
			{
				throw new SerializationException(String.Format("Unknown value: '{0}'", value));
			}
			return convertedValue;
		}

		/// <summary>
		/// Tries to perform the conversion of a string to a value of member type.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="context">The serialization context.</param>
		/// <param name="convertedValue">The converted value.</param>
		/// <returns>True, if conversion was successful; otherwise, False.</returns>
		public abstract bool TryConvert(string value, SerializationContext context, out object convertedValue);

		/// <summary>
		/// Tries to perform the backward conversion of the value of member type to a string.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="context">The serialization context.</param>
		/// <param name="convertedValue">The converted value.</param>
		/// <returns>True, if conversion was successful; otherwise, False.</returns>
		public abstract bool TryConvertBack(object value, SerializationContext context, out string convertedValue);
	}
}
