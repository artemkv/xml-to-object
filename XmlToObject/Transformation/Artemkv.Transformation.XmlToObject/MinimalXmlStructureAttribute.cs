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
	/// The attribute to provide the minimal Xml structure that has to be created upon serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class MinimalXmlStructureAttribute : Attribute
	{
		/// <summary>
		/// Initializes the new instance of <c>EnsureXmlStructureAttribute</c> class.
		/// </summary>
		/// <param name="elementXPath">The XPath of the element.</param>
		public MinimalXmlStructureAttribute(string elementXPath)
		{
			// TODO: validate params

			this.ElementXPath = elementXPath;
		}

		/// <summary>
		/// Gets or sets the XPath of the element.
		/// </summary>
		public string ElementXPath { get; set; }
	}
}
