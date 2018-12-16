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
	public class XmlSerialization_ComplexTypes_ToXml_Test
	{
		[TestCase]
		public void TestStrongTypedListPropertyToXml()
		{
			var serializable = new WithStrongTypedListProperty()
			{
				MyList = new List<InnerClass>() 
				{  
					new InnerClass() { SimpleInnerProperty = "first" } ,
					new InnerClass() { SimpleInnerProperty = "second" }
				}
			};

			var xml = serializable.ToXml();

			var deserialized = XmlSerialization.LoadFromXml<WithStrongTypedListProperty>(xml);

			Assert.AreEqual(serializable.MyList[0].SimpleInnerProperty, deserialized.MyList[0].SimpleInnerProperty);
			Assert.AreEqual(serializable.MyList[1].SimpleInnerProperty, deserialized.MyList[1].SimpleInnerProperty);
		}

		[TestCase]
		public void TestWeakTypedListPropertyToXml()
		{
			var serializable = new WithWeakTypedListProperty()
			{
				MyList = new List<object>() 
				{ 
					"SomeString", 
					true, 
					new InnerClass() { SimpleInnerProperty = "SimpleInner" }
				}
			};

			var xml = serializable.ToXml(true);

			var deserialized = XmlSerialization.LoadFromXml<WithWeakTypedListProperty>(xml);

			Assert.AreEqual(serializable.MyList[0], deserialized.MyList[0]);
			Assert.AreEqual(serializable.MyList[1], deserialized.MyList[1]);
			Assert.AreEqual(((InnerClass)serializable.MyList[2]).SimpleInnerProperty, ((InnerClass)deserialized.MyList[2]).SimpleInnerProperty);
		}

		[TestCase]
		public void TestListPropertyNullToXml()
		{
			var serializable = new WithStrongTypedListProperty()
			{
				MyList = null
			};

			var xml = serializable.ToXml();

			var deserialized = XmlSerialization.LoadFromXml<WithStrongTypedListProperty>(xml);

			Assert.IsTrue(deserialized.MyList == null);
		}
	}
}
