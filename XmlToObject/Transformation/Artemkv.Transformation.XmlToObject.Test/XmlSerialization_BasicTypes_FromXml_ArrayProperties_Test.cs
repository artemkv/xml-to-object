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
	/// Checks that all array properties can be deserialized from Xml.
	/// Happy path scenario: all properties have values that are different from defaults.
	/// 
	/// Formatting is applied.
	/// Min/max values are used when it makes sense.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_BasicTypes_FromXml_ArrayProperties_Test
	{
		#region Load From Xml

		[TestCase]
		public void TestStringArrayPropertyLoadFromXml()
		{
			var xml = @"<String>
  <AnyStrings>
    <AnyString>MyString1</AnyString>
    <AnyString>MyString2</AnyString>
  </AnyStrings>
</String>";

			var serializable = XmlSerialization.LoadFromXml<WithStringArrayProperty>(xml);

			Assert.AreEqual("MyString1", serializable.AnyStrings[0]);
			Assert.AreEqual("MyString2", serializable.AnyStrings[1]);
		}

		[TestCase]
		public void TestCharArrayPropertyLoadFromXml()
		{
			var xml = @"<Char>
  <AnyChars>
    <AnyChar>A</AnyChar>
    <AnyChar>B</AnyChar>
  </AnyChars>
</Char>";

			var serializable = XmlSerialization.LoadFromXml<WithCharArrayProperty>(xml);

			Assert.AreEqual('A', serializable.AnyChars[0]);
			Assert.AreEqual('B', serializable.AnyChars[1]);
		}

		[TestCase]
		public void TesByteArrayPropertyLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnyBytes>
    <AnyByte>0.00</AnyByte>
    <AnyByte>255.00</AnyByte>
  </AnyBytes>
</Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithNumericArrayProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(Byte.MinValue, serializable.AnyBytes[0]);
			Assert.AreEqual(Byte.MaxValue, serializable.AnyBytes[1]);
		}

		[TestCase]
		public void TesArrayPropertyWithNullsLoadFromXml()
		{
			var xml = @"<WithArrayPropertyWithNulls>
  <MyArray>
    <Item>aaa</Item>
    <Item></Item>
    <Item>bbb</Item>
  </MyArray>
</WithArrayPropertyWithNulls>";

			var serializable = XmlSerialization.LoadFromXml<WithArrayPropertyWithNulls>(xml);

			Assert.AreEqual("aaa", serializable.MyArray[0]);
			Assert.AreEqual(null, serializable.MyArray[1]);
			Assert.AreEqual("bbb", serializable.MyArray[2]);
		}

		[TestCase]
		public void TesArrayPropertyWithNullsAndTypesLoadFromXml()
		{
			var xml = @"<WithArrayPropertyWithNulls>
  <MyArray p2:__type='System.String[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' xmlns:p2='http://xmltoobject.codeplex.com'>
    <Item p2:__type='System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'>aaa</Item>
    <Item p2:__isNull='true'></Item>
    <Item p2:__type='System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'>bbb</Item>
  </MyArray>
</WithArrayPropertyWithNulls>";

			var serializable = XmlSerialization.LoadFromXml<WithArrayPropertyWithNulls>(xml);

			Assert.AreEqual("aaa", serializable.MyArray[0]);
			Assert.AreEqual(null, serializable.MyArray[1]);
			Assert.AreEqual("bbb", serializable.MyArray[2]);
		}

		[TestCase]
		public void TesArrayPropertyMandatoryAndEmptyLoadFromXml()
		{
			var xml = @"<WithStringArrayMandatoryAndEmptyProperty>
  <MyArray />
</WithStringArrayMandatoryAndEmptyProperty>";

			var serializable = XmlSerialization.LoadFromXml<WithStringArrayMandatoryAndEmptyProperty>(xml);

			Assert.AreEqual(0, serializable.MyArray.Length);
		}

		#endregion Load From Xml
	}
}
