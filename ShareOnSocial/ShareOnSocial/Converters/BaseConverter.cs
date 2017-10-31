using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ShareOnSocial.Converters
{
	/// <summary>
	/// Base Converter - Contains a singleton for a instance of the converter.
	/// </summary>
	/// <typeparam name="T">Type of converter</typeparam>
	public abstract class BaseConverter<T> : MarkupExtension, IValueConverter
		where T : class, new()
	{
		#region Singleton

		private static Lazy<T> mInstance = new Lazy<T>(() => new T()); ///< Lazy instance for T.

		#endregion

		/// <summary>
		/// <see cref="MarkupExtension.ProvideValue"/>
		/// </summary>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return mInstance.Value;
		}

		/// <summary>
		/// <see cref="IValueConverter.Convert"/>
		/// </summary>
		public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
		
		/// <summary>
		/// <see cref="IValueConverter.ConvertBack"/>
		/// </summary>
		public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
	}
}
