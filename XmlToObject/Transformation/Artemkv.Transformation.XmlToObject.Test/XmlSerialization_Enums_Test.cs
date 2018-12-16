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
	public class XmlSerialization_Enums_Test
	{
		[TestCase]
		public void TestEnumPropertyAsStringToXml()
		{
			var serializable = new WithEnumPropertyAsString()
			{
				Season = Seasons.Summer
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("Seasons/Season");

			Assert.AreEqual("Summer", elementTrue.Value);
		}

		[TestCase]
		public void TestEnumPropertyAsStringFromXml()
		{
			var xml = @"<Seasons>
  <Season>Summer</Season>
</Seasons>";

			var serializable = XmlSerialization.LoadFromXml<WithEnumPropertyAsString>(xml);

			Assert.AreEqual(Seasons.Summer, serializable.Season);
		}

		[TestCase]
		public void TestEnumPropertyAsNumberToXml()
		{
			var serializable = new WithEnumPropertyAsNumber()
			{
				Season = Seasons.Summer
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("Seasons/Season");

			Assert.AreEqual("2", elementTrue.Value);
		}

		[TestCase]
		public void TestEnumPropertyAsNumberFromXml()
		{
			var xml = @"<Seasons>
  <Season>2</Season>
</Seasons>";

			var serializable = XmlSerialization.LoadFromXml<WithEnumPropertyAsNumber>(xml);

			Assert.AreEqual(Seasons.Summer, serializable.Season);
		}

		[TestCase]
		public void TestFlagsPropertyAsStringToXml()
		{
			var serializable = new WithFlagsPropertyAsString()
			{
				Features = PhoneFeatures.GPRS | PhoneFeatures.TouchScreen
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("/Phone/Features");

			Assert.AreEqual("TouchScreen, GPRS", elementTrue.Value);
		}

		[TestCase]
		public void TestFlagsPropertyAsStringFromXml()
		{
			var xml = @"<Phone>
  <Features>TouchScreen, GPRS</Features>
</Phone>";

			var serializable = XmlSerialization.LoadFromXml<WithFlagsPropertyAsString>(xml);

			Assert.AreEqual(PhoneFeatures.GPRS | PhoneFeatures.TouchScreen, serializable.Features);
		}

		[TestCase]
		public void TestFlagsPropertyAsNumberToXml()
		{
			var serializable = new WithFlagsPropertyAsNumber()
			{
				Features = PhoneFeatures.GPRS | PhoneFeatures.TouchScreen
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("/Phone/Features");

			Assert.AreEqual("5", elementTrue.Value);
		}

		[TestCase]
		public void TestFlagsPropertyAsNumberFromXml()
		{
			var xml = @"<Phone>
  <Features>5</Features>
</Phone>";

			var serializable = XmlSerialization.LoadFromXml<WithFlagsPropertyAsNumber>(xml);

			Assert.AreEqual(PhoneFeatures.GPRS | PhoneFeatures.TouchScreen, serializable.Features);
		}

		[TestCase]
		public void TestEnumPropertyNullableAsStringToXml()
		{
			var serializable = new WithEnumPropertyNullableAsString()
			{
				Season = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("Seasons/Season");

			Assert.AreEqual("", elementTrue.Value);
		}

		[TestCase]
		public void TestEnumPropertyNullableAsStringFromXml()
		{
			var xml = @"<Seasons>
  <Season></Season>
</Seasons>";

			var serializable = XmlSerialization.LoadFromXml<WithEnumPropertyNullableAsString>(xml);

			Assert.AreEqual(null, serializable.Season);
		}

		[TestCase]
		public void TestEnumPropertyNullableAsNumberToXml()
		{
			var serializable = new WithEnumPropertyNullableAsNumber()
			{
				Season = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("Seasons/Season");

			Assert.AreEqual("", elementTrue.Value);
		}

		[TestCase]
		public void TestEnumPropertyNullableAsNumberFromXml()
		{
			var xml = @"<Seasons>
  <Season></Season>
</Seasons>";

			var serializable = XmlSerialization.LoadFromXml<WithEnumPropertyNullableAsNumber>(xml);

			Assert.AreEqual(null, serializable.Season);
		}

		[TestCase]
		public void TestFlagsPropertyNullableAsStringToXml()
		{
			var serializable = new WithFlagsPropertyNullableAsString()
			{
				Features = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("/Phone/Features");

			Assert.AreEqual("", elementTrue.Value);
		}

		[TestCase]
		public void TestFlagsPropertyNullableAsStringFromXml()
		{
			var xml = @"<Phone>
  <Features></Features>
</Phone>";

			var serializable = XmlSerialization.LoadFromXml<WithFlagsPropertyNullableAsString>(xml);

			Assert.AreEqual(null, serializable.Features);
		}

		[TestCase]
		public void TestFlagsPropertyNullableAsNumberToXml()
		{
			var serializable = new WithFlagsPropertyNullableAsNumber()
			{
				Features = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("/Phone/Features");

			Assert.AreEqual("", elementTrue.Value);
		}

		[TestCase]
		public void TestFlagsPropertyNullableAsNumberFromXml()
		{
			var xml = @"<Phone>
  <Features></Features>
</Phone>";

			var serializable = XmlSerialization.LoadFromXml<WithFlagsPropertyNullableAsNumber>(xml);

			Assert.AreEqual(null, serializable.Features);
		}
	}
}
