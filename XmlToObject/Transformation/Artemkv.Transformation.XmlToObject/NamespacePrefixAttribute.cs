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
	/// The attribute to provide the namespace used when serializing/deserializing the class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class NamespacePrefixAttribute : Attribute
	{
		/// <summary>
		/// Initializes the new instance of <c>NamespacePrefixAttribute</c> class.
		/// </summary>
		/// <param name="prefix">The prefix to associate with the namespace being added.</param>
		/// <param name="uri">The namespace to add.</param>
		public NamespacePrefixAttribute(string prefix, string uri)
		{
			if (prefix == null)
				throw new ArgumentNullException("prefix");
			if (uri == null)
				throw new ArgumentNullException("uri");

			this.Prefix = prefix;
			this.Uri = uri;
		}

		/// <summary>
		/// Gets or sets the prefix to associate with the namespace being added.
		/// </summary>
		public string Prefix { get; set; }

		/// <summary>
		/// Gets or sets the namespace to add.
		/// </summary>
		public string Uri { get; set; }
	}
}
