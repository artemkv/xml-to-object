# Controlling Xml Structure: Advanced Options

## How to specify that element/attribute is required by Xml Schema?

Apply **XmlMandatoryAttribute** to the class field/property.

```csharp
public class Person : IXPathSerializable
{
	public string FirstName { get; set; }

	[XmlMandatory]
	public string LastName { get; set; }
}
```

As you can see, the FirstName property is optional, and the LastName is a mandatory one.

This has 2 possible effects on a serialization.

First, when you serialize an object with fields/properties that are empty, the optional properties are simply skipped, while for the mandatory properties serialization will still generate empty Xml elements/attributes.

```csharp
Person person = new Person();
string xml = person.ToXml();
```

```xml
<Person>
  <LastName></LastName>
</Person>
```

Second, when you deserialize an object and the value cannot be found in the Xml, optional fields/properties are simply reset to their default values, while in case of a mandatory field/property the **SerializationException** is thrown.

## How can I provide additional Xml structure?

Sometimes it's useful to be able to create elements and attributes that are not directly mapped to the class fields/properties. Imagine the class where all the properties are optional:

```csharp
public class Person : IXPathSerializable
{
	[MappingXPath("/artist/personal_data/given_name")]
	public string FirstName { get; set; }

	[MappingXPath("/artist/personal_data/family_name")]
	public string LastName { get; set; }
}
```

If you serialize a new instance of this class, you will get an empty string, because all your properties will be empty and, thus, skipped.

If you still want personal_data element generated, you can apply **MinimalXmlStructureAttribute** attribute (multiple attributes allowed) to the class specifying the XPath of the element to be created.

```csharp
[MinimalXmlStructureAttribute("/artist/personal_data")]
public class Person : IXPathSerializable
{
	[MappingXPath("/artist/personal_data/given_name")]
	public string FirstName { get; set; }

	[MappingXPath("/artist/personal_data/family_name")]
	public string LastName { get; set; }
}
```

Now the result will be:

```xml
<artist>
  <personal_data />
</artist>
```

## Can I serialize the value as an Xml fragment?

This can be useful if you already have a piece of Xml and you want to wrap it with an outer Xml, similar to wrapping Xml message with the Soap envelope. This can be achieved by using **SerializeAsXmlFragmentAttribute**.

```csharp
public class Envelop : IXPathSerializable
{
	[MappingXPath("/Root/Envelop")]
	[SerializeAsXmlFragment]
	public string Message { get; set; }
}
```

```csharp
Envelop envelop = new Envelop()
{
	Message = "<Message>Hello world</Message>"
};

string xml = envelop.ToXml();
```

```xml
<Root>
  <Envelop>
    <Message>Hello world</Message>
  </Envelop>
</Root>
```

## Can I get/pass XDocument object instead of the string when serializing/deserializing the object?

Use **ToXmlDocument** and **LoadFromXmlDocument** respectively. 

When you pass the string with Xml to **LoadFromXml**, internally it creates an object of XDocument class. If you already have one, passing your XDocument instance to **LoadFromXmlDocument** will spare some CPU cycles.

Using **ToXmlDocument** might be useful if you want to perform some additional manipulations on the XDocument object before converting it to string.

```csharp
XDocument xmlDoc = person.ToXmlDocument();
xmlDoc.AddFirst(new XComment("Serialized with XmlToObject library"));
```

## My Xml uses namespaces, what should I do?

In short, you need to use namespace prefixes when mapping fields/properties to Xml elements and attributes and use **NamespacePrefixAttribute** to provide prefixes and namespaces.

Let's assume you have a schema that describes a message format:

```xml
<t:Envelop t:created="2012-11-18T13:00:00.0000000" xmlns:t="http://Transport">
  <t:Message>
    <Person t:id="123" xmlns="http://Business">
      <FirstName>Hugh</FirstName>
      <LastName>Laurie</LastName>
    </Person>
  </t:Message>
</t:Envelop>
```

Elements Envelop and Message are in the namespace http://Transport. Attribute id of class Person is also in the namespace http://Transport. Elements Person, FirstName and LastName are in the namespace http://Business.

You could map your classes as follows:

```csharp
[NamespacePrefix("t", "http://Transport")]
public class Envelop : IXPathSerializable
{
	[MappingXPath("/t:Envelop/t:Message")]
	public Object Message { get; set; }

	[MappingXPath("/t:Envelop", "t:created")]
	public DateTime Created { get; set; }
}

[NamespacePrefix("b", "http://Business")]
public class Person : IXPathSerializable
{
	[MappingXPath("/b:Person", "t:id")]
	public Int32 Id { get; set; }
	[MappingXPath("/b:Person/b:FirstName")]
	public string FirstName { get; set; }
	[MappingXPath("/b:Person/b:LastName")]
	public string LastName { get; set; }
}
```

Please note that when serialized, namespace prefix declaration flows from the outer class (Envelop) to the inner class, so you did not have to declare prefix for the namespace http://Transport on the class Person.

**Read next**: [Type Conversion: Formats and Format Providers](Type-Conversion.md)
