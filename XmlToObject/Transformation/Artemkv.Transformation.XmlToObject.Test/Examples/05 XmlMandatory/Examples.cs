using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.XmlMandatory
{
	public class Person : IXPathSerializable
	{
		public string FirstName { get; set; }

		[XmlMandatory]
		public string LastName { get; set; }
	}

	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestToXmlA()
		{
			Person person = new Person();

			string xml = person.ToXml();

			XDocument doc = XDocument.Parse(xml);

			XElement elementFirstName = doc.XPathSelectElement("/Person/FirstName");
			Assert.IsNull(elementFirstName);

			XElement elementLastName = doc.XPathSelectElement("/Person/LastName");
			Assert.AreEqual("", elementLastName.Value);
		}

		[TestCase]
		public void TestLoadFromXmlGood()
		{
			string xml = @"<Person>
  <LastName></LastName>
</Person>";

			Person person = XmlSerialization.LoadFromXml<Person>(xml);

			Assert.AreEqual(null, person.FirstName);
			Assert.AreEqual(null, person.LastName);
		}

		[TestCase, ExpectedException(typeof(SerializationException))]
		public void TestLoadFromXmlBad()
		{
			string xml = @"<Person></Person>";

			Person person = XmlSerialization.LoadFromXml<Person>(xml);
		}
	}
}
