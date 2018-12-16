using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.StrongTypeSupport
{
	public abstract class Fruit
	{
		public decimal Price { get; set; }
	}

	public class Apple : Fruit, IXPathSerializable
	{
	}

	public class Orange : Fruit, IXPathSerializable
	{
	}

	public class Basket : IXPathSerializable
	{
		public Fruit Fruit { get; set; }
	}

	[TestFixture]
	public class BasketSerialization
	{
		[Test]
		public void TestToXml()
		{
			Basket basket = new Basket()
			{
				Fruit = new Orange()
				{
					Price = 3m
				}
			};

			string xml = basket.ToXml(emitTypeInfo: true);

			Basket deserialized = XmlSerialization.LoadFromXml<Basket>(xml);

			Assert.AreEqual(3m, deserialized.Fruit.Price);
		}
	}
}
