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
	/// Checks that properties that are mapped as XmlMandatory always generate Xml elements, even if null.
	/// Checks that properties that are not mapped as XmlMandatory do not generate Xml, if null.
	/// Checks that deserializing optional property resets it's value to the dafault one if the value cannot be found in Xml.
	/// Checks that deserializing mandatory property throws an exception if the value cannot be found in Xml.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_Mapping_IsXmlMandatory_Test
	{
		#region To Xml

		[TestCase]
		public void TestStringOptionalPropertyToXml()
		{
			var serializable = new WithStringProperty()
			{
				AnyString = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/String/AnyString");

			// Check that element is NOT created
			Assert.IsNull(element);
		}

		[TestCase]
		public void TestEmptyStringMandatoryPropertyToXml()
		{
			var serializable = new WithStringMandatoryProperty()
			{
				AnyString = ""
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/String/AnyString");

			// Check that element is empty, but still created
			Assert.AreEqual("", element.Value);
		}

		[TestCase]
		public void TestStringMandatoryPropertyToXml()
		{
			var serializable = new WithStringMandatoryProperty()
			{
				AnyString = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/String/AnyString");

			// Check that element is empty, but still created
			Assert.AreEqual("", element.Value);
		}

		[TestCase]
		public void TestStringMandatoryPropertyInAttributeToXml()
		{
			var serializable = new WithStringMandatoryAttribute()
			{
				AnyString = null
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var element = doc.XPathSelectElement("/String/AnyString");

			// Check that attribute is created
			Assert.NotNull(element.Attribute("value"));
		}

		#endregion

		#region Load From Xml

		[TestCase]
		public void TestStringOptionalPropertyLoadFromXml()
		{
			var xml = @"<String></String>";

			var serializable = XmlSerialization.LoadFromXml<WithStringProperty>(xml);

			Assert.AreEqual(null, serializable.AnyString);
		}

		[TestCase]
		public void TestStringArrayOptionalPropertyLoadFromXml()
		{
			var xml = @"<String></String>";

			var serializable = XmlSerialization.LoadFromXml<WithStringArrayProperty>(xml);

			Assert.AreEqual(null, serializable.AnyStrings);
		}

		[TestCase]
		[ExpectedException(typeof(SerializationException))]
		public void TestStringMandatoryPropertyLoadFromXml()
		{
			var xml = @"<String></String>";

			var serializable = XmlSerialization.LoadFromXml<WithStringMandatoryProperty>(xml);
		}

		[TestCase]
		[ExpectedException(typeof(SerializationException))]
		public void TestNumericPropertyLoadFromXml()
		{
			var xml = @"<Numeric></Numeric>";

			var serializable = XmlSerialization.LoadFromXml<WithNumericMandatoryProperty>(xml, provider: CultureInfo.InvariantCulture.NumberFormat);
		}

		[TestCase]
		[ExpectedException(typeof(SerializationException))]
		public void TestStringMandatoryPropertyInAttributeLoadFromXml()
		{
			var xml = @"<String>
  <AnyString/>
</String>";

			var serializable = XmlSerialization.LoadFromXml<WithStringMandatoryAttribute>(xml);
		}

		#endregion
	}
}
