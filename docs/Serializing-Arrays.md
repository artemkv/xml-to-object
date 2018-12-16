# Serializing Arrays

## How are arrays serialized?

Every item of an array is converted to Xml and added as a child element to the element that your field/property is mapped to.

If you used **MappingXPathAttribute** to define mapping, the parent element will be defined by elementXPath parameter. Note that in this case attributeName parameter is ignored (You would not serialize the whole array as an Xml attribute value).

If you don't specify **MappingXPathAttribute**, then name of the parent element is the name of the field/property.

For example:

```csharp
public class Person : IXPathSerializable
{
	public string FirstName { get; set; }
	public string[] MiddleNames { get; set; }
	public string LastName { get; set; }
}
```

```csharp
Person person = new Person()
{
	FirstName = "Jose",
	MiddleNames = new [] { "Antonio", "Alvarez", "Lopez" },
	LastName = "Rodriguez"
};

string xml = person.ToXml();
```

```xml
<Person>
  <FirstName>Jose</FirstName>
  <MiddleNames>
    <Item>Antonio</Item>
    <Item>Alvarez</Item>
    <Item>Lopez</Item>
  </MiddleNames>
  <LastName>Rodriguez</LastName>
</Person>
```

Note that since we have an array of strings, every value in the array has to packed into an element named **'Item'** before added to the element **MiddleNames** that the property **MiddleNames** is mapped to.

## Can I control the name of the element created for an array item?

Yes you can. For that, apply **ArrayItemElementNameAttribute** to your field/property.

```csharp
public class Person : IXPathSerializable
{
	public string FirstName { get; set; }
	[ArrayItemElementName("MiddleName")]
	public string[] MiddleNames { get; set; }
	public string LastName { get; set; }
}
```

This time result will be:

```xml
<Person>
  <FirstName>Jose</FirstName>
  <MiddleNames>
    <MiddleName>Antonio</MiddleName>
    <MiddleName>Alvarez</MiddleName>
    <MiddleName>Lopez</MiddleName>
  </MiddleNames>
  <LastName>Rodriguez</LastName>
</Person>
```

## How are the arrays of complex types serialized?

If array item has the type that implements **IXPathSerializable**, it is serialized normally and added to the parent Xml element that your field/property is mapped to. This time there is no need to wrap the result into additional Xml element.

```csharp
public class Group : IXPathSerializable
{
	public Person[] People { get; set; }
}
```

```csharp
Group group = new Group()
{
	People = new Person[] 
	{
		new Person()
		{
			FirstName = "Jose",
			MiddleNames = new string[] { "Antonio", "Alvarez", "Lopez" },
			LastName = "Rodriguez"
		},
		new Person()
		{
			FirstName = "Rasmus",
			MiddleNames = new string[] { "Christian" },
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
        <MiddleName>Antonio</MiddleName>
        <MiddleName>Alvarez</MiddleName>
        <MiddleName>Lopez</MiddleName>
      </MiddleNames>
      <LastName>Rodriguez</LastName>
    </Person>
    <Person>
      <FirstName>Rasmus</FirstName>
      <MiddleNames>
        <MiddleName>Christian</MiddleName>
      </MiddleNames>
      <LastName>Nørgaard</LastName>
    </Person>
  </People>
</Group>
```

Note that elements **Person** are added as is to the **People** Xml Element that the property **People** is mapped to.

## What happens if I apply a value converter to an array field/property?

In this case value converter will be used to convert the value of every item in an array.

For example, you can apply your custom **BooleanToYesNoValueConverter** to the array of boolean values.

```csharp
class WithBooleanAsYesNoArrayField : IXPathSerializable
{
	[MappingXPath("/Boolean")]
	[ValueConvertor(typeof(BooleanToYesNoValueConverter))]
	public bool[] ArrayOfBoolean;
}
```

When serialized, every boolean value will be converted to 'Yes' or 'No'.

```csharp
var serializable = new WithBooleanAsYesNoArrayField()
{
	ArrayOfBoolean = new bool[] { true, false, true }
};

var xml = serializable.ToXml();
```

```xml
<Boolean>
  <Item>Yes</Item>
  <Item>No</Item>
  <Item>Yes</Item>
</Boolean>
```

## How can I convert byte array to a Base64 string?

Apply **SerializeAsBase64Attribute** to your field/property of type **byte[]**.

```csharp
public class File : IXPathSerializable
{
	public string FileName { get; set; }
	
	[SerializeAsBase64]
	public byte[] FileContent { get; set; }
}
```

```csharp
File file = new File()
{
	FileName = "readme.txt",
	FileContent = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
};

string xml = file.ToXml();
```

```xml
<File>
  <FileName>readme.txt</FileName>
  <FileContent>AQIDBAUGBwgJCg==</FileContent>
</File>
```

**SerializeAsBase64Attribute** can only be used with the fields/properties of type **byte[]**. If you apply this attribute to the field/property of any other type, it will be ignored.

**Read next**: [Serializing Generic Collections](Serializing-Generic-Collections.md)
