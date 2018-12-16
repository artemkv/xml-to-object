using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.ControllingStructure
{
	public class PersonA : IXPathSerializable
	{
		[MappingXPath("/artist/personal_data/given_name")]
		public string FirstName { get; set; }
		[MappingXPath("/artist/personal_data/family_name")]
		public string LastName { get; set; }
	}

	public class PersonB : IXPathSerializable
	{
		[MappingXPath("/person[@profession='artist' and @country='UK']/personal_data/given_name")]
		public string FirstName { get; set; }
		[MappingXPath("/person[@profession='artist' and @country='UK']/personal_data/family_name")]
		public string LastName { get; set; }
	}

	public class PersonC : IXPathSerializable
	{
		[MappingXPath("/person[@profession='artist']/personal_data", attributeName: "given_name")]
		public string FirstName { get; set; }
		[MappingXPath("/person[@profession='artist']/personal_data", attributeName: "family_name")]
		public string LastName { get; set; }
	}

	public class PersonD : IXPathSerializable
	{
		[MappingXPath("/name")]
		public string FirstName { get; set; }
		[MappingXPath("/name", attributeName: "last")]
		public string LastName { get; set; }
	}

	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestToXmlA()
		{
			PersonA person = new PersonA()
			{
				FirstName = "Hugh",
				LastName = "Laurie"
			};

			string xml = person.ToXml();

			var doc = XDocument.Parse(xml);
		
			var elementFirstName = doc.XPathSelectElement("/artist/personal_data/given_name");
			Assert.AreEqual("Hugh", elementFirstName.Value);

			var elementLastName = doc.XPathSelectElement("/artist/personal_data/family_name");
			Assert.AreEqual("Laurie", elementLastName.Value);
		}

		[Test]
		public void TestToXmlB()
		{
			PersonB person = new PersonB()
			{
				FirstName = "Hugh",
				LastName = "Laurie"
			};

			string xml = person.ToXml();

			var doc = XDocument.Parse(xml);

			var elementFirstName = doc.XPathSelectElement("/person[@profession='artist' and @country='UK']/personal_data/given_name");
			Assert.AreEqual("Hugh", elementFirstName.Value);

			var elementLastName = doc.XPathSelectElement("/person[@profession='artist' and @country='UK']/personal_data/family_name");
			Assert.AreEqual("Laurie", elementLastName.Value);
		}

		[Test]
		public void TestToXmlC()
		{
			PersonC person = new PersonC()
			{
				FirstName = "Hugh",
				LastName = "Laurie"
			};

			string xml = person.ToXml();

			var doc = XDocument.Parse(xml);

			var attributeFirstName = doc.XPathSelectElement("/person[@profession='artist']/personal_data").Attribute("given_name");
			Assert.AreEqual("Hugh", attributeFirstName.Value);

			var attributeLastName = doc.XPathSelectElement("/person[@profession='artist']/personal_data").Attribute("family_name");
			Assert.AreEqual("Laurie", attributeLastName.Value);
		}

		[Test]
		public void TestToXmlD()
		{
			PersonD person = new PersonD()
			{
				FirstName = "Hugh",
				LastName = "Laurie"
			};

			string xml = person.ToXml();

			var doc = XDocument.Parse(xml);

			var elementFirstName = doc.XPathSelectElement("/name");
			Assert.AreEqual("Hugh", elementFirstName.Value);

			var attributeLastName = doc.XPathSelectElement("/name").Attribute("last");
			Assert.AreEqual("Laurie", attributeLastName.Value);
		}

		[TestCase]
		public void TestLoadFromXmlA()
		{
			var xml = @"<artist>
  <personal_data>
    <given_name>Hugh</given_name>
    <family_name>Laurie</family_name>
  </personal_data>
</artist>";

			PersonA person = XmlSerialization.LoadFromXml<PersonA>(xml);

			Assert.AreEqual("Hugh", person.FirstName);
			Assert.AreEqual("Laurie", person.LastName);
		}

		[TestCase]
		public void TestLoadFromXmlB()
		{
			var xml = @"<person profession='artist' country='UK'>
  <personal_data>
    <given_name>Hugh</given_name>
    <family_name>Laurie</family_name>
  </personal_data>
</person>";

			PersonB person = XmlSerialization.LoadFromXml<PersonB>(xml);

			Assert.AreEqual("Hugh", person.FirstName);
			Assert.AreEqual("Laurie", person.LastName);
		}

		[TestCase]
		public void TestLoadFromXmlC()
		{
			var xml = @"<person profession='artist'>
  <personal_data given_name='Hugh' family_name='Laurie' />
</person>";

			PersonC person = XmlSerialization.LoadFromXml<PersonC>(xml);

			Assert.AreEqual("Hugh", person.FirstName);
			Assert.AreEqual("Laurie", person.LastName);
		}

		[TestCase]
		public void TestLoadFromXmlD()
		{
			var xml = @"<name last='Laurie'>Hugh</name>";

			PersonD person = XmlSerialization.LoadFromXml<PersonD>(xml);

			Assert.AreEqual("Hugh", person.FirstName);
			Assert.AreEqual("Laurie", person.LastName);
		}
	}
}
