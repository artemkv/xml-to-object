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
	/// Checks that serialization handles incorrect values correctly.
	/// </summary>
	[TestFixture]
    public class XmlSerialization_BasicTypes_FromXml_Properties_Failed_Test
	{
		#region Load From Xml

		[TestCase, ExpectedException(typeof(SerializationException))]
		public void TestCharPropertyLoadFromXml()
		{
			var xml = @"<Char><AnyChar>MoreThanOneChar</AnyChar></Char>";

			var serializable = XmlSerialization.LoadFromXml<WithCharProperty>(xml);
		}

        [TestCase, ExpectedException(typeof(SerializationException))]
        public void TestNumericPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnyByte>18,446,744,073,709,551,615.00</AnyByte>
  <AnySByte>-128.00</AnySByte>
  <AnyInt16>-32,768.00</AnyInt16>
  <AnyUInt16>65,535.00</AnyUInt16>
  <AnyInt32>-2,147,483,648.00</AnyInt32>
  <AnyUInt32>4,294,967,295.00</AnyUInt32>
  <AnyInt64>-9,223,372,036,854,775,808.00</AnyInt64>
  <AnyUInt64>18,446,744,073,709,551,615.00</AnyUInt64>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithNumericProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);
		}

        [TestCase, ExpectedException(typeof(SerializationException))]
        public void TestFloatPointNumericPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnySingle>179,769,313,486,231,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000.00</AnySingle>
  <AnyDouble>179,769,313,486,231,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000.00</AnyDouble>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithFloatPointNumericProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);
		}

        [TestCase, ExpectedException(typeof(SerializationException))]
        public void TestDecimalPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnyDecimal>79,228,162,514,264,337,593,543,950,335,000.00</AnyDecimal>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithDecimalProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);
		}

        [TestCase, ExpectedException(typeof(SerializationException))]
        public void TestBooleanPropertyLoadFromXml()
		{
			var xml = @"<Boolean>
  <BoolTrue>True</BoolTrue>
  <BoolFalse>Undefined</BoolFalse>
</Boolean>";

			var serializable = XmlSerialization.LoadFromXml<WithBooleanProperty>(xml);
		}

        [TestCase, ExpectedException(typeof(SerializationException))]
        public void TestDateTimePropertyLoadFromXml()
		{
			var xml = @"<Date>
  <AnyDate>20121013163055123</AnyDate>
</Date>";

			var serializable = XmlSerialization.LoadFromXml<WithDateTimeProperty>(xml, provider: CultureInfo.InvariantCulture);
		}

        [TestCase, ExpectedException(typeof(SerializationException))]
        public void TestByteArrayPropertyLoadFromXml()
		{
			var xml = @"<Bytes>
  <AnyBase64String>this_is_not_base64_string</AnyBase64String>
</Bytes>";

			var serializable = XmlSerialization.LoadFromXml<WithByteArrayProperty>(xml);
		}

        [TestCase, ExpectedException(typeof(SerializationException))]
        public void TestGuidPropertyLoadFromXml()
		{
			var xml = @"<Guid><AnyGuid>{this_is_not_guid}</AnyGuid></Guid>";

			var serializable = XmlSerialization.LoadFromXml<WithGuidProperty>(xml, provider: CultureInfo.InvariantCulture);
		}

		#endregion
	}
}
