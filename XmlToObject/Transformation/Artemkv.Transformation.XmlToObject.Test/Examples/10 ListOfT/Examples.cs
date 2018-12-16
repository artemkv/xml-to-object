using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.ListOfT
{
	public class Person : IXPathSerializable
	{
		public string FirstName { get; set; }
		[ValueConvertor(typeof(ListOfTValueConverter)), SerializeAsXmlFragment]
		public List<string> MiddleNames { get; set; }
		public string LastName { get; set; }
	}

	public class Group : IXPathSerializable
	{
		[ValueConvertor(typeof(ListOfTValueConverter)), SerializeAsXmlFragment]
		public List<Person> People { get; set; }
	}

	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestGroupToXml()
		{
			Group group = new Group()
			{
				People = new List<Person>()
				{
					new Person()
					{
						FirstName = "Jose",
						MiddleNames = new List<string> { "Antonio", "Alvarez", "Lopez" },
						LastName = "Rodriguez"
					},
					new Person()
					{
						FirstName = "Rasmus",
						MiddleNames = new List<string> { "Christian" },
						LastName = "Nørgaard"
					}
				}
			};

			string xml = group.ToXml();
		}
	}
}
