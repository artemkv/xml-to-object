# NamespacePrefixAttribute Class

**Namespace:** Artemkv.Transformation.XmlToObject

The attribute to provide the namespace used when serializing/deserializing the class.

## Syntax

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)](AttributeUsage(AttributeTargets.Class,-AllowMultiple-=-true,-Inherited-=-true))
public class NamespacePrefixAttribute : Attribute
```

## Constructors

### NamespacePrefixAttribute

#### Summary

Initializes a new instance of NamespacePrefixAttribute class.

```csharp
	public NamespacePrefixAttribute(string prefix, string uri)
```

#### Parameters:

_prefix_
: The prefix to associate with the namespace being added.

_uri_
: The namespace to add.