using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using NUnit.Framework;
using System.Globalization;

namespace Artemkv.Transformation.XmlToObject.Test
{
	/// <summary>
	/// Checks that inherited fields/properties are treates the same as own ones.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_Mapping_Inherited_Test
	{
		#region To Xml

		[TestCase]
		public void TestInheritedPropertyToXml()
		{
			var serializable = new WithStringPropertyHeir()
			{
				AnyString = "MyString"
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/String/AnyString");
			Assert.AreEqual("MyString", element.Value);
		}

		[TestCase]
		public void TestInheritedFieldToXml()
		{
			var serializable = new WithStringFieldHeir()
			{
				AnyString = "MyString"
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/String/AnyString");
			Assert.AreEqual("MyString", element.Value);
		}

		#endregion

		#region Load From Xml

		[TestCase]
		public void TestInheritedPropertyLoadFromXml()
		{
			var xml = @"<String><AnyString>MyString</AnyString></String>";

			var serializable = XmlSerialization.LoadFromXml<WithStringPropertyHeir>(xml);

			Assert.AreEqual("MyString", serializable.AnyString);
		}

		[TestCase]
		public void TestInheritedFieldLoadFromXml()
		{
			var xml = @"<String><AnyString>MyString</AnyString></String>";

			var serializable = XmlSerialization.LoadFromXml<WithStringFieldHeir>(xml);

			Assert.AreEqual("MyString", serializable.AnyString);
		}

		#endregion
	}
}
