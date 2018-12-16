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
	/// Checks that all array properties can be serialized to Xml.
	/// Happy path scenario: all properties have values that are different from defaults.
	/// 
	/// Formatting is applied.
	/// Min/max values are used when it makes sense.
	/// </summary>
	[TestFixture]
	public class XmlSerialization_BasicTypes_ToXml_ArrayProperties_Test
	{
		#region To Xml

		[TestCase]
		public void TestStringArrayPropertyToXml()
		{
			var serializable = new WithStringArrayProperty()
			{
				AnyStrings = new String[] { "MyString1", "MyString2" }
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var elements = doc.XPathSelectElements("/String/AnyStrings/AnyString").ToArray();
			Assert.AreEqual("MyString1", elements[0].Value);
			Assert.AreEqual("MyString2", elements[1].Value);
		}

		[TestCase]
		public void TestCharArrayPropertyToXml()
		{
			var serializable = new WithCharArrayProperty()
			{
				AnyChars = new Char[] { 'A', 'B' }
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var elements = doc.XPathSelectElements("/Char/AnyChars/AnyChar").ToArray();
			Assert.AreEqual("A", elements[0].Value);
			Assert.AreEqual("B", elements[1].Value);
		}

		[TestCase]
		public void TesByteArrayPropertyToXml()
		{
			var serializable = new WithNumericArrayProperty()
			{
				AnyBytes = new byte[] { Byte.MinValue, Byte.MaxValue }
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);

			var doc = XDocument.Parse(xml);
			var elements = doc.XPathSelectElements("/Numeric/AnyBytes/AnyByte").ToArray();
			Assert.AreEqual(Byte.MinValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elements[0].Value);
			Assert.AreEqual(Byte.MaxValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elements[1].Value);
		}

		[TestCase]
		public void TesSByteArrayPropertyToXml()
		{
			var serializable = new WithNumericArrayProperty()
			{
				AnySBytes = new SByte[] { SByte.MinValue, SByte.MaxValue }
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);

			var doc = XDocument.Parse(xml);
			var elements = doc.XPathSelectElements("/Numeric/AnySBytes/AnySByte").ToArray();
			Assert.AreEqual(SByte.MinValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elements[0].Value);
			Assert.AreEqual(SByte.MaxValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elements[1].Value);
		}

		[TestCase]
		public void TesSingleArrayPropertyToXml()
		{
			var serializable = new WithFloatPointNumericArrayProperty()
			{
				AnySingles = new Single[] { Single.MinValue, Single.MaxValue }
			};

			var xml = serializable.ToXml(provider: CultureInfo.InvariantCulture.NumberFormat);

			var doc = XDocument.Parse(xml);
			var elements = doc.XPathSelectElements("/Numeric/AnySingles/AnySingle").ToArray();
			Assert.AreEqual(Single.MinValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elements[0].Value);
			Assert.AreEqual(Single.MaxValue.ToString("N", CultureInfo.InvariantCulture.NumberFormat), elements[1].Value);
		}

		[TestCase]
		public void TesArrayPropertyWithNullsToXml()
		{
			var serializable = new WithArrayPropertyWithNulls();
			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var elements = doc.XPathSelectElements("/WithArrayPropertyWithNulls/MyArray/Item").ToArray();

			Assert.AreEqual(3, elements.Length);
		}

		[TestCase]
		public void TesArrayPropertyWithNullsEmitTypeToXml()
		{
			var serializable = new WithArrayPropertyWithNulls();
			var xml = serializable.ToXml(emitTypeInfo: true);

			var doc = XDocument.Parse(xml);
			var elements = doc.XPathSelectElements("/WithArrayPropertyWithNulls/MyArray/Item").ToArray();

			Assert.AreEqual(3, elements.Length);
		}

		[TestCase]
		public void TestStringArrayPropertyMandatoryAndEmptyToXml()
		{
			var serializable = new WithStringArrayMandatoryAndEmptyProperty();

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var elements = doc.XPathSelectElements("/WithStringArrayMandatoryAndEmptyProperty/MyArray/Item").ToArray();
			Assert.AreEqual(0, elements.Length);
		}

		[TestCase]
		public void TestArrayPropertyTypeInfoToXml()
		{
			var serializable = new WithStringArrayProperty()
			{
				AnyStrings = new String[] { "MyString1" }
			};

			var xml = serializable.ToXml(emitTypeInfo: true);

			var doc = XDocument.Parse(xml);
			var arrayElement = doc.XPathSelectElement("/String/AnyStrings");

			Assert.AreEqual(
				typeof(String[]).AssemblyQualifiedName, arrayElement.Attribute(Constants.TypeInfoAttributeName).Value);

			var arrayItemElement = doc.XPathSelectElement("/String/AnyStrings/AnyString");

			Assert.AreEqual(
				typeof(String).AssemblyQualifiedName, arrayItemElement.Attribute(Constants.TypeInfoAttributeName).Value);
		}

		#endregion
	}
}
