# **XmlToObject**
XmlToObject is a .Net library for object serialization with use of XPath expressions applied to class fields and properties via custom attributes.

## Why?

I started working on this project when I had to integrate with an application API that was using quite generic Xml schemas ('/object[@type]/field[@name]') and I wanted to create a layer of POCO objects to simplify my business code. 
I had a choice: to mirror the generic Xml structure in my classes and have the serialization for free, or create classes with specific strong-typed properties, but at cost of writing all serialization code manually. I didn't like either option, so I made an attempt on serialization library that uses the XPath to map the properties to Xml elements/attributes and allows serialization with the data shaping capabilities.

## Status

The library is currently in beta.

## Documentation

**The best place to start** is an [Intro](docs/Documentation.md) page.

If you are ready for more details, please look into [Tutorial](docs/Tutorial.md).

You can also go directly to the [API Reference](docs/API-Reference.md).
