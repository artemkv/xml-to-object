using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Artemkv.Transformation.XmlToObject.Test
{
	[TestFixture]
	public class XmlSerialization_EmitTypeInfo_Test
	{
		[TestCase]
		public void TestEmitTypeInfoNormally()
		{
			var serializable = new WithCharProperty()
			{
				AnyChar = 'X'
			};

			string xml = serializable.ToXml(true);

			var doc = XDocument.Parse(xml);
			Assert.AreEqual(typeof(char).AssemblyQualifiedName, doc.XPathSelectElement("/Char/AnyChar")
				.Attribute(@"{http://xmltoobject.codeplex.com}__type").Value);

			var deserialized = XmlSerialization.LoadFromXml<WithCharProperty>(xml);

			Assert.AreEqual(serializable.AnyChar, deserialized.AnyChar);
		}

		[TestCase]
		public void TestTypeInfoNotEmittedForNull()
		{
			var serializable = new WithStringMandatoryProperty()
			{
				AnyString = null
			};

			string xml = serializable.ToXml(true);

			Assert.IsTrue(!xml.Contains("__type"), "Type info found");

			var deserialized = XmlSerialization.LoadFromXml<WithStringMandatoryProperty>(xml);

			Assert.AreEqual(serializable.AnyString, deserialized.AnyString);
		}

		[TestCase]
		public void TestObjectNoTypeInfoForNullNoError()
		{
			var serializable = new WithObjectProperty()
			{
				AnyObject = null
			};

			string xml = serializable.ToXml(true);

			var deserialized = XmlSerialization.LoadFromXml<WithObjectProperty>(xml);

			Assert.AreEqual(serializable.AnyObject, deserialized.AnyObject);
		}

		[TestCase]
		public void TestObjectMandatoryNoTypeInfoForNullNoError()
		{
			var serializable = new WithObjectMandatoryProperty()
			{
				AnyObject = null
			};

			string xml = serializable.ToXml(true);

			var deserialized = XmlSerialization.LoadFromXml<WithObjectMandatoryProperty>(xml);

			Assert.AreEqual(serializable.AnyObject, deserialized.AnyObject);
		}
	}
}
