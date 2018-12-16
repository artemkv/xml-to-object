using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.MinimalXmlStructure
{
	[MinimalXmlStructureAttribute("/artist/personal_data")]
	public class Person : IXPathSerializable
	{
		[MappingXPath("/artist/personal_data/given_name")]
		public string FirstName { get; set; }

		[MappingXPath("/artist/personal_data/family_name")]
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

			var doc = XDocument.Parse(xml);

			var elementFirstName = doc.XPathSelectElement("/artist/personal_data");
			Assert.IsNotNull(elementFirstName);
		}
	}
}
