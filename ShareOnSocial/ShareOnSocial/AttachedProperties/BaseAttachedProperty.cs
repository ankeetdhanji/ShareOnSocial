using System;
using System.Windows;

namespace ShareOnSocial.AttachedProperties
{
	/// <summary>
	/// This is the base for attached properties.
	/// </summary>
	/// <typeparam name="Parent">Parent class that is the attached property.</typeparam>
	/// <typeparam name="Property">The type of the attached property.</typeparam>
	public abstract class BaseAttachedProperty<Parent, Property>
		where Parent : new()
	{
		#region Singleton

		private static Lazy<Parent> mInstance = new Lazy<Parent>(() => new Parent()); ///< Singleton of parent.
		
		/// <summary>
		/// Singleton instance of the parent class.
		/// </summary>																	  
		public static Parent Instance { get { return mInstance.Value; } }

		#endregion

		#region Attached Properties

		/// <summary>
		/// The actual attached property.
		/// </summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
			"Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>), 
			new UIPropertyMetadata(default(Property), 
				new PropertyChangedCallback(OnValuePropertyChanged), 
				new CoerceValueCallback(OnValuePropertyUpdated)));

		public static Property GetValue(DependencyObject d)
		{
			return (Property) d.GetValue(ValueProperty);
		}

		public static void SetValue(DependencyObject d, Property value)
		{
			d.SetValue(ValueProperty, value);
		}

		/// <summary>
		/// Callback event. This is triggered when <see cref="ValueProperty"/> is updated.
		/// </summary>
		/// <param name="dependency">The UI element that has had its property updated</param>
		/// <param name="basevalue">The new value of this property.</param>
		/// <returns>The new value.</returns>
		private static object OnValuePropertyUpdated(DependencyObject dependency, object basevalue)
		{
			BaseAttachedProperty<Parent, Property> instance = Instance as BaseAttachedProperty<Parent, Property>;
			if (instance != null)
			{
				// Call the parent function
				instance.OnValueUpdated(dependency, basevalue);

				// Call event listener
				instance.ValueUpdated(dependency, basevalue);
			}
			return basevalue;
		}

		/// <summary>
		/// Callback that is triggered when the <see cref="ValueProperty"/> is changed.
		/// </summary>
		/// <param name="dependency">The UI element that has has its property changed.</param>
		/// <param name="e">Arguments for the event.</param>
		private static void OnValuePropertyChanged(DependencyObject dependency, DependencyPropertyChangedEventArgs e)
		{
			BaseAttachedProperty<Parent, Property> instance = Instance as BaseAttachedProperty<Parent, Property>;
			if (instance != null)
			{
				// Call the parent function
				instance.OnValueChanged(dependency, e);

				// Call event listener
				instance.ValueChanged(dependency, e);
			}
		}

		#endregion

		#region Event Methods

		/// <summary>
		/// Evented triggered when value is changed.
		/// </summary>
		public event Action<DependencyObject, DependencyPropertyChangedEventArgs>  ValueChanged = (sender, e) => { };

		/// <summary>
		/// Event triggered when value is updated.
		/// </summary>
		public event Action<DependencyObject, object> ValueUpdated = (sender, e) => { };

		/// <summary>
		/// The method that is called when any attached property is changed.
		/// </summary>
		/// <param name="sender">UI element that has had its property changed.</param>
		/// <param name="e">Arguments for this event.</param>
		public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

		/// <summary>
		/// The method that is called when any attached property is updated.
		/// </summary>
		/// <param name="sender">UI element that has had its property updated.</param>
		/// <param name="value">New value for this property.</param>
		public virtual void OnValueUpdated(DependencyObject sender, object value) { }

		#endregion
	}
}
