# ArrayItemElementNameAttribute Class

**Namespace:** Artemkv.Transformation.XmlToObject

The attribute to provide the array item element name (only for arrays).

## Syntax

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class ArrayItemElementNameAttribute : Attribute
```

## Constructors

### ArrayItemElementNameAttribute

#### Summary

Initializes a new instance of ArrayItemElementNameAttribute class.

```csharp
	public ArrayItemElementNameAttribute(string elementName)
```

#### Parameters:

_elementName_
: The name of the element used for the array item.
