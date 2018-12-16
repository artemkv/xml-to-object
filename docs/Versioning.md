# Versioning

## How do I exclude the field/property from serialization?

By default every public field/property is serialized. But you can mark the field or property that should not be serialized with the **NotForSerializationAttribute**.

```csharp
public class Person : IXPathSerializable
{
	public string FirstName { get; set; }
	public string LastName { get; set; }

	[NotForSerialization](NotForSerialization)
	public string FullName
	{
		get
		{
			return FirstName + " " + LastName;
		}
	}
}
```

## How can I serialize/deserialize object differently depending on the version?

You can apply **SerializedFromVersionAttribute** to your field/property and specify the version number. This will denote the version in which you introduced the field/property.

When you serialize or deserialize an object, you can pass the version number as an optional parameter of **ToXml** and **LoadFromXml** methods and only fields/properties that marked with the same or earlier version number will be serialized.

For example, after we released the first version (number 0) of the class Person we decided to enrich it with the Address property. In the version 1 we thought it is OK to store the address as a string. But soon enough we discovered that it was not a good idea and we had to add the address as an object.

```csharp
public class Address : IXPathSerializable
{
	public string Street { get; set; }
	public string City { get; set; }
	public string ZipCode { get; set; }
	public string Country { get; set; }
}

public class Person : IXPathSerializable
{
	// Default version = 0
	public string FirstName { get; set; }
	// Default version = 0
	public string LastName { get; set; }

	[NotForSerialization](NotForSerialization)
	public string FullName
	{
		get
		{
			return FirstName + " " + LastName;
		}
	}

	[SerializedFromVersion(1)]
	public string Address { get; set; }

	[SerializedFromVersion(2)]
	public Address AddressFull { get; set; }
}
```

Now we have to maintain all 3 version of the object, because some clients might still use the older version of the Xml. So we will explicitly specify the version number when calling **ToXml** or **FromXml** methods.

```csharp
Person person = new Person()
{
	FirstName = "Clovis",
	LastName = "Labossière",
	Address = "37, Avenue Millies Lacroix 95600 EAUBONNE France",
	AddressFull = new Address()
	{
		Street = "37, Avenue Millies Lacroix",
		City = "EAUBONNE",
		ZipCode = "95600",
		Country = "France"					
	}
};
```

When we don't specify the version, the version 0 is assumed:

```csharp
string xml0 = person.ToXml();
```

```xml
<Person>
  <FirstName>Clovis</FirstName>
  <LastName>Labossière</LastName>
</Person>
```

If we want the version 1, we have be explicit:

```csharp
string xml1 = person.ToXml(version: 1);
```

```xml
<Person>
  <FirstName>Clovis</FirstName>
  <LastName>Labossière</LastName>
  <Address>37, Avenue Millies Lacroix 95600 EAUBONNE France</Address>
</Person>
```

The same for the version 2:

```csharp
string xml2 = person.ToXml(version: 2);
```

```xml
<Person>
  <FirstName>Clovis</FirstName>
  <LastName>Labossière</LastName>
  <Address>37, Avenue Millies Lacroix 95600 EAUBONNE France</Address>
  <AddressFull>
    <Address>
      <Street>37, Avenue Millies Lacroix</Street>
      <City>EAUBONNE</City>
      <ZipCode>95600</ZipCode>
      <Country>France</Country>
    </Address>
  </AddressFull>
</Person>
```

When you deserialize the object, you pass the version number the same way:

```csharp
Person deserialized0 = XmlSerialization.LoadFromXml<Person>(xml0);

Person deserialized1 = XmlSerialization.LoadFromXml<Person>(xml1, version: 1);

Person deserialized2 = XmlSerialization.LoadFromXml<Person>(xml2, version: 2);
```

Alternatively, we could keep the new properties optional (as in example), which is, in fact, a best practice. In this case you could always deserialize using the latest version (Int32.MaxValue), and the fields/properties that are not found in the Xml will be simply skipped.

```csharp
Person deserialized2from0 = XmlSerialization.LoadFromXml<Person>(xml0, version: Int32.MaxValue);
```

## How to serialize/deserialize any specific field(s)/property(ies)?

If you only need some specific fields/properties to be serialized or deserialized, use the **MemberFilter**. The object of member filter is no more than a list of expressions that referring to the members you want to serialize.

In the example below, we define a filter that uses property LastName from class Person and property Country from class Address.

```csharp
MemberFilter<Person> filter = new MemberFilter<Person>()
{
	x => x.LastName,
	x => x.AddressFull,
	x => x.AddressFull.Country
};

string xmlFiltered = person.ToXml(filter, version: Int32.MaxValue);
```

This will produce the following Xml:

```xml
<Person>
  <LastName>Labossière</LastName>
  <AddressFull>
    <Address>
      <Country>France</Country>
    </Address>
  </AddressFull>
</Person>
```
