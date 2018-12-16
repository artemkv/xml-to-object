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
	/// Checks that fields/properties are ignored when NotForSerializationAttribute is applied.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_Selective_Test
	{
		[TestCase]
		public void TestSelectiveToXml()
		{
			var serializable = new SelectiveSerilalizationField()
			{
				BoolTrue = true,
				BoolFalse = false
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementTrue = doc.XPathSelectElement("Boolean/BoolTrue");

			Assert.AreEqual("True", elementTrue.Value);

			Assert.False(xml.Contains("BoolFalse"), "BoolFalse should have been ignored");
		}

		[TestCase]
		public void TestSelectiveLoadFromXml()
		{
			var xml = @"<Boolean>
  <BoolTrue>True</BoolTrue>
  <BoolFalse>True</BoolFalse>
</Boolean>";

			var serializable = XmlSerialization.LoadFromXml<SelectiveSerilalizationField>(xml);

			Assert.AreEqual(true, serializable.BoolTrue);
			Assert.AreEqual(false, serializable.BoolFalse);
		}
	}
}
