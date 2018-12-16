# Strong Type Support

## How does serialization behave with fields/properties of base class or interface types?

When serializing, the serializer always checks the actual type of the value and not the declared type of the field/property. The declared type does not even have to implement **IXPathSerializable** interface. Like in an example below, where Basket class has a property of type Fruit, which is not serializable. However, the runtime value of the property is an instance of class Orange, which is serializable.

```csharp
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
```

```csharp
Basket basket = new Basket()
{
	Fruit = new Orange()
	{
		Price = 3m
	}
};

string xml = basket.ToXml();
```

```xml
<Basket>
  <Fruit>
    <Orange>
      <Price>3.00</Price>
    </Orange>
  </Fruit>
</Basket>
```

By default serialization code does not emit any information about the type into the resulting Xml. This helps to produce cleaner Xml without any extra elements/attributes that might otherwise not be valid according to your Xml schema.

But, in this case when deserializing an object, serializer has no other choice than to use the declared field/property type to create the new value. Of course, in our example it will not be able to deserialize an object, because it has no way for it to know that it has to create a value of class Orange.

This would not be too useful if we could not use base type for the fields/properties and still be able to deserialize the object. The solution is to use the **emitTypeInfo** parameter of **ToXml** method which forces serializer to emit the type info into the Xml. This type info is used to determine the exact type of the new value when deserializing the object.

```csharp
Basket basket = new Basket()
{
	Fruit = new Orange()
	{
		Price = 3m
	}
};

string xml = basket.ToXml(emitTypeInfo: true);
```

```xml
<Basket>
  <Fruit p2:__type="Artemkv.Transformation.XmlToObject.Test.Examples.StrongTypeSupport.Orange, Artemkv.Transformation.XmlToObject.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" xmlns:p2="http://xmltoobject.codeplex.com">
    <Orange>
      <Price p2:__type="System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">3.00</Price>
    </Orange>
  </Fruit>
</Basket>
```

Of course, the Xml is not that clean anymore, but the roundtrip is now possible.

Please note that type info is never emitted if value is serialized as an Xml attribute. In this case there is no good place to store this information.

**Read next**: [Versioning](Versioning.md)
