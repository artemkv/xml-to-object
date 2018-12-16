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
	/// Checks that all properties can be serialized to Xml.
	/// Happy path scenario: all properties have values that are different from defaults.
	/// 
	/// Formatting is applied.
	/// Min/max values are used when it makes sense.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_BasicTypes_ToXml_Properties_Test
	{
		#region To Xml

		[TestCase]
		public void TestStringPropertyToXml()
		{
			var serializable = new WithStringProperty()
			{
				AnyString = "MyString"
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/String/AnyString");
			Assert.AreEqual("MyString", element.Value);
		}

		[TestCase]
		public void TestCharPropertyToXml()
		{
			var serializable = new WithCharProperty()
			{
				AnyChar = 'A'
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/Char/AnyChar");
			Assert.AreEqual("A", element.Value);
		}

		[TestCase]
		public void TestNumericPropertyToXml()
		{
			var serializable = new WithNumericProperty()
			{
				AnyByte = Byte.MaxValue,
				AnySByte = SByte.MinValue,
				AnyInt16 = Int16.MinValue,
				AnyUInt16 = UInt16.MaxValue,
				AnyInt32 = Int32.MinValue,
				AnyUInt32 = UInt32.MaxValue,
				AnyInt64 = Int64.MinValue,
				AnyUInt64 = UInt64.MaxValue
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);

			var doc = XDocument.Parse(xml);
			var elementByte = doc.XPathSelectElement("/Numeric/AnyByte");
			var elementSByte = doc.XPathSelectElement("/Numeric/AnySByte");
			var elementAnyInt16 = doc.XPathSelectElement("/Numeric/AnyInt16");
			var elementAnyUInt16 = doc.XPathSelectElement("/Numeric/AnyUInt16");
			var elementAnyInt32 = doc.XPathSelectElement("/Numeric/AnyInt32");
			var elementAnyUInt32 = doc.XPathSelectElement("/Numeric/AnyUInt32");
			var elementAnyInt64 = doc.XPathSelectElement("/Numeric/AnyInt64");
			var elementAnyUInt64 = doc.XPathSelectElement("/Numeric/AnyUInt64");

			Assert.AreEqual(Byte.MaxValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elementByte.Value);
			Assert.AreEqual(SByte.MinValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elementSByte.Value);
			Assert.AreEqual(Int16.MinValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elementAnyInt16.Value);
			Assert.AreEqual(UInt16.MaxValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elementAnyUInt16.Value);
			Assert.AreEqual(Int32.MinValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elementAnyInt32.Value);
			Assert.AreEqual(UInt32.MaxValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elementAnyUInt32.Value);
			Assert.AreEqual(Int64.MinValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elementAnyInt64.Value);
			Assert.AreEqual(UInt64.MaxValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat) , elementAnyUInt64.Value);
		}

		[TestCase]
		public void TestFloatPointNumericPropertyToXml()
		{
			var serializable = new WithFloatPointNumericProperty()
			{
				AnySingle = 3.402823E+38f,
				AnyDouble = 1.79769313486231E+308,
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);

			var doc = XDocument.Parse(xml);
			var elementSingle = doc.XPathSelectElement("/Numeric/AnySingle");
			var elementDouble = doc.XPathSelectElement("/Numeric/AnyDouble");

			Assert.AreEqual((3.402823E+38f).ToString("N", CultureInfo.InvariantCulture.NumberFormat), elementSingle.Value);
			Assert.AreEqual((1.79769313486231E+308).ToString("N", CultureInfo.InvariantCulture.NumberFormat), elementDouble.Value);
		}

		[TestCase]
		public void TestDecimalPropertyToXml()
		{
			var serializable = new WithDecimalProperty()
			{
				AnyDecimal = Decimal.MaxValue
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/Numeric/AnyDecimal");
			Assert.AreEqual(Decimal.MaxValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), element.Value);
		}

		[TestCase]
		public void TestBooleanPropertyToXml()
		{
			var serializable = new WithBooleanProperty()
			{
				BoolTrue = true,
				BoolFalse = false
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("Boolean/BoolTrue");
			var elementFalse = doc.XPathSelectElement("Boolean/BoolFalse");

			Assert.AreEqual("True", elementTrue.Value);
			Assert.AreEqual("False", elementFalse.Value);
		}

		[TestCase]
		public void TestDateTimePropertyToXml()
		{
			var expectedDate = new DateTime(2012, 10, 13, 16, 30, 55, 123);
			var serializable = new WithDateTimeProperty()
			{
				AnyDateTime = expectedDate
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture);

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/Date/AnyDate");
			Assert.AreEqual(expectedDate.ToString("yyyy/MM/dd HH:mm:ss:fff", CultureInfo.InvariantCulture), element.Value);
		}

		[TestCase]
		public void TestByteArrayPropertyToXml()
		{
			var serializable = new WithByteArrayProperty()
			{
				AnyByteArray = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/Bytes/AnyBase64String");
			Assert.AreEqual("AQIDBAUGBwgJ", element.Value);
		}

		[TestCase]
		public void TestGuidPropertyToXml()
		{
			Guid expectedGuid = Guid.NewGuid();
			var serializable = new WithGuidProperty()
			{
				AnyGuid = expectedGuid
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture);

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/Guid/AnyGuid");
			Assert.AreEqual(expectedGuid.ToString("B", CultureInfo.InvariantCulture), element.Value);
		}

		#endregion
	}
}
