using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;


namespace ShareOnSocial.AttachedProperties.Animations
{
	/// <summary>
	/// Base animation property that runs any animation overriden when the bool provided is set to true.
	/// The reverse of the animation is performed when its false.
	/// </summary>
	/// <typeparam name="Parent"></typeparam>
	public abstract class BaseAnimationProperty<Parent> : BaseAttachedProperty<Parent, bool>
		where Parent : BaseAttachedProperty<Parent, bool>, new()
	{

		private bool mFirstLoad = true;


		public bool FirstLoad { get { return mFirstLoad; } set { mFirstLoad = value; } }
		
 
		public override void OnValueUpdated(DependencyObject sender, object value)
		{
			// if not framework element, cant animate.
			if (!(sender is FrameworkElement))
				return;

			FrameworkElement element = sender as FrameworkElement;

			if (sender.GetValue(ValueProperty) == value)
				return;
										 
			// On first load...
			if (FirstLoad)
			{
				element.Visibility = Visibility.Hidden;
				RoutedEventHandler onLoaded = null;
				onLoaded = (ss, ee) =>
				{
					element.Loaded -= onLoaded;
					DoAnimation(element, (bool)value);
					// no longer in first load
					FirstLoad = false;

				};
				element.Loaded += onLoaded;
			}
			else
			{
				DoAnimation(element, (bool) value);
			}
		}

		protected virtual void DoAnimation(FrameworkElement element, bool value)
		{
			
		}
	}
}
