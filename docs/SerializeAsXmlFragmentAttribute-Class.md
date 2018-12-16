# SerializeAsXmlFragmentAttribute Class

**Namespace:** Artemkv.Transformation.XmlToObject

The attribute to mark fields/properties that should be serialized as an Xml fragment.
The value of the property should contain a valid Xml, or be converted to a valid Xml by a value converter.
	
## Syntax

```csharp
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class SerializeAsXmlFragmentAttribute : Attribute
```
