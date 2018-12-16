using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Provides the type info about the serializable type.
	/// </summary>
	public class SerializableTypeInfo
	{
		#region Private Members

		private static MemoryCache Cache = new MemoryCache("TypeInfo");

		private SerializableMemberInfo[] _memberInfo;

		#endregion Private Members

		#region Constructors

		private SerializableTypeInfo(Type serializableType)
		{
			var members = new List<SerializableMemberInfo>();

			var fields = serializableType.GetFields();
			foreach(var field in fields)
			{
				members.Add(new SerializableMemberInfo(field));
			}

			var properties = serializableType.GetProperties();
			foreach (var property in properties)
			{
				members.Add(new SerializableMemberInfo(property));
			}

			_memberInfo = members.ToArray();
		}

		#endregion Constructors

		#region Public Static Methods

		/// <summary>
		/// Gets the type info for the specified type.
		/// </summary>
		/// <param name="serializableType">The serializable type for which the type info is requested.</param>
		/// <returns>The type info for the specified type.</returns>
		public static SerializableTypeInfo GetTypeInfo(Type serializableType)
		{
			var typeInfo = (SerializableTypeInfo)Cache.Get(serializableType.FullName);

			if (typeInfo == null)
			{
				typeInfo = new SerializableTypeInfo(serializableType);
				Cache.AddOrGetExisting(serializableType.FullName, typeInfo, DateTimeOffset.Now.AddMinutes(InternalConfiguration.CacheExpirationPeriodSeconds));
			}
			return typeInfo;
		}

		#endregion Public Static Methods

		#region Public Properties

		/// <summary>
		/// Gets the member info.
		/// </summary>
		public SerializableMemberInfo[] Members
		{
			get
			{
				return _memberInfo;
			}
		}

		#endregion Public Properties
	}
}
