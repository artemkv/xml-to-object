using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.Arrays
{
	public class Person : IXPathSerializable
	{
		public string FirstName { get; set; }
		[ArrayItemElementName("MiddleName")]
		public string[] MiddleNames { get; set; }
		public string LastName { get; set; }
	}

	public class Group : IXPathSerializable
	{
		public Person[] People { get; set; }
	}

	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestPersonToXml()
		{
			Person person = new Person()
			{
				FirstName = "Jose",
				MiddleNames = new[] { "Antonio", "Alvarez", "Lopez" },
				LastName = "Rodriguez"
			};

			string xml = person.ToXml();
		}

		[Test]
		public void TestGroupToXml()
		{
			Group group = new Group()
			{
				People = new Person[] 
				{
					new Person()
					{
						FirstName = "Jose",
						MiddleNames = new [] { "Antonio", "Alvarez", "Lopez" },
						LastName = "Rodriguez"
					},
					new Person()
					{
						FirstName = "Rasmus",
						MiddleNames = new string[] { "Christian" },
						LastName = "Nørgaard"
					}
				}
			};

			string xml = group.ToXml();
		}
	}
}
