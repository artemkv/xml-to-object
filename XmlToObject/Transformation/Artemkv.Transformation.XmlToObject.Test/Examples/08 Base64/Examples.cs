using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Artemkv.Transformation.XmlToObject.Test.Examples.Base64
{
	public class File : IXPathSerializable
	{
		public string FileName { get; set; }
		[SerializeAsBase64]
		public byte[] FileContent { get; set; }
	}

	[TestFixture]
	public class PersonSerialization
	{
		[Test]
		public void TestToXmlA()
		{
			File file = new File()
			{
				FileName = "readme.txt",
				FileContent = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
			};

			string xml = file.ToXml();
		}
	}
}
