using System;
using System.Globalization;
using ShareOnSocialsLib.Extensions;
using ShareOnSocialsLib.Models;

namespace ShareOnSocial.Converters
{
	/// <summary>
	/// Converts from <see cref="Icon"/> to Font awesome string.
	/// </summary>
	public class IconToFontAwesomeStringConverter : BaseConverter<IconToFontAwesomeStringConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// if nothing to convert then return empty
			if (value == null) 
				return string.Empty;
			
			// Convert icon to font awesome string.
			Icon icon = (Icon) value;
			return icon.ToFontAwesomeString();
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
