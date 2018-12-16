# Serializing Enums and Flags

## What options do I have when serializing enum values and flags?

You can serialize your enum values as strings or as numbers. In both cases you have to use a value converter. 
There are 2 pre-defined value converters for serializing enums(flags). If you prefer strings, the one you need is **EnumStringValueConverter**, otherwise you should use **EnumNumberValueConverter**. They both work for enums and flags.

## How does it work for enums?

Imagine you have this enum:

```csharp
public enum Seasons
{
	Spring = 1,
	Summer = 2,
	Autumn = 3,
	Winter = 4
}
```

Then with **EnumStringValueConverter** this is what you will get:

```csharp
class WithEnumPropertyAsString : IXPathSerializable
{
	[MappingXPath("/Seasons/Season")]
	[ValueConvertor(typeof(EnumStringValueConverter))]
	public Seasons Season { get; set; }
}
```

```csharp
var serializable = new WithEnumPropertyAsString()
{
	Season = Seasons.Summer
};

var xml = serializable.ToXml();
```

```xml
<Seasons>
  <Season>Summer</Season>
</Seasons>
```

And with **EnumNumberValueConverter** the result will be:

```csharp
class WithEnumPropertyAsNumber : IXPathSerializable
{
	[MappingXPath("/Seasons/Season")]
	[ValueConvertor(typeof(EnumNumberValueConverter))]
	public Seasons Season { get; set; }
}
```

```csharp
var serializable = new WithEnumPropertyAsNumber()
{
	Season = Seasons.Summer
};

var xml = serializable.ToXml();
```

```xml
<Seasons>
  <Season>2</Season>
</Seasons>
```

## How does it work for flags?

Very similarly, for the flags, for example:

```csharp
[Flags](Flags)
public enum PhoneFeatures
{
	TouchScreen = 1,
	Camera = 2,
	GPRS = 4,
	Wifi = 8
}
```

With **EnumStringValueConverter** you will get:

```csharp
class WithFlagsPropertyAsString : IXPathSerializable
{
	[MappingXPath("/Phone/Features")]
	[ValueConvertor(typeof(EnumStringValueConverter))]
	public PhoneFeatures Features { get; set; }
}
```

```csharp
var serializable = new WithFlagsPropertyAsString()
{
	Features = PhoneFeatures.GPRS | PhoneFeatures.TouchScreen
};

var xml = serializable.ToXml();
```

```xml
<Phone>
  <Features>TouchScreen, GPRS</Features>
</Phone>
```

And with **EnumNumberValueConverter**:

```csharp
class WithFlagsPropertyAsNumber : IXPathSerializable
{
	[MappingXPath("/Phone/Features")]
	[ValueConvertor(typeof(EnumNumberValueConverter))]
	public PhoneFeatures Features { get; set; }
}
```

```csharp
var serializable = new WithFlagsPropertyAsNumber()
{
	Features = PhoneFeatures.GPRS | PhoneFeatures.TouchScreen
};

var xml = serializable.ToXml();
```

```xml
<Phone>
  <Features>5</Features>
</Phone>
```

**Read next**: [Serializing Arrays](Serializing-Arrays.md)
