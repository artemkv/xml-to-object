# XmlSerialization Class

**Namespace:** Artemkv.Transformation.XmlToObject

Provides the extension methods for object serialization.

## Syntax

```csharp
public static class XmlSerialization
```

## Methods

### XmlSerialization.LoadFromXml<T, TFilter> Method

#### Summary

Loads an object from xml string.

```csharp
	public static T LoadFromXml<T, TFilter>(
		string xmlString,
		MemberFilter<TFilter> filter = null,
		int version = 0,
		IFormatProvider provider = null)
		where T : IXPathSerializable, new()
```

#### Type Parameters:

_T_
: The type of the object to deserialize.

_TFilter_
: The type of the object that is used as a member filter.

#### Parameters:

_xmlString_
: The xml string with the serialized object.

_filter_
: The member filter.

_version_
: The version of the object that should be used when deserializing the object.

_provider_
: An object that supplies culture-specific formatting information.

#### Returns:

The deserialized object.

#### Exceptions:

_System.ArgumentNullException_
: Is thrown if the xmlString parameter is null.

_Artemkv.Transformation.XmlToObject.SerializationException_
: Is thrown if the serialization cannot be performed.

### XmlSerialization.LoadFromXml<T> Method

#### Summary

Loads an object from xml string.

```csharp
	public static T LoadFromXml<T>(
		string xmlString,
		int version = 0,
		IFormatProvider provider = null)
		where T : IXPathSerializable, new()
```

#### Type Parameters:

_T_
: The type of the object to deserialize.

#### Parameters:

_xmlString_
: The xml string with the serialized object.

_version_
: The version of the object that should be used when deserializing the object.

_provider_
: An object that supplies culture-specific formatting information.

#### Returns:

The deserialized object.

#### Exceptions:

_System.ArgumentNullException_
: Is thrown if the xmlString parameter is null.

_Artemkv.Transformation.XmlToObject.SerializationException_
: Is thrown if the serialization cannot be performed.

### XmlSerialization.LoadFromXmlDocument<T, TFilter> Method

#### Summary

Loads an object from xml document.

```csharp
	public static T LoadFromXml<T, TFilter>(
		string xmlString,
		MemberFilter<TFilter> filter = null,
		int version = 0,
		IFormatProvider provider = null)
		where T : IXPathSerializable, new()
```

#### Type Parameters:

_T_
: The type of the object to deserialize.

_TFilter_
: The type of the object that is used as a member filter.

#### Parameters:

_document_
: The xml document with the serialized object.

_filter_
: The member filter.

_version_
: The version of the object that should be used when deserializing the object.

_provider_
: An object that supplies culture-specific formatting information.

#### Returns:

The deserialized object.

#### Exceptions:

_System.ArgumentNullException_
: Is thrown if the document parameter is null.

_Artemkv.Transformation.XmlToObject.SerializationException_
: Is thrown if the serialization cannot be performed.

### XmlSerialization.LoadFromXmlDocument<T> Method

#### Summary

Loads an object from xml document.

```csharp
	public static T LoadFromXmlDocument<T>(
		XDocument document,
		int version = 0,
		IFormatProvider provider = null)
		where T : IXPathSerializable, new()
```

#### Type Parameters:

_T_
: The type of the object to deserialize.

#### Parameters:

_document_
: The xml document with the serialized object.

_version_
: The version of the object that should be used when deserializing the object.

_provider_
: An object that supplies culture-specific formatting information.

#### Returns:

The deserialized object.

#### Exceptions:

_System.ArgumentNullException_
: Is thrown if the document parameter is null.

_Artemkv.Transformation.XmlToObject.SerializationException_
: Is thrown if the serialization cannot be performed.

### XmlSerialization.ToXml Method

#### Summary

Serializes an object to xml.

```csharp
	public static string ToXml(
		this IXPathSerializable serializable,
		bool emitTypeInfo = false,
		int version = 0,
		IFormatProvider provider = null)
```

#### Parameters:

_serializable_
: The object to serialize.

_emitTypeInfo_
: Specifies whether the type info should be emitted when serializing the object.

_version_
: The version of the object that should be used when serializing the object.

_provider_
: An object that supplies culture-specific formatting information.

#### Returns:
The xml string with the serialized object.

#### Exceptions:

_Artemkv.Transformation.XmlToObject.SerializationException_
: Is thrown if the serialization cannot be performed.

### XmlSerialization.ToXml<TFilter> Method

#### Summary

Serializes an object to xml.

```csharp
	public static string ToXml<TFilter>(
		this IXPathSerializable serializable,
		MemberFilter<TFilter> filter = null,
		bool emitTypeInfo = false,
		int version = 0,
		IFormatProvider provider = null)
```

#### Parameters:

_serializable_
: The object to serialize.

_filter_
: The member filter.

_emitTypeInfo_
: Specifies whether the type info should be emitted when serializing the object.

_version_
: The version of the object that should be used when serializing the object.

_provider_
: An object that supplies culture-specific formatting information.

#### Returns:
The xml string with the serialized object.

#### Exceptions:

_Artemkv.Transformation.XmlToObject.SerializationException_
: Is thrown if the serialization cannot be performed.

### XmlSerialization.ToXmlDocument Method

#### Summary

Serializes an object to xml document.

```csharp
	public static XDocument ToXmlDocument(
		this IXPathSerializable serializable,
		bool emitTypeInfo = false,
		int version = 0,
		IFormatProvider provider = null)
```

#### Parameters:

_serializable_
: The object to serialize.

_emitTypeInfo_
: Specifies whether the type info should be emitted when serializing the object.

_version_
: The version of the object that should be used when serializing the object.

_provider_
: An object that supplies culture-specific formatting information.

#### Returns:
The xml document with the serialized object.

#### Exceptions:

_Artemkv.Transformation.XmlToObject.SerializationException_
: Is thrown if the serialization cannot be performed.

### XmlSerialization.ToXmlDocument<TFilter> Method

#### Summary

Serializes an object to xml document.

```csharp
	public static XDocument ToXmlDocument<TFilter>(
		this IXPathSerializable serializable,
		MemberFilter<TFilter> filter = null,
		bool emitTypeInfo = false,
		int version = 0,
		IFormatProvider provider = null)
```

#### Parameters:

_serializable_
: The object to serialize.

_filter_
: The member filter.

_emitTypeInfo_
: Specifies whether the type info should be emitted when serializing the object.

_version_
: The version of the object that should be used when serializing the object.

_provider_
: An object that supplies culture-specific formatting information.

#### Returns:
The xml document with the serialized object.

#### Exceptions:

_Artemkv.Transformation.XmlToObject.SerializationException_
: Is thrown if the serialization cannot be performed.