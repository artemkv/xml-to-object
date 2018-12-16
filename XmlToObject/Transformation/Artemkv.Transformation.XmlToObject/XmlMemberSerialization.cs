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
using System.Reflection;
using System.Globalization;

namespace Artemkv.Transformation.XmlToObject
{
	/// <summary>
	/// Contains methods for the member serialization/deserialization.
	/// </summary>
	internal static class XmlMemberSerialization
	{
		public static string GetMemberXmlValue(object value, SerializationContext context, bool useConvertor = true)
		{
			if (value == null)
			{
				return null;
			}

			// Handle the property that has a convertor defined.
			var valueConvertorAttribute = context.Member.GetSingleCustomAttribute<ValueConvertorAttribute>();
			if (useConvertor && valueConvertorAttribute != null)
			{
				var convertor = valueConvertorAttribute.Converter.GetConstructor(Type.EmptyTypes).Invoke(null) as IValueConvertor;
				if (convertor == null)
				{
					throw new InvalidOperationException(
						String.Format(
							"Cannot serialize {0} '{1}'. Could not create instance of type {2}",
							context.Member.MemeberTypeString,
							context.Member.Name,
							valueConvertorAttribute.Converter.ToString()));
				}

				string convertedValue;
				bool converted = convertor.TryConvertBack(value, context, out convertedValue);
				if (converted)
				{
					return convertedValue;
				}
				else
				{
					throw new SerializationException(
						String.Format(
							"Cannot serialize {0} '{1}'. Could not parse value '{2}' with converter of type {3}",
							context.Member.MemeberTypeString,
							context.Member.Name,
							value,
							valueConvertorAttribute.Converter.ToString()));
				}
			}
			else
			{
				return ConvertPrimitiveTypeToXmlString(value.GetType(), value, context);
			}
		}

		public static object GetMemberActualValue(Type type, string value, SerializationContext context)
		{
			// If property is optional and the value is empty or null, we simply set the property value to the default value
			if (String.IsNullOrEmpty(value))
			{
				// TODO: default value can be set on the mapping
				object defaultValue = type.IsValueType ? Activator.CreateInstance(type) : null;
				return defaultValue;
			}

			// AT this point the property value is neither null nor empty. 
			// So we have to try to assign it (and convert if necessary).

			// Handle the property that has a convertor defined.
			var valueConvertorAttribute = context.Member.GetSingleCustomAttribute<ValueConvertorAttribute>();
			if (valueConvertorAttribute != null)
			{
				var convertor = valueConvertorAttribute.Converter.GetConstructor(Type.EmptyTypes).Invoke(null) as IValueConvertor;
				if (convertor == null)
				{
					throw new InvalidOperationException(
						String.Format(
							"Cannot deserialize {0} '{1}'. Could not create instance of type {2}",
							context.Member.MemeberTypeString,
							context.Member.Name,
							valueConvertorAttribute.Converter.ToString()));
				}

				object convertedValue;
				bool converted = convertor.TryConvert(value, context, out convertedValue);
				if (converted)
				{
					return convertedValue;
				}
				else
				{
					throw new SerializationException(
						String.Format(
							"Cannot deserialize {0} '{1}'. Could not parse value '{2}' with converter of type {3}",
							context.Member.MemeberTypeString,
							context.Member.Name,
							value,
							valueConvertorAttribute.Converter.ToString()));
				}
			}
			else
			{
				return ConvertXmlStringToPrimitiveType(type, value, context);
			}
		}

		public static object ConvertXmlStringToPrimitiveType(Type type, string value, SerializationContext context)
		{
			try
			{
				if (type == typeof(String))
				{
					return value;
				}
				if (type == typeof(Char) || type == typeof(Char?))
				{
					if (value.Length == 1)
					{
						return value[0];
					}
					else
					{
						throw new SerializationException(
							String.Format("Could not parse value '{0}' as a char", value));
					}
				}
				if (type == typeof(bool) || type == typeof(bool?))
				{
					return bool.Parse(value);
				}
				if (type == typeof(Byte) || type == typeof(Byte?))
				{
					return Byte.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(SByte) || type == typeof(SByte?))
				{
					return SByte.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(Int16) || type == typeof(Int16?))
				{
					return Int16.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(UInt16) || type == typeof(UInt16?))
				{
					return UInt16.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(Int32) || type == typeof(Int32?))
				{
					return Int32.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(UInt32) || type == typeof(UInt32?))
				{
					return UInt32.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(Int64) || type == typeof(Int64?))
				{
					return Int64.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(UInt64) || type == typeof(UInt64?))
				{
					return UInt64.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(Single) || type == typeof(Single?))
				{
					return Single.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(Double) || type == typeof(Double?))
				{
					return Double.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(Decimal) || type == typeof(Decimal?))
				{
					return Decimal.Parse(value, context.Mapping.NumberStyle, context.FormatProvider);
				}
				if (type == typeof(byte[]))
				{
					return System.Convert.FromBase64String(value);
				}
				if (type == typeof(DateTime) || type == typeof(DateTime?))
				{
					DateTime date;
					bool parsed = DateTime.TryParseExact(value, context.Mapping.GetFormat(type), context.FormatProvider, context.Mapping.DateTimeStyle, out date);
					if (parsed)
					{
						return date;
					}
					else
					{
						throw new SerializationException(String.Format("Could not parse value '{0}' as a date time.", value));
					}
				}
				if (type == typeof(Guid) || type == typeof(Guid?))
				{
					return Guid.ParseExact(value, context.Mapping.GetFormat(type));
				}

				throw new SerializationException(
					String.Format("Could not parse value '{0}' of type {1}. Type is not supported.", value, type));
			}
			catch (OverflowException ex)
			{
				throw new SerializationException(
					String.Format(
						"Could not convert value '{0}' to a type '{1}'",
						value,
						type.ToString()), ex);
			}
			catch (ArgumentNullException ex)
			{
				throw new SerializationException(
					String.Format(
						"Could not convert value '{0}' to a type '{1}'",
						value,
						type.ToString()), ex);
			}
			catch (FormatException ex)
			{
				throw new SerializationException(
					String.Format(
						"Could not convert value '{0}' to a type '{1}'",
						value,
						type.ToString()), ex);
			}
		}

		public static string ConvertPrimitiveTypeToXmlString(Type type, object value, SerializationContext context)
		{
			if (type == typeof(String))
			{
				return value.ToString();
			}
			if (type == typeof(Char))
			{
				return value.ToString();
			}
			if (type == typeof(Boolean))
			{
				return value.ToString();
			}
			if (type == typeof(Byte) || type == typeof(Byte?))
			{
				return ((Byte)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(SByte) || type == typeof(SByte?))
			{
				return ((SByte)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(Int16) || type == typeof(Int16?))
			{
				return ((Int16)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(UInt16) || type == typeof(UInt16?))
			{
				return ((UInt16)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(Int32) || type == typeof(Int32?))
			{
				return ((Int32)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(UInt32) || type == typeof(UInt32?))
			{
				return ((UInt32)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(Int64) || type == typeof(Int64?))
			{
				return ((Int64)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(UInt64) || type == typeof(UInt64?))
			{
				return ((UInt64)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(Single) || type == typeof(Single?))
			{
				return ((Single)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(Double) || type == typeof(Double?))
			{
				return ((Double)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(Decimal) || type == typeof(Decimal?))
			{
				return ((Decimal)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(DateTime) || type == typeof(DateTime?))
			{
				return ((DateTime)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(Guid) || type == typeof(Guid?))
			{
				return ((Guid)value).ToString(context.Mapping.GetFormat(type), context.FormatProvider);
			}
			if (type == typeof(byte[]))
			{
				return Convert.ToBase64String((byte[])value);
			}

			throw new SerializationException(
				String.Format("Type '{0}' is not supported.", type));
		}
	}
}
