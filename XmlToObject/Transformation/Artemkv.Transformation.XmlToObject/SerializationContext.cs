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
using System.Xml;
using System.Reflection;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Serialization context.
	/// </summary>
	public class SerializationContext
	{
		/// <summary>
		/// Initializes a new instance of <c>SerializationContext</c> class.
		/// </summary>
		/// <param name="serializable">The object which is serialized/deserialized.</param>
		/// <param name="member">The member whose value is serialized/deserialized.</param>
		/// <param name="mapping">The mapping info for the member.</param>
		/// <param name="useMembers">The list of members to use.</param>
		/// <param name="deserializeAs">The type that the value should be deserialized into.</param>
		/// <param name="emitTypeInfo">Specifies whether the type info should be emitted when serializing the object.</param>
		/// <param name="version">The version of the object that should be used when serializing the object.</param>
		/// <param name="formatProvider">The format provider passed to the serializer.</param>
		/// <param name="resolver">The xml namespace resolver.</param>
		public SerializationContext(
			IXPathSerializable serializable, 
			SerializableMemberInfo member, 
			Mapping mapping,
			MemberInfo[] useMembers,
			Type deserializeAs = null,
			bool emitTypeInfo = false,
			int version = 0,
			IFormatProvider formatProvider = null,
			XmlNamespaceManager resolver = null)
		{
			if (serializable == null)
				throw new ArgumentNullException("serializable");
			if (member == null)
				throw new ArgumentNullException("member");
			if (mapping == null)
				throw new ArgumentNullException("mapping");

			this.Serializable = serializable;
			this.Member = member;
			this.Mapping = mapping;
			this.FormatProvider = formatProvider;
			this.EmitTypeInfo = emitTypeInfo;
			this.DeserializeAs = deserializeAs;
			this.NamespaceResolver = resolver;
			this.UseMembers = useMembers;
		}

		/// <summary>
		/// Gets the object which is serialized/deserialized.
		/// </summary>
		public IXPathSerializable Serializable { get; private set; }

		/// <summary>
		/// Gets the member whose value is serialized/deserialized.
		/// </summary>
		public SerializableMemberInfo Member { get; private set; }

		/// <summary>
		/// Gets the mapping info for the member.
		/// </summary>
		public Mapping Mapping { get; private set; }

		/// <summary>
		/// Gets the type that the value should be deserialized into.
		/// </summary>
		public Type DeserializeAs { get; private set; }

		/// <summary>
		/// Gets the value that specifies whether the type info should be emitted when serializing the object.
		/// </summary>
		public bool EmitTypeInfo { get; private set; }

		/// <summary>
		/// Gets the format provider passed to the serializer.
		/// </summary>
		public IFormatProvider FormatProvider { get; private set; }

		/// <summary>
		/// Gets the xml namespace resolver.
		/// </summary>
		public XmlNamespaceManager NamespaceResolver { get; private set; }

		/// <summary>
		/// Gets the version starting from which the class member should be serialized.
		/// </summary>
		public int Version { get; private set; }

		/// <summary>
		/// Gets the list of members to use.
		/// </summary>
		public MemberInfo[] UseMembers { get; private set; }
	}
}
