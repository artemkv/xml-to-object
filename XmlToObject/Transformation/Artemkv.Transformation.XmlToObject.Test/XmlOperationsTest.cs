using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Artemkv.Transformation.XmlToObject.Test
{
	[TestFixture]
	public class XmlOperationsTest
	{
		[TestCase]
		public void TestGetOrCreateElement()
		{
			string xml = @"<purchaseOrder orderDate='2012-11-13'>
  <shipTo country='US'>
    <name>Alice Smith</name>
    <street>123 Maple Street</street>
    <city>Mill Valley</city>
    <state>CA</state>
    <zip>90952</zip>
  </shipTo>
  <billTo/>
</purchaseOrder>";

			var doc = XDocument.Parse(xml);

			var element = XmlOperations.GetOrCreateElement(doc, "/purchaseOrder/billTo/street", null);
			Assert.NotNull(element);
			Assert.AreEqual("", element.Value);

			Assert.NotNull(doc.XPathSelectElement("/purchaseOrder/billTo/street"));
		}
	}
}