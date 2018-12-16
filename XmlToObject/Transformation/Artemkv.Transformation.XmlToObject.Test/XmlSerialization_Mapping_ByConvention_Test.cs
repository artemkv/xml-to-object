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
	/// Checks that properties/fields can be processed even if not explicitly mapped. 
	/// Class name and field/property name is used to generate xpaths.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_Mapping_ByConvention_Test
	{
		#region To Xml

		[TestCase]
		public void TestPropertyToXmlByConvention()
		{
			var serializable = new WithStringPropertyByConvention()
			{
				AnyString = "MyString1"
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/WithStringPropertyByConvention/AnyString");
			Assert.AreEqual("MyString1", element.Value);
		}

		[TestCase]
		public void TestFieldToXmlByConvention()
		{
			var serializable = new WithStringFieldByConvention()
			{
				AnyString = "MyString1"
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/WithStringFieldByConvention/AnyString");
			Assert.AreEqual("MyString1", element.Value);
		}

		[TestCase]
		public void TesByteArrayPropertyToXmlByConvention()
		{
			var serializable = new WithNumericArrayByConvention()
			{
				AnyBytes = new byte[] { Byte.MinValue, Byte.MaxValue }
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);

			var doc = XDocument.Parse(xml);
			var elements = doc.XPathSelectElements("/WithNumericArrayByConvention/AnyBytes/Item").ToArray();
			Assert.AreEqual(Byte.MinValue.ToString("", CultureInfo.InvariantCulture.NumberFormat), elements[0].Value);
			Assert.AreEqual(Byte.MaxValue.ToString("", CultureInfo.InvariantCulture.NumberFormat), elements[1].Value);
		}

		#endregion

		#region Load From Xml

		[TestCase]
		public void TestPropertyLoadFromXmlByConvention()
		{
			var xml = @"<WithStringPropertyByConvention>
  <AnyString>MyString1</AnyString>
</WithStringPropertyByConvention>";

			var serializable = XmlSerialization.LoadFromXml<WithStringPropertyByConvention>(xml);

			Assert.AreEqual("MyString1", serializable.AnyString);
		}

		[TestCase]
		public void TestFieldLoadFromXmlByConvention()
		{
			var xml = @"<WithStringFieldByConvention>
  <AnyString>MyString1</AnyString>
</WithStringFieldByConvention>";

			var serializable = XmlSerialization.LoadFromXml<WithStringFieldByConvention>(xml);

			Assert.AreEqual("MyString1", serializable.AnyString);
		}

		[TestCase]
		public void TesByteArrayPropertyLoadFromXml()
		{
			var xml = @"<WithNumericArrayByConvention>
  <AnyBytes>
    <Item>0</Item>
    <Item>255</Item>
  </AnyBytes>
</WithNumericArrayByConvention>";

			var serializable = XmlSerialization.LoadFromXml<WithNumericArrayByConvention>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(Byte.MinValue, serializable.AnyBytes[0]);
			Assert.AreEqual(Byte.MaxValue, serializable.AnyBytes[1]);
		}

		#endregion
	}
}
