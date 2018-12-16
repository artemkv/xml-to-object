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
	public class XmlSerialization_ObjectTypes_ToXml_Test
	{
		#region To Xml

		[TestCase]
		public void TestOuterFieldToXml()
		{
			var serializable = new OuterClass()
			{
				SimpleOuterProperty = "Simple outer",
				ObjectProperty = new InnerClass()
				{
					SimpleInnerProperty = "Simple inner"
				}
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementSimpleOuter = doc.XPathSelectElement("/OuterClass/SimpleOuterProperty");
			Assert.AreEqual("Simple outer", elementSimpleOuter.Value);

			var elementSimpleInner = doc.XPathSelectElement("/OuterClass/ObjectProperty/InnerClass/SimpleInnerProperty");
			Assert.AreEqual("Simple inner", elementSimpleInner.Value);
		}

		[TestCase]
		public void TestOuterFieldNoCtorToXml()
		{
			var inner = InnerClassNoCtor.Create();
			inner.SimpleInnerProperty = "Simple inner";

			var serializable = new OuterClassNoCtor()
			{
				SimpleOuterProperty = "Simple outer",
				ObjectProperty = inner
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementSimpleOuter = doc.XPathSelectElement("/OuterClassNoCtor/SimpleOuterProperty");
			Assert.AreEqual("Simple outer", elementSimpleOuter.Value);

			var elementSimpleInner = doc.XPathSelectElement("/OuterClassNoCtor/ObjectProperty/InnerClassNoCtor/SimpleInnerProperty");
			Assert.AreEqual("Simple inner", elementSimpleInner.Value);
		}

		[TestCase, ExpectedException(typeof(SerializationException))]
		public void TestOuterFieldCyclicToXml()
		{
			var serializable = new OuterClassCyclic();
			serializable.Child = new InnerClassCyclic()
			{
				Parent = serializable
			};

			var xml = serializable.ToXml();
		}

		[TestCase]
		public void TestObjectAltMappingToXml()
		{
			var serializable = new OuterClassAltMapping()
			{
				SimpleOuterProperty = "Simple outer",
				ObjectProperty = new InnerClassAltMapping()
				{
					SimpleInnerProperty = "Simple inner"
				}
			};

			var xml = serializable.ToXml();

			var doc = XDocument.Parse(xml);

			var elementSimpleOuter = doc.XPathSelectElement("/AlternativeMapping/SimpleOuterProperty");
			Assert.AreEqual("Simple outer", elementSimpleOuter.Value);

			var elementSimpleInner = doc.XPathSelectElement("/AlternativeMapping/Inner/SimpleInnerProperty");
			Assert.AreEqual("Simple inner", elementSimpleInner.Value);
		}

		[TestCase, ExpectedException(typeof(SerializationException))]
		public void TestUnsupportedTypeToXml()
		{
			var serializable = new WithObjectProperty()
			{
				AnyObject = new UTF7Encoding()
			};

			string xml = serializable.ToXml();
		}

		[TestCase]
		public void TestAbsoluteMappingsToXml()
		{
			var address = new Address()
			{
				Coutry = "US",
				Name = "Alice Smith",
				Street = "123 Maple Street",
				City = "Mill Valley",
				State = "CA",
				ZipCode = "90952"
			};

			string xml = address.ToXml();

			var doc = XDocument.Parse(xml);

			var element = doc.XPathSelectElement("/address/city");
			Assert.NotNull(element);
		}

		[TestCase]
		public void TestRelativeMappingsToXml()
		{
			var po = new PurchaseOrder()
			{
				OrderDate = DateTime.Today,
				ShipTo = new Address()
				{
					Coutry = "US",
					Name = "Alice Smith",
					Street = "123 Maple Street",
					City = "Mill Valley",
					State = "CA",
					ZipCode = "90952"
				},
				BillTo = new Address()
				{
					Coutry = "US",
					Name = "Robert Smith",
					Street = "8 Oak Avenue",
					City = "Old Town",
					State = "PA",
					ZipCode = "95819"
				}
			};

			string xml = po.ToXml();

			var doc = XDocument.Parse(xml);

			var element = doc.XPathSelectElement("/purchaseOrder/shipTo/city");
			Assert.NotNull(element);
		}

		[TestCase]
		public void TestAbsoluteMappingsWithTypeInfoToXml()
		{
			var address = new Address()
			{
				Coutry = "US",
				Name = "Alice Smith",
				Street = "123 Maple Street",
				City = "Mill Valley",
				State = "CA",
				ZipCode = "90952"
			};

			string xml = address.ToXml(emitTypeInfo: true);

			var doc = XDocument.Parse(xml);

			var element = doc.XPathSelectElement("/address/city");
			Assert.NotNull(element);

			var addressDeserialized = XmlSerialization.LoadFromXmlDocument<Address>(doc);

			Assert.AreEqual(address.State, addressDeserialized.State);
		}

		[TestCase]
		public void TestRelativeMappingsWithTypeInfoToXml()
		{
			var po = new PurchaseOrder()
			{
				OrderDate = DateTime.Today,
				ShipTo = new Address()
				{
					Coutry = "US",
					Name = "Alice Smith",
					Street = "123 Maple Street",
					City = "Mill Valley",
					State = "CA",
					ZipCode = "90952"
				},
				BillTo = new Address()
				{
					Coutry = "US",
					Name = "Robert Smith",
					Street = "8 Oak Avenue",
					City = "Old Town",
					State = "PA",
					ZipCode = "95819"
				}
			};

			string xml = po.ToXml(emitTypeInfo: true);

			var doc = XDocument.Parse(xml);

			var element = doc.XPathSelectElement("/purchaseOrder/shipTo/city");
			Assert.NotNull(element);

			var poDeserialized = XmlSerialization.LoadFromXmlDocument<PurchaseOrder>(doc);

			Assert.AreEqual(po.BillTo.State, poDeserialized.BillTo.State);
		}

		#endregion

		#region Load From Xml

		[TestCase]
		public void TestOuterFieldLoadFromXml()
		{
			var xml = @"<OuterClass>
  <SimpleOuterProperty>Simple outer</SimpleOuterProperty>
  <ObjectProperty>
    <InnerClass>
      <SimpleInnerProperty>Simple inner</SimpleInnerProperty>
    </InnerClass>
  </ObjectProperty>
</OuterClass>";

			var serializable = XmlSerialization.LoadFromXml<OuterClass>(xml);

			Assert.AreEqual("Simple outer", serializable.SimpleOuterProperty);
			Assert.AreEqual("Simple inner", serializable.ObjectProperty.SimpleInnerProperty);
		}

		[TestCase, ExpectedException(typeof(SerializationException))]
		public void TestOuterFieldNoCtorLoadFromXml()
		{
			var xml = @"<OuterClassNoCtor>
  <SimpleOuterProperty>Simple outer</SimpleOuterProperty>
  <ObjectProperty>
    <InnerClassNoCtor>
      <SimpleInnerProperty>Simple inner</SimpleInnerProperty>
    </InnerClassNoCtor>
  </ObjectProperty>
</OuterClassNoCtor>";

			var serializable = XmlSerialization.LoadFromXml<OuterClassNoCtor>(xml);
		}

		[TestCase]
		public void TestObjectAltMappingLoadFromXml()
		{
			var xml = @"<AlternativeMapping>
  <SimpleOuterProperty>Simple outer</SimpleOuterProperty>
  <Inner>
    <SimpleInnerProperty>Simple inner</SimpleInnerProperty>
  </Inner>
</AlternativeMapping>";

			var serializable = XmlSerialization.LoadFromXml<OuterClassAltMapping>(xml);

			Assert.AreEqual("Simple outer", serializable.SimpleOuterProperty);
			Assert.AreEqual("Simple inner", serializable.ObjectProperty.SimpleInnerProperty);
		}

		[TestCase, ExpectedException(typeof(SerializationException))]
		public void TestUnsupportedTypeLoadFromXml()
		{
			var xml = String.Format(@"<Object>
  <AnyObject __type='{0}'>Some value</AnyObject>
</Object>", typeof(Encoder).AssemblyQualifiedName);

			var serializable = XmlSerialization.LoadFromXml<WithObjectProperty>(xml);
		}

		[TestCase]
		public void TestAbsoluteMappingsFromXml()
		{
			var xml = @"<address country='US'>
  <name>Alice Smith</name>
  <street>123 Maple Street</street>
  <city>Mill Valley</city>
  <state>CA</state>
  <zip>90952</zip>
</address>";

			var serializable = XmlSerialization.LoadFromXml<Address>(xml);

			Assert.AreEqual("Alice Smith", serializable.Name);
		}

		[TestCase]
		public void TestRelativeMappingsFromXml()
		{
			var xml = @"<purchaseOrder orderDate='2012-11-13'>
  <shipTo country='US'>
    <name>Alice Smith</name>
    <street>123 Maple Street</street>
    <city>Mill Valley</city>
    <state>CA</state>
    <zip>90952</zip>
  </shipTo>
  <billTo country='US'>
    <name>Robert Smith</name>
    <street>8 Oak Avenue</street>
    <city>Old Town</city>
    <state>PA</state>
    <zip>95819</zip>
  </billTo>
</purchaseOrder>";

			var serializable = XmlSerialization.LoadFromXml<PurchaseOrder>(xml);

			Assert.AreEqual("Alice Smith", serializable.ShipTo.Name);
		}

		#endregion
	}
}
