# Type Conversion: Formats and Format Providers

## How do I control how numeric values are converted to string and back?

In .Net, when you convert the number to string, you allowed to supply a format and a format provider.
For example:

```csharp
Int32 anyNumber = 123456789;

Console.WriteLine(anyNumber.ToString("N", CultureInfo.GetCultureInfo("en-US").NumberFormat)); 
// Prints '123,456,789.00'

Console.WriteLine(anyNumber.ToString("C", CultureInfo.GetCultureInfo("en-US").NumberFormat));
// Prints '$123,456,789.00'
```

Similarly, when you re-create a number from string, you can provide a number style and a format provider.

```csharp
Int32.Parse("123,456,789.00", NumberStyles.Number, CultureInfo.GetCultureInfo("en-US").NumberFormat);
Int32.Parse("$123,456,789.00", NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US").NumberFormat);
```

Evident lack of symmetry is something that is on Microsoft's conscience. Good new is that what you can do in pure .Net is exactly the same as what you can do using **XmlToObject** library: you specify a number format and a style using **SerializationFormatAttribute** and you provide a format provider when calling **ToXml** or **LoadFromXml**.

```csharp
class WithNumericProperty : IXPathSerializable
{
	[MappingXPath("/Numeric/AnyInt32")]
	[SerializationFormat("N", NumberStyles.Number)]
	public Int32 AnyInt32 { get; set; }
}
```

```csharp
var serializable = new WithNumericProperty()
{
	AnyInt32 = Int32.MaxValue
};

string xml = serializable.ToXml(provider: CultureInfo.GetCultureInfo("en-US").NumberFormat);

serializable = XmlSerialization.LoadFromXml<WithNumericProperty>(xml, provider: CultureInfo.GetCultureInfo("en-US").NumberFormat);
```

## How do I control how DateTime values are converted to string and back?

Very similarly to numbers, .Net allows you to submit a format and a format provider for a date conversion.

```csharp
DateTime now = DateTime.Today;

Console.WriteLine(now.ToString("yyyy/MM/dd HH:mm:ss:fff", CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
// Prints '2012/10/24 00:45:02:615'
```

And, being consistent, it offers you the way to indicate a format, a format provider and a datetime style when parsing the string back:

```csharp
DateTime.ParseExact("2012/10/24 00:45:02:615", "yyyy/MM/dd HH:mm:ss:fff", CultureInfo.GetCultureInfo("en-US").DateTimeFormat, DateTimeStyles.None);
```

Again, **XmlToObject** does not get in the way, and simply allows you to supply the values you need. And again, you specify a format and a style using **SerializationFormatAttribute** and you provide a format provider when calling **ToXml** or **LoadFromXml**.

```csharp
class WithDateTimeProperty : IXPathSerializable
{
	[MappingXPath("/Date/AnyDate")]
	[SerializationFormat("yyyy/MM/dd HH:mm:ss:fff", dateTypeStyle: DateTimeStyles.None)]
	public DateTime AnyDateTime { get; set; }
}
```

```csharp
var serializable = new WithDateTimeProperty()
{
	AnyDateTime = new DateTime(2012, 10, 13, 16, 30, 55, 123)
};

string xml = serializable.ToXml(provider: CultureInfo.GetCultureInfo("en-US").DateTimeFormat);

serializable = XmlSerialization.LoadFromXml<WithDateTimeProperty>(xml, provider: CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
```

## How do I control how guids are converted to string and back?

Very similarly to numbers, .Net allows you to submit a format and a format provider for a guid conversion.

```csharp
Guid unique = Guid.NewGuid();

Console.WriteLine(unique.ToString("B", CultureInfo.InvariantCulture));
// Prints '{695df639-41d3-4c8f-aa11-2e36d2954531}'
```

And you allowed to specify a format when parsing the string back:

```csharp
Guid.ParseExact("{695df639-41d3-4c8f-aa11-2e36d2954531}", "B");
```

The same as with numbers, **XmlToObject** does not get in the way, and simply allows you to supply the value you need. And again, you specify a format using **SerializationFormatAttribute** and you provide a format provider when calling **ToXml** or **LoadFromXml**.

```csharp
class WithGuidProperty : IXPathSerializable
{
	[MappingXPath("/Guid/AnyGuid")]
	[SerializationFormat("B")]
	public Guid AnyGuid { get; set; }
}
```

```csharp
var serializable = new WithGuidProperty()
{
	AnyGuid = Guid.NewGuid()
};

var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture);

serializable = XmlSerialization.LoadFromXml<WithGuidProperty>(xml, provider: CultureInfo.InvariantCulture);
```

## Which format is used if I don't specify the format?

By default, serialization favors styles that make roundtrips possible. NumberStyles.Any is used for numbers and DateTimeStyles.RoundtripKind is used for the DateTime values.
As for the format string, it depends on the value type. Again, serialization picks the one that makes the roundtrip possible: "D" for all integer types, "R" for Single and Double, "F" for Decimal, "o" for DateTime and "B" for Guid.
If you want to use .Net default values for styles (NumberStyles.None and DateTimeStyles.None) and empty string for the format, you can simply apply **SerializationFormatAttribute** without any arguments.

**Read next**: [Type Conversion: IValueConvertor]