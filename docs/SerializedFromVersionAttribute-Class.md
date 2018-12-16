# SerializedFromVersionAttribute Class

**Namespace:** Artemkv.Transformation.XmlToObject

The attribute to provide version starting from which the class member should be serialized.

## Syntax

```csharp
[AttributeUsage(AttributeTargets.Property ](-AttributeTargets.Field,-AllowMultiple-=-false,-Inherited-=-true))
public class SerializedFromVersionAttribute : Attribute
```

## Constructors

### SerializedFromVersionAttribute

#### Summary

Initializes a new instance of SerializedFromVersionAttribute class.

```csharp
	public SerializedFromVersionAttribute(int version)
```

#### Parameters:

_version_
: The version starting from which the class member should be serialized.