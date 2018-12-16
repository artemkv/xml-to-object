using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Artemkv.Transformation.XmlToObject;
using System.Globalization;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.BuiltinTypes
{
public class Address : IXPathSerializable
{
	public string Street { get; set; }
	public string City { get; set; }
	public string ZipCode { get; set; }
	public string Country { get; set; }
}

public class Employee : IXPathSerializable
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime DateOfBirth { get; set; }
	public bool IsMale { get; set; }
	public Decimal Salary { get; set; }
	public Address HomeAddress { get; set; }
	public string[] Skills { get; set; }
}

	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestToXml()
		{
			Employee employee = new Employee()
			{
				FirstName = "Hugh",
				LastName = "Laurie",
				DateOfBirth = new DateTime(1959, 6, 11),
				IsMale = true,
				Salary = 5000000m,
				HomeAddress = new Address()
				{
					Street = "Hamilton Hodell",
					City = "London",
					ZipCode = "W1W 8SR",
					Country = "UK"
				},
				Skills = new string[]
				{
					"actor", "voice artist", "comedian", "writer", "musician", "recording artist", "director"
				}
			};

			string xml = employee.ToXml();
		}

		[Test]
		public void LoadFromXml()
		{
			string xml = @"<Employee>
  <FirstName>Hugh</FirstName>
  <LastName>Laurie</LastName>
  <DateOfBirth></DateOfBirth>
  <IsMale>True</IsMale>
  <Salary>5000000</Salary>
  <HomeAddress>
    <Address>
      <Street>Hamilton Hodell</Street>
      <City>London</City>
      <ZipCode>W1W 8SR</ZipCode>
      <Country>UK</Country>
    </Address>
  </HomeAddress>
  <Skills>
    <Item>actor</Item>
    <Item>voice artist</Item>
    <Item>comedian</Item>
    <Item>writer</Item>
    <Item>musician</Item>
    <Item>recording artist</Item>
    <Item>director</Item>
  </Skills>
</Employee>";

			Employee employee = XmlSerialization.LoadFromXml<Employee>(xml);
		}
	}
}
