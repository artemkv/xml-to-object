# MappingXPathRelativeToParentAttribute Class

**Namespace:** Artemkv.Transformation.XmlToObject

The attribute to provide the mapping information for serializing/deserializing the object as a part of outer object.

## Syntax

```csharp
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class MappingXPathRelativeToParentAttribute : Attribute
```

## Constructors

### MappingXPathRelativeToParentAttribute

#### Summary

Initializes a new instance of MappingXPathRelativeToParentAttribute class.

```csharp
	public MappingXPathRelativeToParentAttribute(string elementXPath, string attributeName = "")
```

#### Parameters:

_elementXPath_
: The XPath of the element.

_attributeName_
: The attribute name.