using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artemkv.Transformation.XmlToObject.Test
{
	public class BooleanToYesNoValueConverter : ValueConverterBase
	{
		public override bool TryConvert(string value, SerializationContext context, out object convertedValue)
		{
			if (value == "Yes")
			{
				convertedValue = true;
				return true; // Convertion succeed
			}
			if (value == "No")
			{
				convertedValue = false;
				return true; // Convertion succeed
			}
			convertedValue = null;
			return false; // Convertion failed
		}

		public override bool TryConvertBack(object value, SerializationContext context, out string convertedValue)
		{
			if (value == null || !(value is bool))
			{
				convertedValue = null;
				return false; // Convertion failed
			}

			bool valueAsBoolean = (bool)value;
			convertedValue = valueAsBoolean ? "Yes" : "No";
			return true; // Convertion succeed
		}
	}
}
