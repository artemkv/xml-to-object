using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Artemkv.Transformation.XmlToObject;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.SerializableClass
{
	public class Person : IXPathSerializable
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestToXml()
		{
			Person person = new Person()
			{
				FirstName = "Hugh",
				LastName = "Laurie"
			};

			string xml = person.ToXml();
		}

		[Test]
		public void LoadFromXml()
		{
			string xml = @"<Person>
  <FirstName>Hugh</FirstName>
  <LastName>Laurie</LastName>
</Person>";

			Person person = XmlSerialization.LoadFromXml<Person>(xml);
		}
	}
}
