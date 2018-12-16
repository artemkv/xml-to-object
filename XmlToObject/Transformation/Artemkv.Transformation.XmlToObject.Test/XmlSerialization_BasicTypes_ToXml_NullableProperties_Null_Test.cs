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
	/// Checks that all properties whose value is null are serialized to Xml as empty strings.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_BasicTypes_ToXml_NullableProperties_Null_Test
	{
		#region To Xml

		[TestCase]
		public void TestStringPropertyToXml()
		{
			var serializable = new WithStringMandatoryProperty()
			{
				AnyString = null			
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/String/AnyString");
			Assert.AreEqual("", element.Value);
		}

		[TestCase]
		public void TestCharPropertyToXml()
		{
			var serializable = new WithCharNullableProperty()
			{
				AnyChar = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/Char/AnyChar");
			Assert.AreEqual("", element.Value);
		}

		[TestCase]
		public void TestNumericPropertyToXml()
		{
			var serializable = new WithNumericNullableProperty()
			{
				AnyByte = null,
				AnySByte = null,
				AnyInt16 = null,
				AnyUInt16 = null,
				AnyInt32 = null,
				AnyUInt32 = null,
				AnyInt64 = null,
				AnyUInt64 = null
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

			Assert.AreEqual("", elementByte.Value);
			Assert.AreEqual("", elementSByte.Value);
			Assert.AreEqual("", elementAnyInt16.Value);
			Assert.AreEqual("", elementAnyUInt16.Value);
			Assert.AreEqual("", elementAnyInt32.Value);
			Assert.AreEqual("", elementAnyUInt32.Value);
			Assert.AreEqual("", elementAnyInt64.Value);
			Assert.AreEqual("" , elementAnyUInt64.Value);
		}

		[TestCase]
		public void TestFloatPointNumericPropertyToXml()
		{
			var serializable = new WithFloatPointNumericNullableProperty()
			{
				AnySingle = null,
				AnyDouble = null,
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);

			var doc = XDocument.Parse(xml);
			var elementSingle = doc.XPathSelectElement("/Numeric/AnySingle");
			var elementDouble = doc.XPathSelectElement("/Numeric/AnyDouble");

			Assert.AreEqual("", elementSingle.Value);
			Assert.AreEqual("", elementDouble.Value);
		}

		[TestCase]
		public void TestDecimalPropertyToXml()
		{
			var serializable = new WithDecimalNullableProperty()
			{
				AnyDecimal = null
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/Numeric/AnyDecimal");
			Assert.AreEqual("", element.Value);
		}

		[TestCase]
		public void TestBooleanPropertyToXml()
		{
			var serializable = new WithBooleanNullableProperty()
			{
				BoolTrue = null,
				BoolFalse = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("Boolean/BoolTrue");
			var elementFalse = doc.XPathSelectElement("Boolean/BoolFalse");

			Assert.AreEqual("", elementTrue.Value);
			Assert.AreEqual("", elementFalse.Value);
		}

		[TestCase]
		public void TestDateTimePropertyToXml()
		{
			var serializable = new WithDateTimeNullableProperty()
			{
				AnyDateTime = null
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture);

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/Date/AnyDate");
			Assert.AreEqual("", element.Value);
		}

		[TestCase]
		public void TestByteArrayPropertyToXml()
		{
			var serializable = new WithByteArrayProperty()
			{
				AnyByteArray = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/Bytes/AnyBase64String");
			Assert.AreEqual("", element.Value);
		}

		#endregion
	}
}
