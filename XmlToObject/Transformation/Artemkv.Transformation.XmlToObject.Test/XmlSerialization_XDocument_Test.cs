using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using NUnit.Framework;
using System.Globalization;
using System.Xml;

namespace Artemkv.Transformation.XmlToObject.Test
{
	/// <summary>
	/// Checks that fields/properties are ignored when NotForSerializationAttribute is applied.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_XDocument_Test
	{
		[TestCase]
		public void TestXDocumentToXml()
		{
			var serializable = new WithFloatPointNumericProperty()
			{
				AnySingle = Single.MaxValue,
				AnyDouble = Single.MinValue
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);
			var xmlDoc = serializable.ToXmlDocument(provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(xml, xmlDoc.ToString());
		}

		[TestCase]
		public void TestXDocumentLoadFromXml()
		{
			var xml = @"<Numeric>
  <AnySingle>340,282,300,000,000,000,000,000,000,000,000,000,000.00</AnySingle>
  <AnyDouble>-340,282,346,638,529,000,000,000,000,000,000,000,000.00</AnyDouble>
</Numeric>";

			var deserializedFromString = XmlSerialization.LoadFromXml<WithFloatPointNumericProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);
			var deserializedFromDocument = XmlSerialization.LoadFromXmlDocument<WithFloatPointNumericProperty>(XDocument.Parse(xml), provider: CultureInfo.InvariantCulture.NumberFormat);

			Assert.AreEqual(deserializedFromString.AnySingle, deserializedFromDocument.AnySingle);
			Assert.AreEqual(deserializedFromString.AnyDouble, deserializedFromDocument.AnyDouble);
		}

		[TestCase]
		public void TestXDocumentWithNamespacesToXml()
		{
			var serializable = new WithGuidPropertyWithNamespace()
			{
				AnyGuid = Guid.NewGuid()
			};

			var xml = serializable.ToXml();

			var deserialized = XmlSerialization.LoadFromXml<WithGuidPropertyWithNamespace>(xml);

			Assert.AreEqual(serializable.AnyGuid, deserialized.AnyGuid);
		}
	}
}
