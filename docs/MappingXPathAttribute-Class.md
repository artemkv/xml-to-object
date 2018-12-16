# MappingXPathAttribute Class

**Namespace:** Artemkv.Transformation.XmlToObject

The attribute to provide the mapping information for serializing/deserializing the object.

## Syntax

```csharp
[AttributeUsage(AttributeTargets.Property ](-AttributeTargets.Field,-AllowMultiple-=-false,-Inherited-=-true))
public class MappingXPathAttribute : Attribute
```

## Constructors

### MappingXPathAttribute

#### Summary

Initializes a new instance of MappingXPathAttribute class.

```csharp
	public MappingXPathAttribute(string elementXPath, string attributeName = "")
```

#### Parameters:

_elementXPath_
: The XPath of the element.

_attributeName_
: The attribute name.