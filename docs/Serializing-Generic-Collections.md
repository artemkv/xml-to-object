# Serializing Generic Collections

## How are generic collections serialized?

To serialize a generic collection, you have to apply a custom value converter. The library includes a **ListOfTValueConverter** which can be used to serialize fields/properties of type List<T>. You can write your own converters to support more types.

## How to serialize field/property of type List<T>?

Apply **ListOfTValueConverter** to your field/property.

```csharp
public class Person : IXPathSerializable
{
	public string FirstName { get; set; }
	[ValueConvertor(typeof(ListOfTValueConverter)), SerializeAsXmlFragment]
	public List<string> MiddleNames { get; set; }
	public string LastName { get; set; }
}

public class Group : IXPathSerializable
{
	[ValueConvertor(typeof(ListOfTValueConverter)), SerializeAsXmlFragment]
	public List<Person> People { get; set; }
}
```

```csharp
Group group = new Group()
{
	People = new List<Person>()
	{
		new Person()
		{
			FirstName = "Jose",
			MiddleNames = new List<string> { "Antonio", "Alvarez", "Lopez" },
			LastName = "Rodriguez"
		},
		new Person()
		{
			FirstName = "Rasmus",
			MiddleNames = new List<string> { "Christian" },
			LastName = "Nørgaard"
		}
	}
};

string xml = group.ToXml();
```

```xml
<Group>
  <People>
    <Person>
      <FirstName>Jose</FirstName>
      <MiddleNames>
        <Item>Antonio</Item>
        <Item>Alvarez</Item>
        <Item>Lopez</Item>
      </MiddleNames>
      <LastName>Rodriguez</LastName>
    </Person>
    <Person>
      <FirstName>Rasmus</FirstName>
      <MiddleNames>
        <Item>Christian</Item>
      </MiddleNames>
      <LastName>Nørgaard</LastName>
    </Person>
  </People>
</Group>
```

Note that values of primitive types are packed into element 'Item'. This behavior cannot be changed.

Also, notice that we used **SerializeAsXmlFragmentAttribute** to make sure that the converted value is inserted as a part of the Xml document.

**Read next**: [Strong Type Support](Strong-Type-Support.md)
