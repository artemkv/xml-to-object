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
	/// The attribute to provide version starting from which the class member should be serialized.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class SerializedFromVersionAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of <c>SerializedFromVersionAttribute</c>.
		/// </summary>
		/// <param name="version">The version starting from which the class member should be serialized.</param>
		public SerializedFromVersionAttribute(int version)
		{
			Version = version;
		}

		/// <summary>
		/// Gets the version starting from which the class member should be serialized.
		/// </summary>
		public int Version { get; private set; }
	}
}
