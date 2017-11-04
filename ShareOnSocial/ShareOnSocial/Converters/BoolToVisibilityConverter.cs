using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using ShareOnSocial.Properties;
using ShareOnSocialsLib.Extensions;
using ShareOnSocialsLib.Models;

namespace ShareOnSocial.Converters
{
	/// <summary>
	/// Converts from boolean> to a Visibility
	/// </summary>
	public class BoolToVisibilityConverter : BaseConverter<BoolToVisibilityConverter>
	{	
		/// <summary>
		/// if supplied value is true then the return Visible
		/// else return collapsed.
		/// </summary>
		/// <param name="value">flag to say if in selected mode</param>
		/// <param name="targetType">not used</param>
		/// <param name="parameter">not used</param>
		/// <param name="culture">not used</param>
		/// <returns></returns>
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// if nothing to convert then return null
			if (value == null)
				return null;

			if ((bool) value)
				return Visibility.Visible;

			return Visibility.Collapsed;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
