using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Artemkv.Transformation.XmlToObject.Test
{
	#region Primitive Types - Properties

	[MinimalXmlStructure("/String")]
	class WithStringProperty : IXPathSerializable
	{
		[MappingXPath("/String/AnyString")]
		public string AnyString { get; set; }
	}

	[MinimalXmlStructure("/Char")]
	class WithCharProperty : IXPathSerializable
	{
		[MappingXPath("/Char/AnyChar")]
		public char AnyChar { get; set; }
	}

	[MinimalXmlStructure("/Numeric")]
	class WithNumericProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnyByte")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Byte AnyByte { get; set; }

		[MappingXPath("/Numeric/AnySByte")]
		[SerializationFormat("N", NumberStyles.Number)]
		public SByte AnySByte { get; set; }

		[MappingXPath("/Numeric/AnyInt16")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Int16 AnyInt16 { get; set; }

		[MappingXPath("/Numeric/AnyUInt16")]
		[SerializationFormat("N", NumberStyles.Number)]
		public UInt16 AnyUInt16 { get; set; }

		[MappingXPath("/Numeric/AnyInt32")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Int32 AnyInt32 { get; set; }

		[MappingXPath("/Numeric/AnyUInt32")]
		[SerializationFormat("N", NumberStyles.Number)]
		public UInt32 AnyUInt32 { get; set; }

		[MappingXPath("/Numeric/AnyInt64")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Int64 AnyInt64 { get; set; }

		[MappingXPath("/Numeric/AnyUInt64")]
		[SerializationFormat("N", NumberStyles.Number)]
		public UInt64 AnyUInt64 { get; set; }
	}

	[MinimalXmlStructure("/Numeric")]
	class WithFloatPointNumericProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnySingle")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Single AnySingle { get; set; }

		[MappingXPath("/Numeric/AnyDouble")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Double AnyDouble { get; set; }
	}

	[MinimalXmlStructure("/Numeric")]
	class WithDecimalProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnyDecimal")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Decimal AnyDecimal { get; set; }
	}

	[MinimalXmlStructure("/Boolean")]
	class WithBooleanProperty : IXPathSerializable
	{
		[MappingXPath("/Boolean/BoolTrue")]
		public bool BoolTrue { get; set; }

		[MappingXPath("/Boolean/BoolFalse")]
		public bool BoolFalse { get; set; }
	}

	[MinimalXmlStructure("/Date")]
	class WithDateTimeProperty : IXPathSerializable
	{
		[MappingXPath("/Date/AnyDate")]
		[SerializationFormat("yyyy/MM/dd HH:mm:ss:fff", dateTypeStyle: DateTimeStyles.None)]
		public DateTime AnyDateTime { get; set; }
	}

	[MinimalXmlStructure("/Bytes")]
	class WithByteArrayProperty : IXPathSerializable
	{
		[MappingXPath("/Bytes/AnyBase64String")]
		[XmlMandatory]
		[SerializeAsBase64]
		public byte[] AnyByteArray { get; set; }
	}

	[MinimalXmlStructure("/Guid")]
	class WithGuidProperty : IXPathSerializable
	{
		[MappingXPath("/Guid/AnyGuid")]
		[SerializationFormat("B")]
		public Guid AnyGuid { get; set; }
	}

	#endregion

	#region Primitive Types - Fields

	[MinimalXmlStructure("/String")]
	class WithStringField : IXPathSerializable
	{
		[MappingXPath("/String/AnyString")]
		public string AnyString;
	}

	[MinimalXmlStructure("/Char")]
	class WithCharField : IXPathSerializable
	{
		[MappingXPath("/Char/AnyChar")]
		public char AnyChar = '\0';
	}

	[MinimalXmlStructure("/Numeric")]
	class WithNumericField : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnyByte")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Byte AnyByte = 0;

		[MappingXPath("/Numeric/AnySByte")]
		[SerializationFormat("N", NumberStyles.Number)]
		public SByte AnySByte = 0;

		[MappingXPath("/Numeric/AnyInt16")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Int16 AnyInt16 = 0;

		[MappingXPath("/Numeric/AnyUInt16")]
		[SerializationFormat("N", NumberStyles.Number)]
		public UInt16 AnyUInt16 = 0;

		[MappingXPath("/Numeric/AnyInt32")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Int32 AnyInt32 = 0;

		[MappingXPath("/Numeric/AnyUInt32")]
		[SerializationFormat("N", NumberStyles.Number)]
		public UInt32 AnyUInt32 = 0;

		[MappingXPath("/Numeric/AnyInt64")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Int64 AnyInt64 = 0;

		[MappingXPath("/Numeric/AnyUInt64")]
		[SerializationFormat("N", NumberStyles.Number)]
		public UInt64 AnyUInt64 = 0;
	}

	[MinimalXmlStructure("/Numeric")]
	class WithFloatPointNumericField : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnySingle")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Single AnySingle = 0.00f;

		[MappingXPath("/Numeric/AnyDouble")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Double AnyDouble = 0.00;
	}

	[MinimalXmlStructure("/Numeric")]
	class WithDecimalField : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnyDecimal")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Decimal AnyDecimal = 0.00m;
	}

	[MinimalXmlStructure("/Boolean")]
	class WithBooleanField : IXPathSerializable
	{
		[MappingXPath("/Boolean/BoolTrue")]
		public bool BoolTrue = false;

		[MappingXPath("/Boolean/BoolFalse")]
		public bool BoolFalse = false;
	}

	[MinimalXmlStructure("/Date")]
	class WithDateTimeField : IXPathSerializable
	{
		[MappingXPath("/Date/AnyDate")]
		[SerializationFormat("yyyy/MM/dd HH:mm:ss:fff", dateTypeStyle: DateTimeStyles.None)]
		public DateTime AnyDateTime = DateTime.MinValue;
	}

	[MinimalXmlStructure("/Bytes")]
	class WithByteArrayField : IXPathSerializable
	{
		[MappingXPath("/Bytes/AnyBase64String")]
		[XmlMandatory]
		[SerializeAsBase64]
		public byte[] AnyByteArray = null;
	}

	#endregion

	#region Nullable Types

	[MinimalXmlStructure("/Char")]
	class WithCharNullableProperty : IXPathSerializable
	{
		[MappingXPath("/Char/AnyChar")]
		[XmlMandatory]
		public char? AnyChar { get; set; }
	}

	[MinimalXmlStructure("/Numeric")]
	class WithNumericNullableProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnyByte")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public Byte? AnyByte { get; set; }

		[MappingXPath("/Numeric/AnySByte")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public SByte? AnySByte { get; set; }

		[MappingXPath("/Numeric/AnyInt16")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public Int16? AnyInt16 { get; set; }

		[MappingXPath("/Numeric/AnyUInt16")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public UInt16? AnyUInt16 { get; set; }

		[MappingXPath("/Numeric/AnyInt32")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public Int32? AnyInt32 { get; set; }

		[MappingXPath("/Numeric/AnyUInt32")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public UInt32? AnyUInt32 { get; set; }

		[MappingXPath("/Numeric/AnyInt64")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public Int64? AnyInt64 { get; set; }

		[MappingXPath("/Numeric/AnyUInt64")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public UInt64? AnyUInt64 { get; set; }
	}

	[MinimalXmlStructure("/Numeric")]
	class WithFloatPointNumericNullableProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnySingle")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public Single? AnySingle { get; set; }

		[MappingXPath("/Numeric/AnyDouble")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public Double? AnyDouble { get; set; }
	}

	[MinimalXmlStructure("/Numeric")]
	class WithDecimalNullableProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnyDecimal")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public Decimal? AnyDecimal { get; set; }
	}

	[MinimalXmlStructure("/Boolean")]
	class WithBooleanNullableProperty : IXPathSerializable
	{
		[MappingXPath("/Boolean/BoolTrue")]
		[XmlMandatory]
		public bool? BoolTrue { get; set; }

		[MappingXPath("/Boolean/BoolFalse")]
		[XmlMandatory]
		public bool? BoolFalse { get; set; }
	}

	[MinimalXmlStructure("/Date")]
	class WithDateTimeNullableProperty : IXPathSerializable
	{
		[MappingXPath("/Date/AnyDate")]
		[SerializationFormat("yyyy/MM/dd HH:mm:ss:fff", dateTypeStyle: DateTimeStyles.None)]
		[XmlMandatory]
		public DateTime? AnyDateTime { get; set; }
	}

	#endregion Nullable Types

	#region Mandatory Xml Properties

	class WithStringMandatoryProperty : IXPathSerializable
	{
		[MappingXPath("/String/AnyString")]
		[XmlMandatory]
		public string AnyString { get; set; }
	}

	class WithNumericMandatoryProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnyInt16")]
		[SerializationFormat("N", NumberStyles.Number)]
		[XmlMandatory]
		public Int16 AnyInt16 { get; set; }
	}

	class WithStringMandatoryAttribute : IXPathSerializable
	{
		[MappingXPath("/String/AnyString", attributeName: "value")]
		[XmlMandatory]
		public string AnyString { get; set; }
	}

	#endregion

	#region Basic Types - Array Properties

	[MinimalXmlStructure("/String")]
	class WithStringArrayProperty : IXPathSerializable
	{
		[MappingXPath("/String/AnyStrings")]
		[ArrayItemElementName("AnyString")]
		public string[] AnyStrings { get; set; }
	}

	[MinimalXmlStructure("/Char")]
	class WithCharArrayProperty : IXPathSerializable
	{
		[MappingXPath("/Char/AnyChars")]
		[ArrayItemElementName("AnyChar")]
		public char[] AnyChars { get; set; }
	}

	[MinimalXmlStructure("/Numeric")]
	class WithNumericArrayProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnyBytes")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnyByte")]
		public Byte[] AnyBytes { get; set; }

		[MappingXPath("/Numeric/AnySBytes")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnySByte")]
		public SByte[] AnySBytes { get; set; }

		[MappingXPath("/Numeric/AnyInts16")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnyInt16")]
		public Int16[] AnyInts16 { get; set; }

		[MappingXPath("/Numeric/AnyUInts16")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnyUInt16")]
		public UInt16[] AnyUInts16 { get; set; }

		[MappingXPath("/Numeric/AnyInts32")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnyInt32")]
		public Int32[] AnyInts32 { get; set; }

		[MappingXPath("/Numeric/AnyUInts32")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnyUInt32")]
		public UInt32[] AnyUInts32 { get; set; }

		[MappingXPath("/Numeric/AnyInts64")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnyInt64")]
		public Int64[] AnyInts64 { get; set; }

		[MappingXPath("/Numeric/AnyUInts64")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnyUInt64")]
		public UInt64[] AnyUInts64 { get; set; }
	}

	[MinimalXmlStructure("/Numeric")]
	class WithFloatPointNumericArrayProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnySingles")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnySingle")]
		public Single[] AnySingles { get; set; }

		[MappingXPath("/Numeric/AnyDoubles")]
		[SerializationFormat("N", NumberStyles.Number)]
		[ArrayItemElementName("AnyDouble")]
		public Double[] AnyDoubles { get; set; }
	}

	[MinimalXmlStructure("/Numeric")]
	class WithDecimalArrayProperty : IXPathSerializable
	{
		[MappingXPath("/Numeric/AnyDecimals")]
		[SerializationFormat("N", NumberStyles.Number)]
		public Decimal[] AnyDecimals { get; set; }
	}

	[MinimalXmlStructure("/Boolean")]
	class WithBooleanArrayProperty : IXPathSerializable
	{
		[MappingXPath("/Boolean/Bools")]
		public bool[] AnyBools { get; set; }
	}

	[MinimalXmlStructure("/Date")]
	class WithDateTimeArrayProperty : IXPathSerializable
	{
		[MappingXPath("/Date/AnyDates")]
		[SerializationFormat("yyyy/MM/dd HH:mm:ss:fff", dateTypeStyle: DateTimeStyles.None)]
		public DateTime[] AnyDates { get; set; }
	}

	public class WithArrayPropertyWithNulls : IXPathSerializable
	{
		private string[] _myArray = new[] { "aaa", null, "bbb" };

		public string[] MyArray
		{
			get
			{
				return _myArray;
			}
			set
			{
				_myArray = value;
			}
		}
	}

	class WithStringArrayMandatoryAndEmptyProperty : IXPathSerializable
	{
		private string[] _myArray = new string[0];

		[XmlMandatory]
		public string[] MyArray
		{
			get
			{
				return _myArray;
			}
			set
			{
				_myArray = value;
			}
		}
	}

	#endregion

	#region Inherited Members

	class WithStringPropertyHeir : WithStringProperty
	{
	}

	class WithStringFieldHeir : WithStringField
	{
	}

	#endregion

	#region Mapping By Convention

	class WithStringPropertyByConvention : IXPathSerializable
	{
		public string AnyString { get; set; }
	}

	class WithStringFieldByConvention : IXPathSerializable
	{
		public string AnyString;
	}

	class WithNumericArrayByConvention : IXPathSerializable
	{
		public Byte[] AnyBytes { get; set; }
	}

	#endregion

	#region Object Properties

	public class InnerClass : IXPathSerializable
	{
		public string SimpleInnerProperty;
	}

	public class OuterClass : IXPathSerializable
	{
		public string SimpleOuterProperty;

		public InnerClass ObjectProperty;
	}

	public class InnerClassNoCtor : IXPathSerializable
	{
		private InnerClassNoCtor()
		{
		}

		public static InnerClassNoCtor Create()
		{
			return new InnerClassNoCtor();
		}

		public string SimpleInnerProperty;
	}

	public class OuterClassNoCtor : IXPathSerializable
	{
		public string SimpleOuterProperty;

		public InnerClassNoCtor ObjectProperty;
	}

	public class InnerClassCyclic : IXPathSerializable
	{
		public OuterClassCyclic Parent;
	}

	public class OuterClassCyclic : IXPathSerializable
	{
		public InnerClassCyclic Child;
	}

	public class InnerClassAltMapping : IXPathSerializable
	{
		[MappingXPath("/SimpleInnerProperty")]
		public string SimpleInnerProperty;
	}

	public class OuterClassAltMapping : IXPathSerializable
	{
		[MappingXPath("/AlternativeMapping/SimpleOuterProperty")]
		public string SimpleOuterProperty;

		[MappingXPath("/AlternativeMapping/Inner")]
		public InnerClassAltMapping ObjectProperty;
	}

	#endregion

	#region Array Of Serializable

	public class OuterClassWithArrayOfSerializable : IXPathSerializable
	{
		public string SimpleOuterProperty;

		[MappingXPath("/OuterClassWithArrayOfSerializable/InnerObjects")]
		public InnerClass[] InnerObjects;
	}

	public class OuterClassWithArrayOfSerializableNoCtor : IXPathSerializable
	{
		public string SimpleOuterProperty;
		public InnerClassNoCtor[] InnerObjects;
	}

	#endregion

	#region With ValueConverter

	class WithBooleanAsYesNoField : IXPathSerializable
	{
		[MappingXPath("/Boolean/BoolTrue")]
		[ValueConvertor(typeof(BooleanToYesNoValueConverter))]
		public bool BoolTrue;

		[MappingXPath("/Boolean/BoolFalse")]
		[ValueConvertor(typeof(BooleanToYesNoValueConverter))]
		public bool BoolFalse;
	}

	class WithBooleanAsYesNoArrayField : IXPathSerializable
	{
		[MappingXPath("/Boolean")]
		[ValueConvertor(typeof(BooleanToYesNoValueConverter))]
		public bool[] ArrayOfBoolean;
	}

	#endregion

	#region Enum Type

	public enum Seasons
	{
		Spring = 1,
		Summer,
		Autumn,
		Winter
	}

	class WithEnumPropertyAsString : IXPathSerializable
	{
		[MappingXPath("/Seasons/Season")]
		[ValueConvertor(typeof(EnumStringValueConverter))]
		public Seasons Season { get; set; }
	}

	class WithEnumPropertyAsNumber : IXPathSerializable
	{
		[MappingXPath("/Seasons/Season")]
		[ValueConvertor(typeof(EnumNumberValueConverter))]
		public Seasons Season { get; set; }
	}

	class WithEnumPropertyNullableAsString : IXPathSerializable
	{
		[MappingXPath("/Seasons/Season")]
		[ValueConvertor(typeof(EnumStringValueConverter))]
		[XmlMandatory]
		public Seasons? Season { get; set; }
	}

	class WithEnumPropertyNullableAsNumber : IXPathSerializable
	{
		[MappingXPath("/Seasons/Season")]
		[ValueConvertor(typeof(EnumNumberValueConverter))]
		[XmlMandatory]
		public Seasons? Season { get; set; }
	}

	#endregion

	#region Flags Type

	[Flags]
	public enum PhoneFeatures
	{
		TouchScreen = 1,
		Camera = 2,
		GPRS = 4,
		Wifi = 8
	}

	class WithFlagsPropertyAsString : IXPathSerializable
	{
		[MappingXPath("/Phone/Features")]
		[ValueConvertor(typeof(EnumStringValueConverter))]
		public PhoneFeatures Features { get; set; }
	}

	class WithFlagsPropertyAsNumber : IXPathSerializable
	{
		[MappingXPath("/Phone/Features")]
		[ValueConvertor(typeof(EnumNumberValueConverter))]
		public PhoneFeatures Features { get; set; }
	}

	class WithFlagsPropertyNullableAsString : IXPathSerializable
	{
		[MappingXPath("/Phone/Features")]
		[ValueConvertor(typeof(EnumStringValueConverter))]
		[XmlMandatory]
		public PhoneFeatures? Features { get; set; }
	}

	class WithFlagsPropertyNullableAsNumber : IXPathSerializable
	{
		[MappingXPath("/Phone/Features")]
		[ValueConvertor(typeof(EnumNumberValueConverter))]
		[XmlMandatory]
		public PhoneFeatures? Features { get; set; }
	}

	#endregion

	#region Special Cases

	class With2GuidPropertiesSameElement : IXPathSerializable
	{
		[MappingXPath("/Guid")]
		[SerializationFormat("B")]
		public Guid One { get; set; }

		[MappingXPath("/Guid")]
		[SerializationFormat("B")]
		public Guid Two { get; set; }
	}

	class WithStringAsElementAndAsAttribute : IXPathSerializable
	{
		[MappingXPath("/WithStringAsElementAndAsAttribute/MyElement")]
		public String InElement;

		[MappingXPath("/WithStringAsElementAndAsAttribute/MyElement", "MyAttribute")]
		public String InAttribute;
	}

	[NamespacePrefix("soap", "http://www.w3.org/2001/12/soap-encoding")]
	[MinimalXmlStructure("/soap:Envelope[@soap:encodingStyle='http://www.w3.org/2001/12/soap-encoding']")]
	class WithUrlInAttribute : IXPathSerializable
	{
	}

	#endregion

	#region Not Serialized

	class SelectiveSerilalizationField : IXPathSerializable
	{
		[MappingXPath("/Boolean/BoolTrue")]
		public bool BoolTrue = false;

		[MappingXPath("/Boolean/BoolFalse")]
		[NotForSerialization]
		public bool BoolFalse = false;
	}

	#endregion

	#region Complex Types

	[MinimalXmlStructure("/WithListProperty")]
	class WithStrongTypedListProperty : IXPathSerializable
	{
		[MappingXPath("/WithListProperty/List"), SerializeAsXmlFragment]
		[ValueConvertor(typeof(ListOfTValueConverter))]
		public List<InnerClass> MyList { get; set; }
	}

	class WithWeakTypedListProperty : IXPathSerializable
	{
		[MappingXPath("/WithListProperty/List"), SerializeAsXmlFragment]
		[ValueConvertor(typeof(ListOfTValueConverter))]
		public List<object> MyList { get; set; }
	}

	#endregion

	#region Weakly-typed

	[MinimalXmlStructure("Object")]
	class WithObjectProperty : IXPathSerializable
	{
		[MappingXPath("/Object/AnyObject")]
		public object AnyObject { get; set; }
	}

	class WithObjectMandatoryProperty : IXPathSerializable
	{
		[MappingXPath("/Object/AnyObject")]
		[XmlMandatory]
		public object AnyObject { get; set; }
	}

	#endregion

	#region Namespaces

	[NamespacePrefix("one", @"https://xmltoobject.codeplex.com")]
	[NamespacePrefix("two", @"http://stackoverflow.com/")]
	class WithGuidPropertyWithNamespace : IXPathSerializable
	{
		[MappingXPath("/one:Guid[@two:name='with_namespace']/two:AnyGuid", attributeName: "one:value")]
		[SerializationFormat("B")]
		public Guid? AnyGuid { get; set; }
	}

	#endregion

	#region Versions

	class WithVersions : IXPathSerializable
	{
		[ValueConvertor(typeof(EnumStringValueConverter)), XmlMandatory]
		public ConsoleColor? Color1 { get; set; }

		[ValueConvertor(typeof(EnumStringValueConverter)), XmlMandatory, SerializedFromVersion(2)]
		public ConsoleColor? Color2 { get; set; }
	}

	class WithFilterInternal : IXPathSerializable
	{
		[XmlMandatory]
		public string StringToSerialize { get; set; }
		[XmlMandatory]
		public string StringToSkip { get; set; }
	}

	class WithFilter : IXPathSerializable
	{
		[ValueConvertor(typeof(EnumStringValueConverter)), XmlMandatory]
		public PhoneFeatures? PropertyToSerialize { get; set; }
		[ValueConvertor(typeof(EnumStringValueConverter)), XmlMandatory]
		public PhoneFeatures? PropertyToSkip { get; set; }
		[ValueConvertor(typeof(EnumStringValueConverter)), XmlMandatory]
		public PhoneFeatures? FieldToSerialize;
		[ValueConvertor(typeof(EnumStringValueConverter)), XmlMandatory]
		public PhoneFeatures? FieldToSkip;
		[XmlMandatory]
		public WithFilterInternal InnerObject { get; set; }
	}

	#endregion

	#region Complex Properties - Within Parent

	public class Address : IXPathSerializable
	{
		[MappingXPath("/address/name")]
		[MappingXPathRelativeToParent("/name")]
		public string Name { get; set; }

		[MappingXPath("/address/street")]
		[MappingXPathRelativeToParent("/street")]
		public string Street { get; set; }

		[MappingXPath("/address/city")]
		[MappingXPathRelativeToParent("/city")]
		public string City { get; set; }

		[MappingXPath("/address/state")]
		[MappingXPathRelativeToParent("/state")]
		public string State { get; set; }

		[MappingXPath("/address/zip")]
		[MappingXPathRelativeToParent("/zip")]
		public string ZipCode { get; set; }

		[MappingXPath("/address", attributeName: "country")]
		[MappingXPathRelativeToParent("", attributeName: "country")]
		public string Coutry { get; set; }
	}

	public class PurchaseOrder : IXPathSerializable
	{
		[MappingXPath("/purchaseOrder", attributeName: "orderDate"), SerializationFormat("yyyy-MM-dd", dateTypeStyle: DateTimeStyles.AssumeUniversal)]
		public DateTime OrderDate { get; set; }

		[MappingXPath("/purchaseOrder/shipTo")]
		public Address ShipTo { get; set; }

		[MappingXPath("/purchaseOrder/billTo")]
		public Address BillTo { get; set; }
	}

	#endregion

	#region Structs

	struct SerializableStructInner : IXPathSerializable
	{
		public string AnyStringFieldInner { get; set; }
	}

	struct SerializableStruct : IXPathSerializable
	{
		public string AnyStringField;

		public string AnyStringProperty { get; set; }

		public SerializableStructInner InnerStruct { get; set; }

		public SerializableStruct[] StructArray { get; set; }

		[ValueConvertor(typeof(ListOfTValueConverter)), SerializeAsXmlFragment]
		public List<SerializableStruct> StructList { get; set; }
	}

	#endregion Structs
}
