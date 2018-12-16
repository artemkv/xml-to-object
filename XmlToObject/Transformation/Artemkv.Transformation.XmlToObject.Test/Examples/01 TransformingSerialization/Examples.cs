using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.TransformingSerialization
{
	public class Person : IXPathSerializable
	{
		[MappingXPath("/Object/Field[@name='FirstName']")]
		public string FirstName { get; set; }
		[MappingXPath("/Object/Field[@name='LastName']")]
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
			string xml = @"<Object>
  <Field name='FirstName'>Hugh</Field>
  <Field name='LastName'>Laurie</Field>
</Object>";

			Person person = XmlSerialization.LoadFromXml<Person>(xml);
		}
	}
}
