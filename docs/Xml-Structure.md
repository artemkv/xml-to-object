# Controlling Xml Structure: Elements and Attributes

## How do I control which element will the value be put in?

Apply **MappingXPathAttribute** to your class fields/properties.

```csharp
public class PersonA : IXPathSerializable
{
	[MappingXPath("/artist/personal_data/given_name")]
	public string FirstName { get; set; }
	[MappingXPath("/artist/personal_data/family_name")]
	public string LastName { get; set; }
}
```

Result:

{code:xml}
<artist>
  <personal_data>
    <given_name>Hugh</given_name>
    <family_name>Laurie</family_name>
  </personal_data>
</artist>
{code:xml}

## How complex can mapping XPath be?

Obviously, you cannot specify just any XPath for the mapping. Serialization library should be able to unambiguously determine the element that the value should be written to. 
So you should start XPath with a single slash ('/'), starting selection from the root node, and then add more elements separated by slash to elaborate your way deeper into the Xml. You can specify attribute values, but if you specify more than one attribute, you can only use **and** operator.

The next example shows pretty much what you can do:

```csharp
public class PersonB : IXPathSerializable
{
	[MappingXPath("/person[@profession='artist' and @country='UK']
	public string FirstName { get; set; }
	[MappingXPath("/person[@profession='artist' and @country='UK']
	public string LastName { get; set; }
}
```

The result will be:

{code:xml}
<person profession="artist" country="UK">
  <personal_data>
    <given_name>Hugh</given_name>
    <family_name>Laurie</family_name>
  </personal_data>
</person>
{code:xml}

These are the current limitations and they might be eased in the future.

## Can I put the value into an attribute?

Yes, you need to specify the attribute name when mapping the field/property to the Xml element.

```csharp
public class PersonC : IXPathSerializable
{
	[MappingXPath("/person[@profession='artist']
	public string FirstName { get; set; }
	[MappingXPath("/person[@profession='artist']
	public string LastName { get; set; }
}
```

And a result:

{code:xml}
<person profession="artist">
  <personal_data given_name="Hugh" family_name="Laurie" />
</person>
{code:xml}

## So how does it work exactly?

Fields and properties are serialized in the order they are declared in the class. If the element with given XPath can be found, it is used. If not, the element is added.

In the example below serializing property **FirstName** creates the element **name**, serializing property **LastName** uses the created element to add a new attribute to it.

```csharp
public class PersonD : IXPathSerializable
{
	[MappingXPath("/name")]
	public string FirstName { get; set; }
	[MappingXPath("/name", attributeName: "last")]
	public string LastName { get; set; }
}
```

Result:

{code:xml}
<name last="Laurie">Hugh</name>
{code:xml}

**Read next**: [Controlling Xml Structure: Advanced Options](Advanced-Xml-Structure.md)