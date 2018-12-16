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
	[TestFixture]
	public class XmlSerialization_ObjectArray_ToXml_Test
	{
		#region To Xml

		[TestCase]
		public void OuterClassWithArrayOfSerializableToXml()
		{
			var serializable = new OuterClassWithArrayOfSerializable()
			{
				SimpleOuterProperty = "Simple outer",
			};
			serializable.InnerObjects = new InnerClass[] {
				new InnerClass() { SimpleInnerProperty = "first" },
				new InnerClass() { SimpleInnerProperty = "second" } 
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementSimpleOuter = doc.XPathSelectElement("/OuterClassWithArrayOfSerializable/SimpleOuterProperty");
			Assert.AreEqual("Simple outer", elementSimpleOuter.Value);

			var elementsInner = doc.XPathSelectElements("/OuterClassWithArrayOfSerializable/InnerObjects/InnerClass/SimpleInnerProperty").ToArray();
			Assert.AreEqual("first", elementsInner[0].Value);
			Assert.AreEqual("second", elementsInner[1].Value);
		}

		[TestCase]
		public void OuterClassWithArrayOfSerializableNoCtorToXml()
		{
			var serializable = new OuterClassWithArrayOfSerializableNoCtor()
			{
				SimpleOuterProperty = "Simple outer",
			};
			serializable.InnerObjects = new InnerClassNoCtor[2];

			serializable.InnerObjects[0] = InnerClassNoCtor.Create();
			serializable.InnerObjects[0].SimpleInnerProperty = "first";

			serializable.InnerObjects[1] = InnerClassNoCtor.Create();
			serializable.InnerObjects[1].SimpleInnerProperty = "second";
	
			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementSimpleOuter = doc.XPathSelectElement("/OuterClassWithArrayOfSerializableNoCtor/SimpleOuterProperty");
			Assert.AreEqual("Simple outer", elementSimpleOuter.Value);

			var elementsInner = doc.XPathSelectElements("/OuterClassWithArrayOfSerializableNoCtor/InnerObjects/InnerClassNoCtor/SimpleInnerProperty").ToArray();
			Assert.AreEqual("first", elementsInner[0].Value);
			Assert.AreEqual("second", elementsInner[1].Value);
		}

		#endregion

		#region Load From Xml

		[TestCase]
		public void OuterClassWithArrayOfSerializableFromXml()
		{
			var xml = @"<OuterClassWithArrayOfSerializable>
  <SimpleOuterProperty>Simple outer</SimpleOuterProperty>
  <InnerObjects>
    <InnerClass>
      <SimpleInnerProperty>first</SimpleInnerProperty>
    </InnerClass>
    <InnerClass>
      <SimpleInnerProperty>second</SimpleInnerProperty>
    </InnerClass>
  </InnerObjects>
</OuterClassWithArrayOfSerializable>";

			var serializable = XmlSerialization.LoadFromXml<OuterClassWithArrayOfSerializable>(xml);

			Assert.AreEqual("Simple outer", serializable.SimpleOuterProperty);
			Assert.AreEqual("first", serializable.InnerObjects[0].SimpleInnerProperty);
			Assert.AreEqual("second", serializable.InnerObjects[1].SimpleInnerProperty);
		}

		[TestCase, ExpectedException(typeof(SerializationException))]
		public void OuterClassWithArrayOfSerializableNoCtorFromXml()
		{
			var xml = @"<OuterClassWithArrayOfSerializableNoCtor>
  <SimpleOuterProperty>Simple outer</SimpleOuterProperty>
  <InnerObjects>
    <InnerClassNoCtor>
      <SimpleInnerProperty>first</SimpleInnerProperty>
    </InnerClassNoCtor>
    <InnerClassNoCtor>
      <SimpleInnerProperty>second</SimpleInnerProperty>
    </InnerClassNoCtor>
  </InnerObjects>
</OuterClassWithArrayOfSerializableNoCtor>";

			var serializable = XmlSerialization.LoadFromXml<OuterClassWithArrayOfSerializableNoCtor>(xml);
		}

		
		#endregion
	}
}
