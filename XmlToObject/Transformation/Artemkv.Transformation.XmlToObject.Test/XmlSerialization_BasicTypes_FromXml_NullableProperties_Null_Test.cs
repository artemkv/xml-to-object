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
	/// Checks that all properties are set to null in case the value cannot be found when deserialized from Xml.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_BasicTypes_FromXml_NullableProperties_Null_Test
	{
		#region Load From Xml

		[TestCase]
		public void TestStringPropertyLoadFromXml()
		{
			var xml = @"<String><AnyString></AnyString></String>";

			var serializable = XmlSerialization.LoadFromXml<WithStringProperty>(xml);

			Assert.AreEqual(null, serializable.AnyString);
		}

		[TestCase]
		public void TestCharPropertyLoadFromXml()
		{
			var xml = @"<Char><AnyChar></AnyChar></Char>";

			var serializable = XmlSerialization.LoadFromXml<WithCharNullableProperty>(xml);

			Assert.AreEqual(null, serializable.AnyChar);
		}

		[TestCase]
		public void TestNumericPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnyByte></AnyByte>
  <AnySByte></AnySByte>
  <AnyInt16></AnyInt16>
  <AnyUInt16></AnyUInt16>
  <AnyInt32></AnyInt32>
  <AnyUInt32></AnyUInt32>
  <AnyInt64></AnyInt64>
  <AnyUInt64></AnyUInt64>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithNumericNullableProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(null, serializable.AnyByte);
			Assert.AreEqual(null, serializable.AnySByte);
			Assert.AreEqual(null, serializable.AnyInt16);
			Assert.AreEqual(null, serializable.AnyUInt16);
			Assert.AreEqual(null, serializable.AnyInt32);
			Assert.AreEqual(null, serializable.AnyUInt32);
			Assert.AreEqual(null, serializable.AnyInt64);
			Assert.AreEqual(null, serializable.AnyUInt64);
		}

		[TestCase]
		public void TestFloatPointNumericPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnySingle></AnySingle>
  <AnyDouble></AnyDouble>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithFloatPointNumericNullableProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(null, serializable.AnySingle);
			Assert.AreEqual(null, serializable.AnyDouble);
		}

		[TestCase]
		public void TestDecimalPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnyDecimal></AnyDecimal>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithDecimalNullableProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(null, serializable.AnyDecimal);
		}

		[TestCase]
		public void TestBooleanPropertyLoadFromXml()
		{
			var xml = @"<Boolean>
  <BoolTrue></BoolTrue>
  <BoolFalse></BoolFalse>
</Boolean>";

			var serializable = XmlSerialization.LoadFromXml<WithBooleanNullableProperty>(xml);

			Assert.AreEqual(null, serializable.BoolTrue);
			Assert.AreEqual(null, serializable.BoolFalse);
		}

		[TestCase]
		public void TestDateTimePropertyLoadFromXml()
		{
			var xml = @"<Date>
  <AnyDate></AnyDate>
</Date>";

			var serializable = XmlSerialization.LoadFromXml<WithDateTimeNullableProperty>(xml, provider: CultureInfo.InvariantCulture);

			Assert.AreEqual(null, serializable.AnyDateTime);
		}

		[TestCase]
		public void TestByteArrayPropertyLoadFromXml()
		{
			var xml = @"<Bytes>
  <AnyBase64String></AnyBase64String>
</Bytes>";

			var serializable = XmlSerialization.LoadFromXml<WithByteArrayProperty>(xml);

			Assert.AreEqual(null, serializable.AnyByteArray);
		}

		#endregion
	}
}
