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
	/// The attribute to provide the mapping information for serializing/deserializing the object.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class SerializationFormatAttribute : Attribute
	{
		/// <summary>
		/// Initializes the new instance of <c>SerializationFormatAttribute</c> class.
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <param name="numberStyle">The number style.</param>
		/// <param name="dateTypeStyle">The datetime style.</param>
		public SerializationFormatAttribute(
			string format = "", 
			NumberStyles numberStyle = NumberStyles.None, 
			DateTimeStyles dateTypeStyle = DateTimeStyles.None)
		{
			this.Format = format;
			this.NumberStyle = numberStyle;
			this.DateTimeStyle = dateTypeStyle;
		}

		/// <summary>
		/// Gets or sets the format string.
		/// </summary>
		public string Format { get; set; }

		/// <summary>
		/// Gets or sets the number style.
		/// </summary>
		public NumberStyles NumberStyle { get; set; }

		/// <summary>
		/// Gets or sets the datetime style.
		/// </summary>
		public DateTimeStyles DateTimeStyle { get; set; }
	}
}
