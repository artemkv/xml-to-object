# Basic Serialization

## How to make a class serializable?

This is easy. Simple mark the class as implementing **IXPathSerializable** interface. This is an empty interface, so you don't have to implement any methods. But you do so, you will be able to use extension methods from **Artemkv.Transformation.XmlToObject** namespace.

```csharp
public class Person : IXPathSerializable
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
}
```

## How do I serialize the class to Xml?

Use **ToXml** method from **Artemkv.Transformation.XmlToObject** namespace.

```csharp
using Artemkv.Transformation.XmlToObject;

...
	Person person = new Person()
	{
		FirstName = "Hugh",
		LastName = "Laurie"
	};

	string xml = person.ToXml();
```

By default, all the public fields and properties are serialized. Since we didn't specify any mapping rules, the Xml structure will mirror the class structure.

```xml
<Person>
  <FirstName>Hugh</FirstName>
  <LastName>Laurie</LastName>
</Person>
```

## How do I deserialize the class from Xml?

Use **LoadFromXml** method from **Artemkv.Transformation.XmlToObject** namespace.

```csharp
using Artemkv.Transformation.XmlToObject;

...

	string xml = @"<Person>
  <FirstName>Hugh</FirstName>
  <LastName>Laurie</LastName>
</Person>";

	Person person = XmlSerialization.LoadFromXml<Person>(xml);
```

Of course, this is the simplest example, and you can do more interesting things.

## What types are supported?

Please refer to the table below to find out information about supported types. Note that all primitive types are supported, and some more complex types might require additional efforts.

| Category | Types | Nullable | Array |
| --- | --- | --- | --- |
| _String_ | System.String | System.String? | System.String[] |
| _Char_ | System.Char | System.Char? | System.Char[] |
| _Integer_ | System.Byte, System.SByte, System.Int16, System.UInt16, System.Int32, System.UInt32, System.Int64, System.UInt64 | System.Byte?, System.SByte?, System.Int16?, System.UInt16?, System.Int32?, System.UInt32?, System.Int64?, System.UInt64? | System.Byte[], System.SByte[], System.Int16[], System.UInt16[], System.Int32[], System.UInt32[], System.Int64[], System.UInt64[] |
| _Float_ | System.Single, System.Double, System.Decimal | System.Single?, System.Double?, System.Decimal? | System.Single[], System.Double[], System.Decimal[] |
| _Boolean_ | System.Boolean | System.Boolean? | System.Boolean[] |
| _Date and time_ | System.DateTime | System.DateTime? | System.DateTime[] |
| _Guid_ | System.Guid | System.Guid? | System.Guid[] |
| _Base64 encoded string_ | System.Byte[] using SerializeAsBase64Attribute | N/A | N/A |
| _Enum_ | Using EnumStringValueConverter or EnumNumberValueConverter | Using EnumStringValueConverter or EnumNumberValueConverter | Using EnumStringValueConverter or EnumNumberValueConverter |
| _Flags_ | Using EnumStringValueConverter or EnumNumberValueConverter | Using EnumStringValueConverter or EnumNumberValueConverter | Using EnumStringValueConverter or EnumNumberValueConverter |
| _Object_ | IXPathSerializable | IXPathSerializable | IXPathSerializable[] |
| _Other_ | Using ValueConverter | Using ValueConverter | Using ValueConverter |

## What is default serialization behavior?

By default, serialization uses class/member names to create an Xml. As a result, you will get an Xml that mirrors the class structure.

For example, this class:

```csharp
public class Address : IXPathSerializable
{
	public string Street { get; set; }
	public string City { get; set; }
	public string ZipCode { get; set; }
	public string Country { get; set; }
}

public class Employee : IXPathSerializable
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime DateOfBirth { get; set; }
	public bool IsMale { get; set; }
	public Decimal Salary { get; set; }
	public Address HomeAddress { get; set; }
	public string[]() Skills { get; set; }
}
```

Will be serialized to:

```xml
<Employee>
  <FirstName>Hugh</FirstName>
  <LastName>Laurie</LastName>
  <DateOfBirth>1959-06-11T00:00:00.0000000</DateOfBirth>
  <IsMale>True</IsMale>
  <Salary>5000000.00</Salary>
  <HomeAddress>
    <Address>
      <Street>Hamilton Hodell</Street>
      <City>London</City>
      <ZipCode>W1W 8SR</ZipCode>
      <Country>UK</Country>
    </Address>
  </HomeAddress>
  <Skills>
    <Item>actor</Item>
    <Item>voice artist</Item>
    <Item>comedian</Item>
    <Item>writer</Item>
    <Item>musician</Item>
    <Item>recording artist</Item>
    <Item>director</Item>
  </Skills>
</Employee>
```

**Read next**: [Controlling Xml Structure: Elements and Attributes](Xml-Structure.md)
