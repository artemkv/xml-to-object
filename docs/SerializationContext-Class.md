# SerializationContext class

**Namespace:** Artemkv.Transformation.XmlToObject

Serialization context.

## Syntax

```csharp
public class SerializationContext
```

## Constructors

### SerializationContext

#### Summary

Initializes a new instance of SerializationContext class.

```csharp
	public SerializationContext(
		IXPathSerializable serializable, 
		SerializableMemberInfo member, 
		Mapping mapping,
		MemberInfo[]() useMembers,
		Type deserializeAs = null,
		bool emitTypeInfo = false,
		int version = 0,
		IFormatProvider formatProvider = null,
		IXmlNamespaceResolver resolver = null)
```

#### Parameters:

_serializable_
: The object which is serialized/deserialized.

_member_
: The member whose value is serialized/deserialized.

_mapping_
: The mapping info for the member.

_useMembers_
: The list of members to use.

_deserializeAs_
: The type that the value should be deserialized into.

_emitTypeInfo_
: Specifies whether the type info should be emitted when serializing the object.

_version_
: The version of the object that should be used when serializing the object.

_formatProvider_
: The format provider passed to the serializer.

_resolver_
: The xml namespace resolver.