# Type Conversion: IValueConvertor

## How can I override the way the values are converted to Xml and back?

You can provide your own way to convert class member values to a string and back. In order to do so, you have to implement your own **value converter** and apply it to the field/property using **ValueConvertorAttribute**. Value converter is the class that implements **IValueConvertor** interface and is responsible for this kind of conversion.

**IValueConvertor** defines 4 methods:

object **Convert**(string value, SerializationContext context)
Performs the conversion of a string to a value of member type.

bool **TryConvert**(string value, SerializationContext context, out object convertedValue)
Tries to perform the conversion of a string to a value of member type.

string **ConvertBack**(object value, SerializationContext context)
Performs the backward conversion of the value of member type to a string.

bool **TryConvertBack**(object value, SerializationContext context, out string convertedValue)
Tries to perform the backward conversion of the value of member type to a string.

**Convert** and **ConvertBack** should throw **SerializationException** when conversion fails. **TryConvert** and **TryConvertBack** in the same situation should not throw an exception.

You can inherit from **ValueConverterBase** class that provides implementation for **Convert** and **ConvertBack**, so you only have to implement **TryConvert** and **TryConvertBack**.


As an example, let's create a converter that serializes boolean values as "Yes"/"No" instead of "True"/"False".

```csharp
public class BooleanToYesNoValueConverter : ValueConverterBase
{
	public override bool TryConvert(string value, SerializationContext context, out object convertedValue)
	{
		if (value == "Yes")
		{
			convertedValue = true;
			return true; // Convertion succeed
		}
		if (value == "No")
		{
			convertedValue = false;
			return true; // Convertion succeed
		}
		convertedValue = null;
		return false; // Convertion failed
	}

	public override bool TryConvertBack(object value, SerializationContext context, out string convertedValue)
	{
		if (value == null || !(value is bool))
		{
			convertedValue = null;
			return false; // Convertion failed
		}

		bool valueAsBoolean = (bool)value;
		convertedValue = valueAsBoolean ? "Yes" : "No";
		return true; // Convertion succeed
	}
}
```

Please note how the input parameters are handled: if the method cannot convert the value, it returns false.

Now we can create a serializable class that takes advantage of the new convertor. Note that we use **ValueConvertorAttribute** to provide a value converter.

```csharp
class WithBooleanAsYesNoField : IXPathSerializable
{
	[MappingXPath("/Boolean/True")](MappingXPath(__Boolean_True_))
	[ValueConvertor(typeof(BooleanToYesNoValueConverter))](ValueConvertor(typeof(BooleanToYesNoValueConverter)))
	public bool Yes;

	[MappingXPath("/Boolean/False")](MappingXPath(__Boolean_False_))
	[ValueConvertor(typeof(BooleanToYesNoValueConverter))](ValueConvertor(typeof(BooleanToYesNoValueConverter)))
	public bool No;
}
```

And finally, serialization code:

```csharp
var serializable = new WithBooleanAsYesNoField()
{
	Yes= true,
	No= false
};

var xml = serializable.ToXml();
```

The result is:

```xml
<Boolean>
  <True>Yes</True>
  <False>No</False>
</Boolean>
```

## When a value converter can be used?

If the member type is a non-array one, and a **ValueConvertorAttribute** is applied, the value of the field/property is passed to a value converter (for an exception, see below).

If the member type is an array, and a **ValueConvertorAttribute** is applied, the value of the every item in the array is passed to a value converter.

The **ValueConvertorAttribute** is ignored when applied to a field/property or an array item of a type that implements **IXPathSerializable**.

**Read next**: [Enums and Flags](Enums-and-Flags.md)