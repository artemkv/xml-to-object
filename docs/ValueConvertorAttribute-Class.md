# ValueConvertorAttribute Class

**Namespace:** Artemkv.Transformation.XmlToObject

The attribute to provide the converter for serializing/deserializing the object property/field.

## Syntax

```csharp
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ValueConvertorAttribute : Attribute
```

## Constructors

### ValueConvertorAttribute

#### Summary

Initializes a new instance of ValueConvertorAttribute class.

```csharp
	public ValueConvertorAttribute(Type converter)
```

#### Parameters:

_converter_
: The type that should be used as a converter.