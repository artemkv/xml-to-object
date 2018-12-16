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
	/// <summary>
	/// Checks that all properties can be deserialized from Xml.
	/// Happy path scenario: all properties have values that are different from defaults.
	/// 
	/// Formatting is applied.
	/// Min/max values are used when it makes sense.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_BasicTypes_FromXml_Properties_Test
	{
		#region Load From Xml

		[TestCase]
		public void TestStringPropertyLoadFromXml()
		{
			var xml = @"<String><AnyString>MyString</AnyString></String>";

			var serializable = XmlSerialization.LoadFromXml<WithStringProperty>(xml);

			Assert.AreEqual("MyString", serializable.AnyString);
		}

		[TestCase]
		public void TestCharPropertyLoadFromXml()
		{
			var xml = @"<Char><AnyChar>A</AnyChar></Char>";

			var serializable = XmlSerialization.LoadFromXml<WithCharProperty>(xml);

			Assert.AreEqual('A', serializable.AnyChar);
		}

		[TestCase]
		public void TestNumericPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnyByte>255.00</AnyByte>
  <AnySByte>-128.00</AnySByte>
  <AnyInt16>-32,768.00</AnyInt16>
  <AnyUInt16>65,535.00</AnyUInt16>
  <AnyInt32>-2,147,483,648.00</AnyInt32>
  <AnyUInt32>4,294,967,295.00</AnyUInt32>
  <AnyInt64>-9,223,372,036,854,775,808.00</AnyInt64>
  <AnyUInt64>18,446,744,073,709,551,615.00</AnyUInt64>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithNumericProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(Byte.MaxValue, serializable.AnyByte);
			Assert.AreEqual(SByte.MinValue, serializable.AnySByte);
			Assert.AreEqual(Int16.MinValue, serializable.AnyInt16);
			Assert.AreEqual(UInt16.MaxValue, serializable.AnyUInt16);
			Assert.AreEqual(Int32.MinValue, serializable.AnyInt32);
			Assert.AreEqual(UInt32.MaxValue, serializable.AnyUInt32);
			Assert.AreEqual(Int64.MinValue, serializable.AnyInt64);
			Assert.AreEqual(UInt64.MaxValue, serializable.AnyUInt64);
		}

		[TestCase]
		public void TestFloatPointNumericPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnySingle>340,282,300,000,000,000,000,000,000,000,000,000,000.00</AnySingle>
  <AnyDouble>179,769,313,486,231,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000.00</AnyDouble>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithFloatPointNumericProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(3.402823E+38f, serializable.AnySingle);
			Assert.AreEqual(1.79769313486231E+308, serializable.AnyDouble);
		}

		[TestCase]
		public void TestDecimalPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnyDecimal>79,228,162,514,264,337,593,543,950,335.00</AnyDecimal>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithDecimalProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(Decimal.MaxValue, serializable.AnyDecimal);
		}

		[TestCase]
		public void TestBooleanPropertyLoadFromXml()
		{
			var xml = @"<Boolean>
  <BoolTrue>True</BoolTrue>
  <BoolFalse>False</BoolFalse>
</Boolean>";

			var serializable = XmlSerialization.LoadFromXml<WithBooleanProperty>(xml);

			Assert.AreEqual(true, serializable.BoolTrue);
			Assert.AreEqual(false, serializable.BoolFalse);
		}

		[TestCase]
		public void TestDateTimePropertyLoadFromXml()
		{
			var xml = @"<Date>
  <AnyDate>2012/10/13 16:30:55:123</AnyDate>
</Date>";

			var serializable = XmlSerialization.LoadFromXml<WithDateTimeProperty>(xml, provider: CultureInfo.InvariantCulture);

			Assert.AreEqual(new DateTime(2012, 10, 13, 16, 30, 55, 123), serializable.AnyDateTime);
		}

		[TestCase]
		public void TestByteArrayPropertyLoadFromXml()
		{
			var xml = @"<Bytes>
  <AnyBase64String>AQIDBAUGBwgJ</AnyBase64String>
</Bytes>";

			var serializable = XmlSerialization.LoadFromXml<WithByteArrayProperty>(xml);

			Assert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, serializable.AnyByteArray);
		}

		[TestCase]
		public void TestGuidPropertyLoadFromXml()
		{
			var xml = @"<Guid><AnyGuid>{3F9E7762-09D8-4594-AA1B-5455F2B39D8B}</AnyGuid></Guid>";

			var serializable = XmlSerialization.LoadFromXml<WithGuidProperty>(xml, provider: CultureInfo.InvariantCulture);

			Assert.AreEqual(Guid.ParseExact("{3F9E7762-09D8-4594-AA1B-5455F2B39D8B}", "B"), serializable.AnyGuid);
		}

		#endregion
	}
}
