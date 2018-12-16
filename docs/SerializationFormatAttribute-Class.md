# SerializationFormatAttribute Class

**Namespace:** Artemkv.Transformation.XmlToObject

The attribute to provide the mapping information for serializing/deserializing the object.

## Syntax

```csharp
[AttributeUsage(AttributeTargets.Property ](-AttributeTargets.Field,-AllowMultiple-=-false,-Inherited-=-true))
public class SerializationFormatAttribute : Attribute
```

## Constructors

### SerializationFormatAttribute

#### Summary

Initializes a new instance of SerializationFormatAttribute class.

```csharp
	public SerializationFormatAttribute(
		string format = "", 
		NumberStyles numberStyle = NumberStyles.None, 
		DateTimeStyles dateTypeStyle = DateTimeStyles.None)
```

#### Parameters:

_format_
: The format string.

_numberStyle_
: The number style.

_dateTypeStyle_
: The datetime style.