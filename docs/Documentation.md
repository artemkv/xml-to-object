##### Skip intro and go to [Tutorial](Tutorial.md) / [API Reference](API-Reference.md)

# Intro

The best way to describe the idea of the project is to show the examples of the code.

Imagine you have a class Person with the FirstName and LastName properties:

```csharp
public class Person
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
}
```

And you have an instance of this class:

```csharp
Person person = new Person()
{
	FirstName = "Hugh",
	LastName = "Laurie"
};
```

And you need to serialize it to the Xml structure as follows:

```xml
<Object>
  <Field name="FirstName">Hugh</Field>
  <Field name="LastName">Laurie</Field>
</Object>
```

The main problem here is that the Xml does not mirror the class structure, so you have to shape your data in order to put it into the Xml like this. Normally, you have to do it manually.

Using XmlToObject all you need to do is:
1. Mark your class as **IXPathSerializable** (this is simply to attach extension methods to it)
2. Provide mapping by applying **MappingXPath** attribute to the properties you want to serialize

```csharp
public class Person : IXPathSerializable
{
	[MappingXPath("/Object/Field[@name='FirstName']")]
	public string FirstName { get; set; }
	[MappingXPath("/Object/Field[@name='LastName']")]
	public string LastName { get; set; }
}
```

3. Call **ToXml** to serialize an object to Xml

```csharp
string xml = person.ToXml();
```

4. Call **LoadFromXml** to deserialize an object from Xml

```csharp
Person person = XmlSerialization.LoadFromXml<Person>(xml);
```

# What's next?

Interested? Please read the [Tutorial](Tutorial.md)