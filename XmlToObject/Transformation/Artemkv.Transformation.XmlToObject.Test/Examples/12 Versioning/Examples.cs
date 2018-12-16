using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.Versioning
{
	public class Address : IXPathSerializable
	{
		public string Street { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }
		public string Country { get; set; }
	}

	public class Person : IXPathSerializable
	{
		// Default version = 0
		public string FirstName { get; set; }
		// Default version = 0
		public string LastName { get; set; }

		[NotForSerialization]
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		[SerializedFromVersion(1)]
		public string Address { get; set; }

		[SerializedFromVersion(2)]
		public Address AddressFull { get; set; }
	}
	
	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestToXml()
		{
			Person person = new Person()
			{
				FirstName = "Clovis",
				LastName = "Labossière"
			};
			string xml = person.ToXml();
		}

		[Test]
		public void TestToXmlWithVersions()
		{
			Person person = new Person()
			{
				FirstName = "Clovis",
				LastName = "Labossière",
				Address = "37, Avenue Millies Lacroix 95600 EAUBONNE France",
				AddressFull = new Address()
				{
					Street = "37, Avenue Millies Lacroix",
					City = "EAUBONNE",
					ZipCode = "95600",
					Country = "France"					
				}
			};
			string xml0 = person.ToXml();
			string xml1 = person.ToXml(version: 1);
			string xml2 = person.ToXml(version: 2);

			Person deserialized0 = XmlSerialization.LoadFromXml<Person>(xml0);

			Person deserialized1 = XmlSerialization.LoadFromXml<Person>(xml1, version: 1);

			Person deserialized2 = XmlSerialization.LoadFromXml<Person>(xml2, version: 2);

			Person deserialized2from0 = XmlSerialization.LoadFromXml<Person>(xml0, version: Int32.MaxValue);
		}

		[Test]
		public void TestToXmlFiltered()
		{
			Person person = new Person()
			{
				FirstName = "Clovis",
				LastName = "Labossière",
				Address = "37, Avenue Millies Lacroix 95600 EAUBONNE France",
				AddressFull = new Address()
				{
					Street = "37, Avenue Millies Lacroix",
					City = "EAUBONNE",
					ZipCode = "95600",
					Country = "France"
				}
			};

			MemberFilter<Person> filter = new MemberFilter<Person>()
			{
				x => x.LastName,
				
				x => x.AddressFull,
				x => x.AddressFull.Country
			};

			string xmlFiltered = person.ToXml(filter, version: Int32.MaxValue);
		}
	}
}
