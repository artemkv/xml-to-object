using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.AsXmlFragment
{
	public class Envelop : IXPathSerializable
	{
		[MappingXPath("/Root/Envelop")]
		[SerializeAsXmlFragment]
		public string Message { get; set; }
	}

	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestToXml()
		{
			Envelop envelop = new Envelop()
			{
				Message = "<Message>Hello world</Message>"
			};

			string xml = envelop.ToXml();

			XDocument doc = XDocument.Parse(xml);

			XElement elementInner = doc.XPathSelectElement("/Root/Envelop/Message");
			Assert.AreEqual("Hello world", elementInner.Value);
		}

		[TestCase]
		public void TestLoadFromXml()
		{
			var xml = @"<Root>
  <Envelop>
    <Message>Hello world</Message>
  </Envelop>
</Root>";

			Envelop envelop = XmlSerialization.LoadFromXml<Envelop>(xml);
			Assert.AreEqual("<Message>Hello world</Message>", envelop.Message);
		}
	}
}
