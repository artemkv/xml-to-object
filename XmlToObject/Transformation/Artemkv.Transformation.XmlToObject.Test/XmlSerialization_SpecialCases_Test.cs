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
	public class XmlSerialization_SpecialCases_Test
	{
		[TestCase]
		public void TestSameElementsToXml()
		{
			var serializable = new With2GuidPropertiesSameElement()
			{
				One = new Guid("6BEE371D-D031-4106-ACB9-5FE0C4E1F384"),
				Two = new Guid("ACB2A5B7-89A8-4426-93D5-1F8610C8B6A5"),
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elements = doc.XPathSelectElements("/Guid").ToArray();

			Assert.AreEqual(1, elements.Length);
			Assert.AreEqual("{acb2a5b7-89a8-4426-93d5-1f8610c8b6a5}", elements[0].Value);
		}

		[TestCase]
		public void TestSpecialCharachtersToXml()
		{
			var serializable = new WithStringAsElementAndAsAttribute()
			{
				InElement = "<A/>",
				InAttribute = "<B/>"
			};

			string xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/WithStringAsElementAndAsAttribute/MyElement");

			Assert.AreEqual("<A/>", element.Value);
			Assert.AreEqual("<B/>", element.Attribute("MyAttribute").Value);
		}

		[TestCase]
		public void TestUrlInAttributeToXml()
		{
			var serializable = new WithUrlInAttribute();
			var xml = serializable.ToXml();
		}
	}
}
