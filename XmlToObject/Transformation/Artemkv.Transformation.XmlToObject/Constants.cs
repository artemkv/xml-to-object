﻿#region Copyright
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
	/// Contains constants used by serialization.
	/// </summary>
	public class Constants
	{
		/// <summary>
		/// The name of the attribute used to store type info in the Xml.
		/// </summary>
		public static readonly string TypeInfoAttributeName = @"{http://xmltoobject.codeplex.com}__type";

		/// <summary>
		/// The name of the attribute that is used if the value is null.
		/// </summary>
		public static readonly string NullValueAttributeName = @"{http://xmltoobject.codeplex.com}__isNull";
	}
}
