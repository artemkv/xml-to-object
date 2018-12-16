# ValueConverterBase Class

**Namespace:** Artemkv.Transformation.XmlToObject

Base implementation of the IValueConvertor.

## Syntax

```csharp
public abstract class ValueConverterBase : IValueConvertor
```

## Methods

### Convert

#### Summary

Performs the conversion of a string to a value of member type.

```csharp
	public object Convert(string value, SerializationContext context)
```

#### Parameters:

_value_
: The value to convert.

_context_
: The serialization context.

#### Returns:

The converted value.

### ConvertBack

#### Summary

Performs the backward conversion of the value of member type to a string.

```csharp
	public string ConvertBack(object value, SerializationContext context)
```

#### Parameters:

_value_
: The value to convert.

_context_
: The serialization context.

#### Returns:

The converted value.

### TryConvert

#### Summary

Tries to perform the conversion of a string to a value of member type.

```csharp
	public abstract bool TryConvert(string value, SerializationContext context, out object convertedValue)
```

#### Parameters:

_value_
: The value to convert.

_context_
: The serialization context.

_convertedValue_
: The converted value.

#### Returns:

True, if conversion was successful; otherwise, False.

### TryConvertBack

#### Summary

Performs the conversion of a string to a value of member type.

```csharp
	public abstract bool TryConvertBack(object value, SerializationContext context, out string convertedValue)
```

#### Parameters:

_value_
: The value to convert.

_context_
: The serialization context.

_convertedValue_
: The converted value.

#### Returns:

True, if conversion was successful; otherwise, False.