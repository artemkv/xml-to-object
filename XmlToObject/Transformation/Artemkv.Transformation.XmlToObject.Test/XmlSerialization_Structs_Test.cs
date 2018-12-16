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
	public class XmlSerialization_Structs_Test
	{
		[TestCase]
		public void TestStructToXml()
		{
			var serializable = new SerializableStruct()
			{
				AnyStringField = "My Field",
				AnyStringProperty = "My Property",
				InnerStruct = new SerializableStructInner()
				{
					AnyStringFieldInner = "Inner"
				},
				StructArray = new SerializableStruct[] 
				{
					new SerializableStruct()
					{
						AnyStringField = "Array Field",
						AnyStringProperty = "Array Property"
					}
				},
				StructList = new List<SerializableStruct>()
				{
					new SerializableStruct()
					{
						AnyStringField = "List Field",
						AnyStringProperty = "List Property"
					}
				}
			};

			string xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);
			var elementField = doc.XPathSelectElement("/SerializableStruct/AnyStringField");
			var elementProperty = doc.XPathSelectElement("/SerializableStruct/AnyStringProperty");

			Assert.AreEqual("My Field", elementField.Value);
			Assert.AreEqual("My Property", elementProperty.Value);

			var elementPropertyInner = doc.XPathSelectElement("/SerializableStruct/InnerStruct/SerializableStructInner/AnyStringFieldInner");

			Assert.AreEqual("Inner", elementPropertyInner.Value);

			var elementFieldArray = doc.XPathSelectElement("/SerializableStruct/StructArray/SerializableStruct/AnyStringField");
			var elementPropertyArray = doc.XPathSelectElement("/SerializableStruct/StructArray/SerializableStruct/AnyStringProperty");

			Assert.AreEqual("Array Field", elementFieldArray.Value);
			Assert.AreEqual("Array Property", elementPropertyArray.Value);
		}

		[TestCase]
		public void TestStructFromXml()
		{
			var xml = @"<SerializableStruct>
  <AnyStringField>My Field</AnyStringField>
  <AnyStringProperty>My Property</AnyStringProperty>
  <InnerStruct>
    <SerializableStructInner>
      <AnyStringFieldInner>Inner</AnyStringFieldInner>
    </SerializableStructInner>
  </InnerStruct>
  <StructArray>
    <SerializableStruct>
      <AnyStringField>Array Field</AnyStringField>
      <AnyStringProperty>Array Property</AnyStringProperty>
      <InnerStruct />
    </SerializableStruct>
  </StructArray>
  <StructList>
    <SerializableStruct>
      <AnyStringField>List Field</AnyStringField>
      <AnyStringProperty>List Property</AnyStringProperty>
      <InnerStruct />
    </SerializableStruct>
  </StructList>
</SerializableStruct>";

			var serializable = XmlSerialization.LoadFromXml<SerializableStruct>(xml);

			Assert.AreEqual("My Field", serializable.AnyStringField);
			Assert.AreEqual("My Property", serializable.AnyStringProperty);
			Assert.AreEqual("Inner", serializable.InnerStruct.AnyStringFieldInner);
			Assert.AreEqual("Array Field", serializable.StructArray[0].AnyStringField);
			Assert.AreEqual("Array Property", serializable.StructArray[0].AnyStringProperty);
			Assert.AreEqual("List Field", serializable.StructList[0].AnyStringField);
			Assert.AreEqual("List Property", serializable.StructList[0].AnyStringProperty);
		}
	}
}
