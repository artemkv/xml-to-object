using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using NUnit.Framework;
using System.Globalization;

namespace Artemkv.Transformation.XmlToObject.Test
{
	[TestFixture]
	public class XmlSerialization_Roundtrip_Test
	{
		private static readonly Guid Guid1 = Guid.NewGuid();
		private static readonly Guid Guid2 = Guid.NewGuid();
		private static readonly Guid Guid3 = Guid.NewGuid();

		class RoundtripTest : IXPathSerializable
		{
			public string AnyString = "Some text";
			public char AnyChar = 'S';

			public Byte AnyByteMin = Byte.MinValue;
			public SByte AnySByteMin = SByte.MinValue;
			public Int16 AnyInt16Min = Int16.MinValue;
			public UInt16 AnyUInt16Min = UInt16.MinValue;
			public Int32 AnyInt32Min = Int32.MinValue;
			public UInt32 AnyUInt32Min = UInt32.MinValue;
			public Int64 AnyInt64Min = Int64.MinValue;
			public UInt64 AnyUInt64Min = UInt64.MinValue;
			public Single AnySingleMin = Single.MinValue;
			public Double AnyDoubleMin = Double.MinValue;
			public Decimal AnyDecimalMin = Decimal.MinValue;

			public Byte AnyByteMax = Byte.MaxValue;
			public SByte AnySByteMax = SByte.MaxValue;
			public Int16 AnyInt16Max = Int16.MaxValue;
			public UInt16 AnyUInt16Max = UInt16.MaxValue;
			public Int32 AnyInt32Max = Int32.MaxValue;
			public UInt32 AnyUInt32Max = UInt32.MaxValue;
			public Int64 AnyInt64Max = Int64.MaxValue;
			public UInt64 AnyUInt64Max = UInt64.MaxValue;
			public Single AnySingleMax = Single.MaxValue;
			public Double AnyDoubleMax = Double.MaxValue;
			public Decimal AnyDecimalMax = Decimal.MaxValue;

			public bool BoolTrue = true;
			public bool BoolFalse = false;

			public DateTime AnyDateTimeMin = DateTime.MinValue;
			public DateTime AnyDateTimeMax = DateTime.MaxValue;

			public Guid AnyGuid = Guid1;

			public char? AnyCharNullable = 'S';

			public Byte? AnyByteMinNullable = Byte.MinValue;
			public SByte? AnySByteMinNullable = SByte.MinValue;
			public Int16? AnyInt16MinNullable = Int16.MinValue;
			public UInt16? AnyUInt16MinNullable = UInt16.MinValue;
			public Int32? AnyInt32MinNullable = Int32.MinValue;
			public UInt32? AnyUInt32MinNullable = UInt32.MinValue;
			public Int64? AnyInt64MinNullable = Int64.MinValue;
			public UInt64? AnyUInt64MinNullable = UInt64.MinValue;
			public Single? AnySingleMinNullable = Single.MinValue;
			public Double? AnyDoubleMinNullable = Double.MinValue;
			public Decimal? AnyDecimalMinNullable = Decimal.MinValue;

			public Byte? AnyByteMaxNullable = Byte.MaxValue;
			public SByte? AnySByteMaxNullable = SByte.MaxValue;
			public Int16? AnyInt16MaxNullable = Int16.MaxValue;
			public UInt16? AnyUInt16MaxNullable = UInt16.MaxValue;
			public Int32? AnyInt32MaxNullable = Int32.MaxValue;
			public UInt32? AnyUInt32MaxNullable = UInt32.MaxValue;
			public Int64? AnyInt64MaxNullable = Int64.MaxValue;
			public UInt64? AnyUInt64MaxNullable = UInt64.MaxValue;
			public Single? AnySingleMaxNullable = Single.MaxValue;
			public Double? AnyDoubleMaxNullable = Double.MaxValue;
			public Decimal? AnyDecimalMaxNullable = Decimal.MaxValue;

			public bool? BoolTrueNullable = true;
			public bool? BoolFalseNullable = false;

			public DateTime? AnyDateTimeMinNullable = DateTime.MinValue;
			public DateTime? AnyDateTimeMaxNullable = DateTime.MaxValue;

			public Guid? AnyGuidNullable = Guid1;

			[SerializeAsBase64]
			public byte[] Base64ByteArray = new Byte[] { 1, 2, 3, 4, 5 };

			public string[] AnyStringArray = new string[] { "First", "Second" };
			public char[] AnyCharArray = new char[] { 'S', 'T' };

			public Byte[] AnyByteArray = new Byte[] { Byte.MinValue, Byte.MaxValue };
			public SByte[] AnySByteArray = new SByte[] { SByte.MinValue, SByte.MaxValue };
			public Int16[] AnyInt16Array = new Int16[] { Int16.MinValue, Int16.MaxValue };
			public UInt16[] AnyUInt16Array = new UInt16[] { UInt16.MinValue, UInt16.MaxValue };
			public Int32[] AnyInt32Array = new Int32[] { Int32.MinValue, Int32.MaxValue };
			public UInt32[] AnyUInt32Array = new UInt32[] { UInt32.MinValue, UInt32.MaxValue };
			public Int64[] AnyInt64Array = new Int64[] { Int64.MinValue, Int64.MaxValue };
			public UInt64[] AnyUInt64Array = new UInt64[] { UInt64.MinValue, UInt64.MaxValue };
			public Single[] AnySingleArray = new Single[] { Single.MinValue, Single.MaxValue };
			public Double[] AnyDoubleArray = new Double[] { Double.MinValue, Double.MaxValue };
			public Decimal[] AnyDecimalArray = new Decimal[] { Decimal.MinValue, Decimal.MaxValue };

			public bool[] BoolArray = new bool[] { true, false };

			public DateTime[] AnyDateTimeArray = new DateTime[] { DateTime.MinValue, DateTime.MaxValue };

			public Guid[] AnyGuidArray = new Guid[] { Guid2, Guid3 };

			public InnerClass ObjectProperty = new InnerClass() { SimpleInnerProperty = "Inner" };
			public InnerClass[] InnerObjects = new InnerClass[] 
			{ 
				new InnerClass() { SimpleInnerProperty = "one" }, 
				new InnerClass() { SimpleInnerProperty = "two" } 
			};

			[ValueConvertor(typeof(EnumStringValueConverter))]
			public Seasons Season = Seasons.Spring;
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public PhoneFeatures Features = PhoneFeatures.GPRS | PhoneFeatures.Wifi;

			[ValueConvertor(typeof(EnumStringValueConverter))]
			public Seasons[] SeasonArray = new Seasons[] { Seasons.Spring, Seasons.Summer };
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public PhoneFeatures[] FeaturesArray = new PhoneFeatures[] { PhoneFeatures.GPRS | PhoneFeatures.Wifi, PhoneFeatures.Camera | PhoneFeatures.TouchScreen };

			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public Seasons SeasonAsNumber = Seasons.Spring;
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public PhoneFeatures FeaturesAsNumber = PhoneFeatures.GPRS | PhoneFeatures.Wifi;

			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public Seasons[] SeasonArrayAsNumber = new Seasons[] { Seasons.Spring, Seasons.Summer };
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public PhoneFeatures[] FeaturesArrayAsNumber = new PhoneFeatures[] { PhoneFeatures.GPRS | PhoneFeatures.Wifi, PhoneFeatures.Camera | PhoneFeatures.TouchScreen };

			[ValueConvertor(typeof(EnumStringValueConverter))]
			public Seasons? SeasonNullable = Seasons.Spring;
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public PhoneFeatures? FeaturesNullable = PhoneFeatures.GPRS | PhoneFeatures.Wifi;

			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public Seasons? SeasonAsNumberNullable = Seasons.Spring;
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public PhoneFeatures? FeaturesAsNumberNullable = PhoneFeatures.GPRS | PhoneFeatures.Wifi;
		}

		class RoundtripWeakTest : IXPathSerializable
		{
			public object AnyString = "Some text";
			public object AnyChar = 'S';

			public object AnyByteMin = Byte.MinValue;
			public object AnySByteMin = SByte.MinValue;
			public object AnyInt16Min = Int16.MinValue;
			public object AnyUInt16Min = UInt16.MinValue;
			public object AnyInt32Min = Int32.MinValue;
			public object AnyUInt32Min = UInt32.MinValue;
			public object AnyInt64Min = Int64.MinValue;
			public object AnyUInt64Min = UInt64.MinValue;
			public object AnySingleMin = Single.MinValue;
			public object AnyDoubleMin = Double.MinValue;
			public object AnyDecimalMin = Decimal.MinValue;

			public object AnyByteMax = Byte.MaxValue;
			public object AnySByteMax = SByte.MaxValue;
			public object AnyInt16Max = Int16.MaxValue;
			public object AnyUInt16Max = UInt16.MaxValue;
			public object AnyInt32Max = Int32.MaxValue;
			public object AnyUInt32Max = UInt32.MaxValue;
			public object AnyInt64Max = Int64.MaxValue;
			public object AnyUInt64Max = UInt64.MaxValue;
			public object AnySingleMax = Single.MaxValue;
			public object AnyDoubleMax = Double.MaxValue;
			public object AnyDecimalMax = Decimal.MaxValue;

			public object BoolTrue = true;
			public object BoolFalse = false;

			public object AnyDateTimeMin = DateTime.MinValue;
			public object AnyDateTimeMax = DateTime.MaxValue;

			public object AnyGuid = Guid1;

			public object AnyCharNullable = 'S';

			public object AnyByteMinNullable = Byte.MinValue;
			public object AnySByteMinNullable = SByte.MinValue;
			public object AnyInt16MinNullable = Int16.MinValue;
			public object AnyUInt16MinNullable = UInt16.MinValue;
			public object AnyInt32MinNullable = Int32.MinValue;
			public object AnyUInt32MinNullable = UInt32.MinValue;
			public object AnyInt64MinNullable = Int64.MinValue;
			public object AnyUInt64MinNullable = UInt64.MinValue;
			public object AnySingleMinNullable = Single.MinValue;
			public object AnyDoubleMinNullable = Double.MinValue;
			public object AnyDecimalMinNullable = Decimal.MinValue;

			public object AnyByteMaxNullable = Byte.MaxValue;
			public object AnySByteMaxNullable = SByte.MaxValue;
			public object AnyInt16MaxNullable = Int16.MaxValue;
			public object AnyUInt16MaxNullable = UInt16.MaxValue;
			public object AnyInt32MaxNullable = Int32.MaxValue;
			public object AnyUInt32MaxNullable = UInt32.MaxValue;
			public object AnyInt64MaxNullable = Int64.MaxValue;
			public object AnyUInt64MaxNullable = UInt64.MaxValue;
			public object AnySingleMaxNullable = Single.MaxValue;
			public object AnyDoubleMaxNullable = Double.MaxValue;
			public object AnyDecimalMaxNullable = Decimal.MaxValue;

			public object BoolTrueNullable = true;
			public object BoolFalseNullable = false;

			public object AnyDateTimeMinNullable = DateTime.MinValue;
			public object AnyDateTimeMaxNullable = DateTime.MaxValue;

			public object AnyGuidNullable = Guid1;

			[SerializeAsBase64]
			public object Base64ByteArray = new Byte[] { 1, 2, 3, 4, 5 };

			public object AnyStringArray = new string[] { "First", "Second" };
			public object AnyCharArray = new char[] { 'S', 'T' };

			public object AnyByteArray = new Byte[] { Byte.MinValue, Byte.MaxValue };
			public object AnySByteArray = new SByte[] { SByte.MinValue, SByte.MaxValue };
			public object AnyInt16Array = new Int16[] { Int16.MinValue, Int16.MaxValue };
			public object AnyUInt16Array = new UInt16[] { UInt16.MinValue, UInt16.MaxValue };
			public object AnyInt32Array = new Int32[] { Int32.MinValue, Int32.MaxValue };
			public object AnyUInt32Array = new UInt32[] { UInt32.MinValue, UInt32.MaxValue };
			public object AnyInt64Array = new Int64[] { Int64.MinValue, Int64.MaxValue };
			public object AnyUInt64Array = new UInt64[] { UInt64.MinValue, UInt64.MaxValue };
			public object AnySingleArray = new Single[] { Single.MinValue, Single.MaxValue };
			public object AnyDoubleArray = new Double[] { Double.MinValue, Double.MaxValue };
			public object AnyDecimalArray = new Decimal[] { Decimal.MinValue, Decimal.MaxValue };

			public object BoolArray = new bool[] { true, false };

			public object AnyDateTimeArray = new DateTime[] { DateTime.MinValue, DateTime.MaxValue };

			public object AnyGuidArray = new Guid[] { Guid2, Guid3 };

			public object ObjectProperty = new InnerClass() { SimpleInnerProperty = "Inner" };
			public object InnerObjects = new InnerClass[] 
			{ 
				new InnerClass() { SimpleInnerProperty = "one" }, 
				new InnerClass() { SimpleInnerProperty = "two" } 
			};

			[ValueConvertor(typeof(EnumStringValueConverter))]
			public object Season = Seasons.Spring;
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public object Features = PhoneFeatures.GPRS | PhoneFeatures.Wifi;

			[ValueConvertor(typeof(EnumStringValueConverter))]
			public object SeasonArray = new Seasons[] { Seasons.Spring, Seasons.Summer };
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public object FeaturesArray = new PhoneFeatures[] { PhoneFeatures.GPRS | PhoneFeatures.Wifi, PhoneFeatures.Camera | PhoneFeatures.TouchScreen };

			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public object SeasonAsNumber = Seasons.Spring;
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public object FeaturesAsNumber = PhoneFeatures.GPRS | PhoneFeatures.Wifi;

			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public object SeasonArrayAsNumber = new Seasons[] { Seasons.Spring, Seasons.Summer };
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public object FeaturesArrayAsNumber = new PhoneFeatures[] { PhoneFeatures.GPRS | PhoneFeatures.Wifi, PhoneFeatures.Camera | PhoneFeatures.TouchScreen };

			[ValueConvertor(typeof(EnumStringValueConverter))]
			public object SeasonNullable = Seasons.Spring;
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public object FeaturesNullable = PhoneFeatures.GPRS | PhoneFeatures.Wifi;

			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public object SeasonAsNumberNullable = Seasons.Spring;
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public object FeaturesAsNumberNullable = PhoneFeatures.GPRS | PhoneFeatures.Wifi;
		}

		class RoundtripAttributesTest : IXPathSerializable
		{
			[MappingXPath("/RoundtripAttributes/Values", "AnyString")]
			public string AnyString = "Some text";
			[MappingXPath("/RoundtripAttributes/Values", "AnyChar")]
			public char AnyChar = 'S';

			[MappingXPath("/RoundtripAttributes/Values", "AnyByteMin")]
			public Byte AnyByteMin = Byte.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnySByteMin")]
			public SByte AnySByteMin = SByte.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt16Min")]
			public Int16 AnyInt16Min = Int16.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt16Min")]
			public UInt16 AnyUInt16Min = UInt16.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt32Min")]
			public Int32 AnyInt32Min = Int32.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt32Min")]
			public UInt32 AnyUInt32Min = UInt32.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt64Min")]
			public Int64 AnyInt64Min = Int64.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt64Min")]
			public UInt64 AnyUInt64Min = UInt64.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnySingleMin")]
			public Single AnySingleMin = Single.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyDoubleMin")]
			public Double AnyDoubleMin = Double.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "RoundtripAttributes")]
			public Decimal AnyDecimalMin = Decimal.MinValue;

			[MappingXPath("/RoundtripAttributes/Values", "AnyByteMax")]
			public Byte AnyByteMax = Byte.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnySByteMax")]
			public SByte AnySByteMax = SByte.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt16Max")]
			public Int16 AnyInt16Max = Int16.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt16Max")]
			public UInt16 AnyUInt16Max = UInt16.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt32Max")]
			public Int32 AnyInt32Max = Int32.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt32Max")]
			public UInt32 AnyUInt32Max = UInt32.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt64Max")]
			public Int64 AnyInt64Max = Int64.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt64Max")]
			public UInt64 AnyUInt64Max = UInt64.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnySingleMax")]
			public Single AnySingleMax = Single.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyDoubleMax")]
			public Double AnyDoubleMax = Double.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyDecimalMax")]
			public Decimal AnyDecimalMax = Decimal.MaxValue;

			[MappingXPath("/RoundtripAttributes/Values", "BoolTrue")]
			public bool BoolTrue = true;
			[MappingXPath("/RoundtripAttributes/Values", "BoolFalse")]
			public bool BoolFalse = false;

			[MappingXPath("/RoundtripAttributes/Values", "AnyDateTimeMin")]
			public DateTime AnyDateTimeMin = DateTime.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyDateTimeMax")]
			public DateTime AnyDateTimeMax = DateTime.MaxValue;

			[MappingXPath("/RoundtripAttributes/Values", "AnyGuid")]
			public Guid AnyGuid = Guid1;

			[MappingXPath("/RoundtripAttributes/Values", "AnyCharNullable")]
			public char? AnyCharNullable = 'S';

			[MappingXPath("/RoundtripAttributes/Values", "AnyByteMinNullable")]
			public Byte? AnyByteMinNullable = Byte.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnySByteMinNullable")]
			public SByte? AnySByteMinNullable = SByte.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt16MinNullable")]
			public Int16? AnyInt16MinNullable = Int16.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt16MinNullable")]
			public UInt16? AnyUInt16MinNullable = UInt16.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt32MinNullable")]
			public Int32? AnyInt32MinNullable = Int32.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt32MinNullable")]
			public UInt32? AnyUInt32MinNullable = UInt32.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt64MinNullable")]
			public Int64? AnyInt64MinNullable = Int64.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt64MinNullable")]
			public UInt64? AnyUInt64MinNullable = UInt64.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnySingleMinNullable")]
			public Single? AnySingleMinNullable = Single.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyDoubleMinNullable")]
			public Double? AnyDoubleMinNullable = Double.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyDecimalMinNullable")]
			public Decimal? AnyDecimalMinNullable = Decimal.MinValue;

			[MappingXPath("/RoundtripAttributes/Values", "AnyByteMaxNullable")]
			public Byte? AnyByteMaxNullable = Byte.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnySByteMaxNullable")]
			public SByte? AnySByteMaxNullable = SByte.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt16MaxNullable")]
			public Int16? AnyInt16MaxNullable = Int16.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt16MaxNullable")]
			public UInt16? AnyUInt16MaxNullable = UInt16.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt32MaxNullable")]
			public Int32? AnyInt32MaxNullable = Int32.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt32MaxNullable")]
			public UInt32? AnyUInt32MaxNullable = UInt32.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyInt64MaxNullable")]
			public Int64? AnyInt64MaxNullable = Int64.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyUInt64MaxNullable")]
			public UInt64? AnyUInt64MaxNullable = UInt64.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnySingleMaxNullable")]
			public Single? AnySingleMaxNullable = Single.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyDoubleMaxNullable")]
			public Double? AnyDoubleMaxNullable = Double.MaxValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyDecimalMaxNullable")]
			public Decimal? AnyDecimalMaxNullable = Decimal.MaxValue;

			[MappingXPath("/RoundtripAttributes/Values", "BoolTrueNullable")]
			public bool? BoolTrueNullable = true;
			[MappingXPath("/RoundtripAttributes/Values", "BoolFalseNullable")]
			public bool? BoolFalseNullable = false;

			[MappingXPath("/RoundtripAttributes/Values", "AnyDateTimeMinNullable")]
			public DateTime? AnyDateTimeMinNullable = DateTime.MinValue;
			[MappingXPath("/RoundtripAttributes/Values", "AnyDateTimeMaxNullable")]
			public DateTime? AnyDateTimeMaxNullable = DateTime.MaxValue;

			[MappingXPath("/RoundtripAttributes/Values", "AnyGuidNullable")]
			public Guid? AnyGuidNullable = Guid1;

			[MappingXPath("/RoundtripAttributes/Values", "Season")]
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public Seasons Season = Seasons.Spring;
			[MappingXPath("/RoundtripAttributes/Values", "Features")]
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public PhoneFeatures Features = PhoneFeatures.GPRS | PhoneFeatures.Wifi;

			[MappingXPath("/RoundtripAttributes/Values", "SeasonAsNumber")]
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public Seasons SeasonAsNumber = Seasons.Spring;
			[MappingXPath("/RoundtripAttributes/Values", "FeaturesAsNumber")]
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public PhoneFeatures FeaturesAsNumber = PhoneFeatures.GPRS | PhoneFeatures.Wifi;

			[MappingXPath("/RoundtripAttributes/Values", "SeasonNullable")]
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public Seasons? SeasonNullable = Seasons.Spring;
			[MappingXPath("/RoundtripAttributes/Values", "FeaturesNullable")]
			[ValueConvertor(typeof(EnumStringValueConverter))]
			public PhoneFeatures? FeaturesNullable = PhoneFeatures.GPRS | PhoneFeatures.Wifi;

			[MappingXPath("/RoundtripAttributes/Values", "SeasonAsNumberNullable")]
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public Seasons? SeasonAsNumberNullable = Seasons.Spring;
			[MappingXPath("/RoundtripAttributes/Values", "FeaturesAsNumberNullable")]
			[ValueConvertor(typeof(EnumNumberValueConverter))]
			public PhoneFeatures? FeaturesAsNumberNullable = PhoneFeatures.GPRS | PhoneFeatures.Wifi;
		}

		[TestCase]
		public void TestRoundtrip()
		{
			var serializable = new RoundtripTest();

			var xml = serializable.ToXml();

			Assert.IsTrue(!xml.Contains("__type"), "Type info found");

			var deserialized = XmlSerialization.LoadFromXml<RoundtripTest>(xml);

			Assert.AreEqual(serializable.AnyString, deserialized.AnyString);
			Assert.AreEqual(serializable.AnyChar, deserialized.AnyChar);
			Assert.AreEqual(serializable.AnyByteMin, deserialized.AnyByteMin);
			Assert.AreEqual(serializable.AnySByteMin, deserialized.AnySByteMin);
			Assert.AreEqual(serializable.AnyInt16Min, deserialized.AnyInt16Min);
			Assert.AreEqual(serializable.AnyUInt16Min, deserialized.AnyUInt16Min);
			Assert.AreEqual(serializable.AnyInt32Min, deserialized.AnyInt32Min);
			Assert.AreEqual(serializable.AnyUInt32Min, deserialized.AnyUInt32Min);
			Assert.AreEqual(serializable.AnyInt64Min, deserialized.AnyInt64Min);
			Assert.AreEqual(serializable.AnyUInt64Min, deserialized.AnyUInt64Min);
			Assert.AreEqual(serializable.AnySingleMin, deserialized.AnySingleMin);
			Assert.AreEqual(serializable.AnyDoubleMin, deserialized.AnyDoubleMin);
			Assert.AreEqual(serializable.AnyDecimalMin, deserialized.AnyDecimalMin);
			Assert.AreEqual(serializable.AnyByteMax, deserialized.AnyByteMax);
			Assert.AreEqual(serializable.AnySByteMax, deserialized.AnySByteMax);
			Assert.AreEqual(serializable.AnyInt16Max, deserialized.AnyInt16Max);
			Assert.AreEqual(serializable.AnyUInt16Max, deserialized.AnyUInt16Max);
			Assert.AreEqual(serializable.AnyInt32Max, deserialized.AnyInt32Max);
			Assert.AreEqual(serializable.AnyUInt32Max, deserialized.AnyUInt32Max);
			Assert.AreEqual(serializable.AnyInt64Max, deserialized.AnyInt64Max);
			Assert.AreEqual(serializable.AnyUInt64Max, deserialized.AnyUInt64Max);
			Assert.AreEqual(serializable.AnySingleMax, deserialized.AnySingleMax);
			Assert.AreEqual(serializable.AnyDoubleMax, deserialized.AnyDoubleMax);
			Assert.AreEqual(serializable.AnyDecimalMax, deserialized.AnyDecimalMax);
			Assert.AreEqual(serializable.BoolTrue, deserialized.BoolTrue);
			Assert.AreEqual(serializable.BoolFalse, deserialized.BoolFalse);
			Assert.AreEqual(serializable.AnyDateTimeMin, deserialized.AnyDateTimeMin);
			Assert.AreEqual(serializable.AnyDateTimeMax, deserialized.AnyDateTimeMax);
			Assert.AreEqual(serializable.AnyGuid, deserialized.AnyGuid);
			Assert.AreEqual(serializable.AnyCharNullable, deserialized.AnyCharNullable);
			Assert.AreEqual(serializable.AnyByteMinNullable, deserialized.AnyByteMinNullable);
			Assert.AreEqual(serializable.AnySByteMinNullable, deserialized.AnySByteMinNullable);
			Assert.AreEqual(serializable.AnyInt16MinNullable, deserialized.AnyInt16MinNullable);
			Assert.AreEqual(serializable.AnyUInt16MinNullable, deserialized.AnyUInt16MinNullable);
			Assert.AreEqual(serializable.AnyInt32MinNullable, deserialized.AnyInt32MinNullable);
			Assert.AreEqual(serializable.AnyUInt32MinNullable, deserialized.AnyUInt32MinNullable);
			Assert.AreEqual(serializable.AnyInt64MinNullable, deserialized.AnyInt64MinNullable);
			Assert.AreEqual(serializable.AnyUInt64MinNullable, deserialized.AnyUInt64MinNullable);
			Assert.AreEqual(serializable.AnySingleMinNullable, deserialized.AnySingleMinNullable);
			Assert.AreEqual(serializable.AnyDoubleMinNullable, deserialized.AnyDoubleMinNullable);
			Assert.AreEqual(serializable.AnyDecimalMinNullable, deserialized.AnyDecimalMinNullable);
			Assert.AreEqual(serializable.AnyByteMaxNullable, deserialized.AnyByteMaxNullable);
			Assert.AreEqual(serializable.AnySByteMaxNullable, deserialized.AnySByteMaxNullable);
			Assert.AreEqual(serializable.AnyInt16MaxNullable, deserialized.AnyInt16MaxNullable);
			Assert.AreEqual(serializable.AnyUInt16MaxNullable, deserialized.AnyUInt16MaxNullable);
			Assert.AreEqual(serializable.AnyInt32MaxNullable, deserialized.AnyInt32MaxNullable);
			Assert.AreEqual(serializable.AnyUInt32MaxNullable, deserialized.AnyUInt32MaxNullable);
			Assert.AreEqual(serializable.AnyInt64MaxNullable, deserialized.AnyInt64MaxNullable);
			Assert.AreEqual(serializable.AnyUInt64MaxNullable, deserialized.AnyUInt64MaxNullable);
			Assert.AreEqual(serializable.AnySingleMaxNullable, deserialized.AnySingleMaxNullable);
			Assert.AreEqual(serializable.AnyDoubleMaxNullable, deserialized.AnyDoubleMaxNullable);
			Assert.AreEqual(serializable.AnyDecimalMaxNullable, deserialized.AnyDecimalMaxNullable);
			Assert.AreEqual(serializable.BoolTrueNullable, deserialized.BoolTrueNullable);
			Assert.AreEqual(serializable.BoolFalseNullable, deserialized.BoolFalseNullable);
			Assert.AreEqual(serializable.AnyDateTimeMinNullable, deserialized.AnyDateTimeMinNullable);
			Assert.AreEqual(serializable.AnyDateTimeMaxNullable, deserialized.AnyDateTimeMaxNullable);
			Assert.AreEqual(serializable.AnyGuidNullable, deserialized.AnyGuidNullable);
			Assert.AreEqual(serializable.Season, deserialized.Season);
			Assert.AreEqual(serializable.Features, deserialized.Features);
			Assert.AreEqual(serializable.SeasonAsNumber, deserialized.SeasonAsNumber);
			Assert.AreEqual(serializable.FeaturesAsNumber, deserialized.FeaturesAsNumber);

			Assert.IsTrue(serializable.Base64ByteArray.SequenceEqual(deserialized.Base64ByteArray));
			Assert.IsTrue(serializable.AnyStringArray.SequenceEqual(deserialized.AnyStringArray));
			Assert.IsTrue(serializable.AnyCharArray.SequenceEqual(deserialized.AnyCharArray));
			Assert.IsTrue(serializable.AnyByteArray.SequenceEqual(deserialized.AnyByteArray));
			Assert.IsTrue(serializable.AnySByteArray.SequenceEqual(deserialized.AnySByteArray));
			Assert.IsTrue(serializable.AnyInt16Array.SequenceEqual(deserialized.AnyInt16Array));
			Assert.IsTrue(serializable.AnyUInt16Array.SequenceEqual(deserialized.AnyUInt16Array));
			Assert.IsTrue(serializable.AnyInt32Array.SequenceEqual(deserialized.AnyInt32Array));
			Assert.IsTrue(serializable.AnyUInt32Array.SequenceEqual(deserialized.AnyUInt32Array));
			Assert.IsTrue(serializable.AnyInt64Array.SequenceEqual(deserialized.AnyInt64Array));
			Assert.IsTrue(serializable.AnyUInt64Array.SequenceEqual(deserialized.AnyUInt64Array));
			Assert.IsTrue(serializable.AnySingleArray.SequenceEqual(deserialized.AnySingleArray));
			Assert.IsTrue(serializable.AnyDoubleArray.SequenceEqual(deserialized.AnyDoubleArray));
			Assert.IsTrue(serializable.AnyDecimalArray.SequenceEqual(deserialized.AnyDecimalArray));
			Assert.IsTrue(serializable.BoolArray.SequenceEqual(deserialized.BoolArray));
			Assert.IsTrue(serializable.AnyDateTimeArray.SequenceEqual(deserialized.AnyDateTimeArray));
			Assert.IsTrue(serializable.AnyGuidArray.SequenceEqual(deserialized.AnyGuidArray));
			Assert.IsTrue(serializable.SeasonArray.SequenceEqual(deserialized.SeasonArray));
			Assert.IsTrue(serializable.FeaturesArray.SequenceEqual(deserialized.FeaturesArray));
			Assert.IsTrue(serializable.SeasonArrayAsNumber.SequenceEqual(deserialized.SeasonArrayAsNumber));
			Assert.IsTrue(serializable.FeaturesArrayAsNumber.SequenceEqual(deserialized.FeaturesArrayAsNumber));

			Assert.AreEqual(serializable.ObjectProperty.SimpleInnerProperty, deserialized.ObjectProperty.SimpleInnerProperty);

			Assert.AreEqual(serializable.InnerObjects[0].SimpleInnerProperty, deserialized.InnerObjects[0].SimpleInnerProperty);
			Assert.AreEqual(serializable.InnerObjects[1].SimpleInnerProperty, deserialized.InnerObjects[1].SimpleInnerProperty);

			Assert.AreEqual(serializable.SeasonNullable, deserialized.SeasonNullable);
			Assert.AreEqual(serializable.FeaturesNullable, deserialized.FeaturesNullable);
			Assert.AreEqual(serializable.SeasonAsNumberNullable, deserialized.SeasonAsNumberNullable);
			Assert.AreEqual(serializable.FeaturesAsNumberNullable, deserialized.FeaturesAsNumberNullable);
		}

		[TestCase]
		public void TestWeakTypesTheSameXml()
		{
			var serializableStrong = new RoundtripTest();
			var serializableWeak = new RoundtripWeakTest();

			var xmlStrong = serializableStrong.ToXml();
			var xmlWeak = serializableWeak.ToXml();

			Assert.AreEqual(xmlStrong.Replace("RoundtripTest", "RoundtripWeakTest"), xmlWeak);
		}

		[TestCase]
		public void TestRoundtripWeakTypesWithEmittedTypes()
		{
			var serializable = new RoundtripTest();
			var serializableWeak = new RoundtripWeakTest();

			var xml = serializableWeak.ToXml(emitTypeInfo: true);

			var deserialized = XmlSerialization.LoadFromXml<RoundtripWeakTest>(xml);

			Assert.AreEqual(serializable.AnyString, deserialized.AnyString);
			Assert.AreEqual(serializable.AnyChar, deserialized.AnyChar);
			Assert.AreEqual(serializable.AnyByteMin, deserialized.AnyByteMin);
			Assert.AreEqual(serializable.AnySByteMin, deserialized.AnySByteMin);
			Assert.AreEqual(serializable.AnyInt16Min, deserialized.AnyInt16Min);
			Assert.AreEqual(serializable.AnyUInt16Min, deserialized.AnyUInt16Min);
			Assert.AreEqual(serializable.AnyInt32Min, deserialized.AnyInt32Min);
			Assert.AreEqual(serializable.AnyUInt32Min, deserialized.AnyUInt32Min);
			Assert.AreEqual(serializable.AnyInt64Min, deserialized.AnyInt64Min);
			Assert.AreEqual(serializable.AnyUInt64Min, deserialized.AnyUInt64Min);
			Assert.AreEqual(serializable.AnySingleMin, deserialized.AnySingleMin);
			Assert.AreEqual(serializable.AnyDoubleMin, deserialized.AnyDoubleMin);
			Assert.AreEqual(serializable.AnyDecimalMin, deserialized.AnyDecimalMin);
			Assert.AreEqual(serializable.AnyByteMax, deserialized.AnyByteMax);
			Assert.AreEqual(serializable.AnySByteMax, deserialized.AnySByteMax);
			Assert.AreEqual(serializable.AnyInt16Max, deserialized.AnyInt16Max);
			Assert.AreEqual(serializable.AnyUInt16Max, deserialized.AnyUInt16Max);
			Assert.AreEqual(serializable.AnyInt32Max, deserialized.AnyInt32Max);
			Assert.AreEqual(serializable.AnyUInt32Max, deserialized.AnyUInt32Max);
			Assert.AreEqual(serializable.AnyInt64Max, deserialized.AnyInt64Max);
			Assert.AreEqual(serializable.AnyUInt64Max, deserialized.AnyUInt64Max);
			Assert.AreEqual(serializable.AnySingleMax, deserialized.AnySingleMax);
			Assert.AreEqual(serializable.AnyDoubleMax, deserialized.AnyDoubleMax);
			Assert.AreEqual(serializable.AnyDecimalMax, deserialized.AnyDecimalMax);
			Assert.AreEqual(serializable.BoolTrue, deserialized.BoolTrue);
			Assert.AreEqual(serializable.BoolFalse, deserialized.BoolFalse);
			Assert.AreEqual(serializable.AnyDateTimeMin, deserialized.AnyDateTimeMin);
			Assert.AreEqual(serializable.AnyDateTimeMax, deserialized.AnyDateTimeMax);
			Assert.AreEqual(serializable.AnyGuid, deserialized.AnyGuid);
			Assert.AreEqual(serializable.AnyCharNullable, deserialized.AnyCharNullable);
			Assert.AreEqual(serializable.AnyByteMinNullable, deserialized.AnyByteMinNullable);
			Assert.AreEqual(serializable.AnySByteMinNullable, deserialized.AnySByteMinNullable);
			Assert.AreEqual(serializable.AnyInt16MinNullable, deserialized.AnyInt16MinNullable);
			Assert.AreEqual(serializable.AnyUInt16MinNullable, deserialized.AnyUInt16MinNullable);
			Assert.AreEqual(serializable.AnyInt32MinNullable, deserialized.AnyInt32MinNullable);
			Assert.AreEqual(serializable.AnyUInt32MinNullable, deserialized.AnyUInt32MinNullable);
			Assert.AreEqual(serializable.AnyInt64MinNullable, deserialized.AnyInt64MinNullable);
			Assert.AreEqual(serializable.AnyUInt64MinNullable, deserialized.AnyUInt64MinNullable);
			Assert.AreEqual(serializable.AnySingleMinNullable, deserialized.AnySingleMinNullable);
			Assert.AreEqual(serializable.AnyDoubleMinNullable, deserialized.AnyDoubleMinNullable);
			Assert.AreEqual(serializable.AnyDecimalMinNullable, deserialized.AnyDecimalMinNullable);
			Assert.AreEqual(serializable.AnyByteMaxNullable, deserialized.AnyByteMaxNullable);
			Assert.AreEqual(serializable.AnySByteMaxNullable, deserialized.AnySByteMaxNullable);
			Assert.AreEqual(serializable.AnyInt16MaxNullable, deserialized.AnyInt16MaxNullable);
			Assert.AreEqual(serializable.AnyUInt16MaxNullable, deserialized.AnyUInt16MaxNullable);
			Assert.AreEqual(serializable.AnyInt32MaxNullable, deserialized.AnyInt32MaxNullable);
			Assert.AreEqual(serializable.AnyUInt32MaxNullable, deserialized.AnyUInt32MaxNullable);
			Assert.AreEqual(serializable.AnyInt64MaxNullable, deserialized.AnyInt64MaxNullable);
			Assert.AreEqual(serializable.AnyUInt64MaxNullable, deserialized.AnyUInt64MaxNullable);
			Assert.AreEqual(serializable.AnySingleMaxNullable, deserialized.AnySingleMaxNullable);
			Assert.AreEqual(serializable.AnyDoubleMaxNullable, deserialized.AnyDoubleMaxNullable);
			Assert.AreEqual(serializable.AnyDecimalMaxNullable, deserialized.AnyDecimalMaxNullable);
			Assert.AreEqual(serializable.BoolTrueNullable, deserialized.BoolTrueNullable);
			Assert.AreEqual(serializable.BoolFalseNullable, deserialized.BoolFalseNullable);
			Assert.AreEqual(serializable.AnyDateTimeMinNullable, deserialized.AnyDateTimeMinNullable);
			Assert.AreEqual(serializable.AnyDateTimeMaxNullable, deserialized.AnyDateTimeMaxNullable);
			Assert.AreEqual(serializable.AnyGuidNullable, deserialized.AnyGuidNullable);
			Assert.AreEqual(serializable.Season, deserialized.Season);
			Assert.AreEqual(serializable.Features, deserialized.Features);
			Assert.AreEqual(serializable.SeasonAsNumber, deserialized.SeasonAsNumber);
			Assert.AreEqual(serializable.FeaturesAsNumber, deserialized.FeaturesAsNumber);

			Assert.IsTrue(((byte[])deserialized.Base64ByteArray).SequenceEqual(serializable.Base64ByteArray));
			Assert.IsTrue(((string[])deserialized.AnyStringArray).SequenceEqual(serializable.AnyStringArray));
			Assert.IsTrue(((char[])deserialized.AnyCharArray).SequenceEqual(serializable.AnyCharArray));
			Assert.IsTrue(((byte[])deserialized.AnyByteArray).SequenceEqual(serializable.AnyByteArray));
			Assert.IsTrue(((sbyte[])deserialized.AnySByteArray).SequenceEqual(serializable.AnySByteArray));
			Assert.IsTrue(((Int16[])deserialized.AnyInt16Array).SequenceEqual(serializable.AnyInt16Array));
			Assert.IsTrue(((UInt16[])deserialized.AnyUInt16Array).SequenceEqual(serializable.AnyUInt16Array));
			Assert.IsTrue(((Int32[])deserialized.AnyInt32Array).SequenceEqual(serializable.AnyInt32Array));
			Assert.IsTrue(((UInt32[])deserialized.AnyUInt32Array).SequenceEqual(serializable.AnyUInt32Array));
			Assert.IsTrue(((Int64[])deserialized.AnyInt64Array).SequenceEqual(serializable.AnyInt64Array));
			Assert.IsTrue(((UInt64[])deserialized.AnyUInt64Array).SequenceEqual(serializable.AnyUInt64Array));
			Assert.IsTrue(((Single[])deserialized.AnySingleArray).SequenceEqual(serializable.AnySingleArray));
			Assert.IsTrue(((Double[])deserialized.AnyDoubleArray).SequenceEqual(serializable.AnyDoubleArray));
			Assert.IsTrue(((Decimal[])deserialized.AnyDecimalArray).SequenceEqual(serializable.AnyDecimalArray));
			Assert.IsTrue(((bool[])deserialized.BoolArray).SequenceEqual(serializable.BoolArray));
			Assert.IsTrue(((DateTime[])deserialized.AnyDateTimeArray).SequenceEqual(serializable.AnyDateTimeArray));
			Assert.IsTrue(((Guid[])deserialized.AnyGuidArray).SequenceEqual(serializable.AnyGuidArray));
			Assert.IsTrue(((Seasons[])deserialized.SeasonArray).SequenceEqual(serializable.SeasonArray));
			Assert.IsTrue(((PhoneFeatures[])deserialized.FeaturesArray).SequenceEqual(serializable.FeaturesArray));
			Assert.IsTrue(((Seasons[])deserialized.SeasonArrayAsNumber).SequenceEqual(serializable.SeasonArrayAsNumber));
			Assert.IsTrue(((PhoneFeatures[])deserialized.FeaturesArrayAsNumber).SequenceEqual(serializable.FeaturesArrayAsNumber));

			Assert.AreEqual(((InnerClass)serializable.ObjectProperty).SimpleInnerProperty, serializable.ObjectProperty.SimpleInnerProperty);

			Assert.AreEqual(((InnerClass[])serializable.InnerObjects)[0].SimpleInnerProperty, serializable.InnerObjects[0].SimpleInnerProperty);
			Assert.AreEqual(((InnerClass[])serializable.InnerObjects)[1].SimpleInnerProperty, serializable.InnerObjects[1].SimpleInnerProperty);
		}

		[TestCase]
		public void TestRoundtripMappedAsAttributes()
		{
			var serializable = new RoundtripAttributesTest();

			var xml = serializable.ToXml();

			Assert.IsTrue(!xml.Contains("__type"), "Type info found");

			var deserialized = XmlSerialization.LoadFromXml<RoundtripAttributesTest>(xml);

			Assert.AreEqual(serializable.AnyString, deserialized.AnyString);
			Assert.AreEqual(serializable.AnyChar, deserialized.AnyChar);
			Assert.AreEqual(serializable.AnyByteMin, deserialized.AnyByteMin);
			Assert.AreEqual(serializable.AnySByteMin, deserialized.AnySByteMin);
			Assert.AreEqual(serializable.AnyInt16Min, deserialized.AnyInt16Min);
			Assert.AreEqual(serializable.AnyUInt16Min, deserialized.AnyUInt16Min);
			Assert.AreEqual(serializable.AnyInt32Min, deserialized.AnyInt32Min);
			Assert.AreEqual(serializable.AnyUInt32Min, deserialized.AnyUInt32Min);
			Assert.AreEqual(serializable.AnyInt64Min, deserialized.AnyInt64Min);
			Assert.AreEqual(serializable.AnyUInt64Min, deserialized.AnyUInt64Min);
			Assert.AreEqual(serializable.AnySingleMin, deserialized.AnySingleMin);
			Assert.AreEqual(serializable.AnyDoubleMin, deserialized.AnyDoubleMin);
			Assert.AreEqual(serializable.AnyDecimalMin, deserialized.AnyDecimalMin);
			Assert.AreEqual(serializable.AnyByteMax, deserialized.AnyByteMax);
			Assert.AreEqual(serializable.AnySByteMax, deserialized.AnySByteMax);
			Assert.AreEqual(serializable.AnyInt16Max, deserialized.AnyInt16Max);
			Assert.AreEqual(serializable.AnyUInt16Max, deserialized.AnyUInt16Max);
			Assert.AreEqual(serializable.AnyInt32Max, deserialized.AnyInt32Max);
			Assert.AreEqual(serializable.AnyUInt32Max, deserialized.AnyUInt32Max);
			Assert.AreEqual(serializable.AnyInt64Max, deserialized.AnyInt64Max);
			Assert.AreEqual(serializable.AnyUInt64Max, deserialized.AnyUInt64Max);
			Assert.AreEqual(serializable.AnySingleMax, deserialized.AnySingleMax);
			Assert.AreEqual(serializable.AnyDoubleMax, deserialized.AnyDoubleMax);
			Assert.AreEqual(serializable.AnyDecimalMax, deserialized.AnyDecimalMax);
			Assert.AreEqual(serializable.BoolTrue, deserialized.BoolTrue);
			Assert.AreEqual(serializable.BoolFalse, deserialized.BoolFalse);
			Assert.AreEqual(serializable.AnyDateTimeMin, deserialized.AnyDateTimeMin);
			Assert.AreEqual(serializable.AnyDateTimeMax, deserialized.AnyDateTimeMax);
			Assert.AreEqual(serializable.AnyGuid, deserialized.AnyGuid);
			Assert.AreEqual(serializable.AnyCharNullable, deserialized.AnyCharNullable);
			Assert.AreEqual(serializable.AnyByteMinNullable, deserialized.AnyByteMinNullable);
			Assert.AreEqual(serializable.AnySByteMinNullable, deserialized.AnySByteMinNullable);
			Assert.AreEqual(serializable.AnyInt16MinNullable, deserialized.AnyInt16MinNullable);
			Assert.AreEqual(serializable.AnyUInt16MinNullable, deserialized.AnyUInt16MinNullable);
			Assert.AreEqual(serializable.AnyInt32MinNullable, deserialized.AnyInt32MinNullable);
			Assert.AreEqual(serializable.AnyUInt32MinNullable, deserialized.AnyUInt32MinNullable);
			Assert.AreEqual(serializable.AnyInt64MinNullable, deserialized.AnyInt64MinNullable);
			Assert.AreEqual(serializable.AnyUInt64MinNullable, deserialized.AnyUInt64MinNullable);
			Assert.AreEqual(serializable.AnySingleMinNullable, deserialized.AnySingleMinNullable);
			Assert.AreEqual(serializable.AnyDoubleMinNullable, deserialized.AnyDoubleMinNullable);
			Assert.AreEqual(serializable.AnyDecimalMinNullable, deserialized.AnyDecimalMinNullable);
			Assert.AreEqual(serializable.AnyByteMaxNullable, deserialized.AnyByteMaxNullable);
			Assert.AreEqual(serializable.AnySByteMaxNullable, deserialized.AnySByteMaxNullable);
			Assert.AreEqual(serializable.AnyInt16MaxNullable, deserialized.AnyInt16MaxNullable);
			Assert.AreEqual(serializable.AnyUInt16MaxNullable, deserialized.AnyUInt16MaxNullable);
			Assert.AreEqual(serializable.AnyInt32MaxNullable, deserialized.AnyInt32MaxNullable);
			Assert.AreEqual(serializable.AnyUInt32MaxNullable, deserialized.AnyUInt32MaxNullable);
			Assert.AreEqual(serializable.AnyInt64MaxNullable, deserialized.AnyInt64MaxNullable);
			Assert.AreEqual(serializable.AnyUInt64MaxNullable, deserialized.AnyUInt64MaxNullable);
			Assert.AreEqual(serializable.AnySingleMaxNullable, deserialized.AnySingleMaxNullable);
			Assert.AreEqual(serializable.AnyDoubleMaxNullable, deserialized.AnyDoubleMaxNullable);
			Assert.AreEqual(serializable.AnyDecimalMaxNullable, deserialized.AnyDecimalMaxNullable);
			Assert.AreEqual(serializable.BoolTrueNullable, deserialized.BoolTrueNullable);
			Assert.AreEqual(serializable.BoolFalseNullable, deserialized.BoolFalseNullable);
			Assert.AreEqual(serializable.AnyDateTimeMinNullable, deserialized.AnyDateTimeMinNullable);
			Assert.AreEqual(serializable.AnyDateTimeMaxNullable, deserialized.AnyDateTimeMaxNullable);
			Assert.AreEqual(serializable.AnyGuidNullable, deserialized.AnyGuidNullable);
			Assert.AreEqual(serializable.Season, deserialized.Season);
			Assert.AreEqual(serializable.Features, deserialized.Features);
			Assert.AreEqual(serializable.SeasonAsNumber, deserialized.SeasonAsNumber);
			Assert.AreEqual(serializable.FeaturesAsNumber, deserialized.FeaturesAsNumber);
			Assert.AreEqual(serializable.SeasonNullable, deserialized.SeasonNullable);
			Assert.AreEqual(serializable.FeaturesNullable, deserialized.FeaturesNullable);
			Assert.AreEqual(serializable.SeasonAsNumberNullable, deserialized.SeasonAsNumberNullable);
			Assert.AreEqual(serializable.FeaturesAsNumberNullable, deserialized.FeaturesAsNumberNullable);
		}
	}
}
