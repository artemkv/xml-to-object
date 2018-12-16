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
using System.Runtime.Serialization;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// The exception is thrown when serialization cannot be performed.
	/// </summary>
	[Serializable]
	public class SerializationException : Exception, ISerializable
	{
		/// <summary>
		/// Initializes a new instance of <c>SerializationException</c> class.
		/// </summary>
		public SerializationException()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of <c>SerializationException</c> class.
		/// </summary>
		/// <param name="message">The message that describes an error.</param>
		public SerializationException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of <c>SerializationException</c> class.
		/// </summary>
		/// <param name="message">The message that describes an error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
		public SerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of <c>SerializationException</c> class.
		/// </summary>
		/// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized
		/// object data about the exception being thrown.</param>
		/// <param name="context"> The System.Runtime.Serialization.StreamingContext that contains contextual
		/// information about the source or destination.</param>
		protected SerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
