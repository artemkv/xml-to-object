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
	/// Checks that all properties can be serialized to Xml and deserialized from Xml using value converter.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_ValueConverter_Test
	{
		[TestCase]
		public void TestBooleanWithConverterToXml()
		{
			var serializable = new WithBooleanAsYesNoField()
			{
				BoolTrue = true,
				BoolFalse = false
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("Boolean/BoolTrue");
			var elementFalse = doc.XPathSelectElement("Boolean/BoolFalse");

			Assert.AreEqual("Yes", elementTrue.Value);
			Assert.AreEqual("No", elementFalse.Value);
		}

		[TestCase]
		public void TestBooleanWithConverterFromXml()
		{
			var xml = @"<Boolean>
  <BoolTrue>Yes</BoolTrue>
  <BoolFalse>No</BoolFalse>
</Boolean>";

			var serializable = XmlSerialization.LoadFromXml<WithBooleanAsYesNoField>(xml);

			Assert.AreEqual(true, serializable.BoolTrue);
			Assert.AreEqual(false, serializable.BoolFalse);
		}

		[TestCase]
		public void TestBooleanArrayWithConverterToXml()
		{
			var serializable = new WithBooleanAsYesNoArrayField()
			{
				ArrayOfBoolean = new bool[] { true, false, true }
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elements = doc.XPathSelectElements("Boolean/Item").ToArray();

			Assert.AreEqual("Yes", elements[0].Value);
			Assert.AreEqual("No", elements[1].Value);
			Assert.AreEqual("Yes", elements[2].Value);
		}

		[TestCase]
		public void TestBooleanArrayWithConverterFromXml()
		{
			var xml = @"<Boolean>
  <Item>Yes</Item>
  <Item>No</Item>
  <Item>Yes</Item>
</Boolean>";

			var serializable = XmlSerialization.LoadFromXml<WithBooleanAsYesNoArrayField>(xml);

			Assert.AreEqual(true, serializable.ArrayOfBoolean[0]);
			Assert.AreEqual(false, serializable.ArrayOfBoolean[1]);
			Assert.AreEqual(true, serializable.ArrayOfBoolean[2]);
		}
	}
}
