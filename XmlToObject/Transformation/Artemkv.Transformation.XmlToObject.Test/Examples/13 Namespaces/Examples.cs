using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.Namespaces
{
	[NamespacePrefix("t", "http://Transport")]
	public class Envelop : IXPathSerializable
	{
		[MappingXPath("/t:Envelop/t:Message")]
		public Object Message { get; set; }

		[MappingXPath("/t:Envelop", "t:created")]
		public DateTime Created { get; set; }
	}

	[NamespacePrefix("b", "http://Business")]
	public class Person : IXPathSerializable
	{
		[MappingXPath("/b:Person", "t:id")]
		public Int32 Id { get; set; }
		[MappingXPath("/b:Person/b:FirstName")]
		public string FirstName { get; set; }
		[MappingXPath("/b:Person/b:LastName")]
		public string LastName { get; set; }
	}

	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestToXml()
		{
			Envelop envelop = new Envelop()
			{
				Created = new DateTime(2012, 11, 18, 13, 0, 0),
				Message = new Person()
				{
					Id = 123,
					FirstName = "Hugh",
					LastName = "Laurie"
				}
			};

			string xml = envelop.ToXml(emitTypeInfo: true);

			var doc = XDocument.Parse(xml);

			var resolver = new XmlNamespaceManager(new NameTable());
			resolver.AddNamespace("t", "http://Transport");
			resolver.AddNamespace("b", "http://Business");

			var elementInner = doc.XPathSelectElement("/t:Envelop/t:Message/b:Person/b:LastName", resolver);
			Assert.AreEqual("Laurie", elementInner.Value);
		}

		[TestCase]
		public void TestLoadFromXml()
		{
			string xml = @"<Envelop p1:created='2012-11-18T13:00:00.0000000' xmlns:p1='http://Transport' xmlns='http://Transport'>
  <p1:Message p3:__type='Artemkv.Transformation.XmlToObject.Test.Examples.Namespaces.Person, Artemkv.Transformation.XmlToObject.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' xmlns:p3='http://xmltoobject.codeplex.com'>
    <Person p1:id='123' xmlns='http://Business'>
      <FirstName p3:__type='System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'>Hugh</FirstName>
      <LastName p3:__type='System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'>Laurie</LastName>
    </Person>
  </p1:Message>
</Envelop>";

			Envelop envelop = XmlSerialization.LoadFromXml<Envelop>(xml);
			Assert.AreEqual("Laurie", (envelop.Message as Person).LastName);
		}
	}
}
