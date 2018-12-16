# MinimalXmlStructureAttribute Class

**Namespace:** Artemkv.Transformation.XmlToObject

The attribute to provide the minimal Xml structure that has to be created upon serialization.

## Syntax

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)](AttributeUsage(AttributeTargets.Class,-AllowMultiple-=-true,-Inherited-=-true))
public class MinimalXmlStructureAttribute : Attribute
```

## Constructors

### MinimalXmlStructureAttribute

#### Summary

Initializes a new instance of MinimalXmlStructureAttribute class.

```csharp
	public MinimalXmlStructureAttribute(string elementXPath)
```

#### Parameters:

_elementXPath_
: The XPath of the element.