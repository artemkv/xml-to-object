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
	public class XmlSerialization_Versions_Test
	{
		[TestCase]
		public void TestDefaultVersionToXml()
		{
			var serializable = new WithVersions()
			{
				Color1 = ConsoleColor.Cyan,
				Color2 = ConsoleColor.Blue
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementColor1 = doc.XPathSelectElement("WithVersions/Color1");
			Assert.AreEqual("Cyan", elementColor1.Value);

			var elementColor2 = doc.XPathSelectElement("WithVersions/Color2");
			Assert.IsNull(elementColor2);
		}

		[TestCase]
		public void TestSpecificVersionToXml()
		{
			var serializable = new WithVersions()
			{
				Color1 = ConsoleColor.Cyan,
				Color2 = ConsoleColor.Blue
			};

			var xml = serializable.ToXml(version: 2);

			var doc = XDocument.Parse(xml);

			var elementColor1 = doc.XPathSelectElement("WithVersions/Color1");
			Assert.AreEqual("Cyan", elementColor1.Value);

			var elementColor2 = doc.XPathSelectElement("WithVersions/Color2");
			Assert.AreEqual("Blue", elementColor2.Value);
		}

		[TestCase]
		public void TestNextVersionToXml()
		{
			var serializable = new WithVersions()
			{
				Color1 = ConsoleColor.Cyan,
				Color2 = ConsoleColor.Blue
			};

			var xml = serializable.ToXml(version: 3);

			var doc = XDocument.Parse(xml);

			var elementColor1 = doc.XPathSelectElement("WithVersions/Color1");
			Assert.AreEqual("Cyan", elementColor1.Value);

			var elementColor2 = doc.XPathSelectElement("WithVersions/Color2");
			Assert.AreEqual("Blue", elementColor2.Value);
		}


		[TestCase]
		public void TestDefaultVersionLoadFromXml()
		{
			var xml = @"<WithVersions>
  <Color1>Cyan</Color1>
</WithVersions>";

			var serializable = XmlSerialization.LoadFromXml<WithVersions>(xml);

			Assert.AreEqual(ConsoleColor.Cyan, serializable.Color1);
			Assert.AreEqual(null, serializable.Color2);
		}

		[TestCase, ExpectedException(typeof(SerializationException))]
		public void TestSpecificVersionLoadFromXml()
		{
			var xml = @"<WithVersions>
  <Color1>Cyan</Color1>
</WithVersions>";

			var serializable = XmlSerialization.LoadFromXml<WithVersions>(xml, version: 2);
		}

		[TestCase]
		public void TestFilteredToXml()
		{
			var serializable = new WithFilter()
			{
				PropertyToSerialize = PhoneFeatures.Camera | PhoneFeatures.GPRS,
				PropertyToSkip = PhoneFeatures.Camera | PhoneFeatures.GPRS,
				FieldToSerialize = PhoneFeatures.Camera | PhoneFeatures.GPRS,
				FieldToSkip = PhoneFeatures.Camera | PhoneFeatures.GPRS,
				InnerObject = new WithFilterInternal()
				{
					StringToSerialize = "Serialized",
					StringToSkip = "Skipped"
				}
			};

			var filter = new MemberFilter<WithFilter>()
			{
				x => x.PropertyToSerialize,
				x => x.FieldToSerialize,
				x => x.InnerObject,
				x => x.InnerObject.StringToSerialize
			};

			var xml = serializable.ToXml(filter);

			var doc = XDocument.Parse(xml);

			var elementProperty = doc.XPathSelectElement("/WithFilter/PropertyToSerialize");
			Assert.AreEqual("Camera, GPRS", elementProperty.Value);

			var elementField = doc.XPathSelectElement("/WithFilter/FieldToSerialize");
			Assert.AreEqual("Camera, GPRS", elementField.Value);

			Assert.IsNull(doc.XPathSelectElement("/WithFilter/PropertyToSkip"));
			Assert.IsNull(doc.XPathSelectElement("/WithFilter/FieldToSkip"));
		}

		[TestCase]
		public void TestFilteredLoadFromXml()
		{
			var xml = @"<WithFilter>
  <FieldToSerialize>Camera, GPRS</FieldToSerialize>
  <PropertyToSerialize>Camera, GPRS</PropertyToSerialize>
  <InnerObject>
    <WithFilterInternal>
      <StringToSerialize>Serialized</StringToSerialize>
    </WithFilterInternal>
  </InnerObject>
</WithFilter>";

			var filter = new MemberFilter<WithFilter>()
			{
				x => x.PropertyToSerialize,
				x => x.FieldToSerialize,
				x => x.InnerObject,
				x => x.InnerObject.StringToSerialize
			};

			var serializable = XmlSerialization.LoadFromXml<WithFilter, WithFilter>(xml, filter);

			Assert.AreEqual(PhoneFeatures.Camera | PhoneFeatures.GPRS, serializable.PropertyToSerialize);
			Assert.AreEqual(PhoneFeatures.Camera | PhoneFeatures.GPRS, serializable.FieldToSerialize);
			Assert.IsNull(serializable.PropertyToSkip);
			Assert.IsNull(serializable.FieldToSkip);

			Assert.AreEqual("Serialized", serializable.InnerObject.StringToSerialize);
			Assert.IsNull(serializable.InnerObject.StringToSkip);
		}
	}
}
