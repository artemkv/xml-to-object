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
	/// The attribute to provide the array item element name (only for arrays).
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class ArrayItemElementNameAttribute : Attribute
	{
		/// <summary>
		/// Initializes the new instance of <c>ArrayItemElementNameAttribute</c> class.
		/// </summary>
		/// <param name="elementName">The name of the element used for the array item.</param>
		public ArrayItemElementNameAttribute(string elementName)
		{
			// TODO: validate params

			this.ElementName = elementName;
		}

		/// <summary>
		/// Gets or sets the name of the element created for the array item.
		/// </summary>
		public string ElementName { get; set; }
	}
}
