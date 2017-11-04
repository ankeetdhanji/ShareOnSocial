using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ShareOnSocial.Extensions;

namespace ShareOnSocial.Animations
{
	/// <summary>
	/// Animate framework elements.
	/// </summary>
	public static class FrameworkElementAnimations
	{
		public static async Task SlideAndFadeInFromBottomLeftAsync(this FrameworkElement element, Thickness targetposition, float duration = 0.3f)
		{
			await DoAnimation(AnimationType.SlideAndFadeInFromBottomLeftCorner, element, duration, targetposition);
		}

		public static async Task SlideAndFadeOutToBottomLeftAsync(this FrameworkElement element, Thickness targetposition, float duration = 0.3f)
		{
			await DoAnimation(AnimationType.SlideAndFadeOutToBottomLeftCorner, element, duration, targetposition);
		}

		private static async Task DoAnimation(AnimationType type, FrameworkElement element, float duration,
			Thickness targetthickness)
		{
			// Create the storyboard to add the animation too.
			Storyboard storyboard = new Storyboard();


			switch (type)
			{
				case AnimationType.SlideAndFadeInFromBottomLeftCorner:
					element.Visibility = Visibility.Visible;
					AnimateSlideBottomCorner(storyboard, element,duration, targetthickness, true);
					break;
				case AnimationType.SlideAndFadeOutToBottomLeftCorner:
					AnimateSlideBottomCorner(storyboard, element, duration, targetthickness, false);
					break;
				case AnimationType.None:
				default:
					// Do nothing
					break;
			}

			// Start the animation on the element.
			storyboard.Begin(element);
			
			// Wait for it to finish
			await Task.Delay(TimeSpan.FromSeconds(duration));
			if (type == AnimationType.SlideAndFadeOutToBottomLeftCorner)
				element.Visibility = Visibility.Collapsed;

		}

		public static void AnimateSlideBottomCorner(Storyboard storyboard, FrameworkElement element, float duration, Thickness targetthickness, bool slideto)
		{
			Thickness start = new Thickness(0, ((Grid)element.Parent).ActualHeight-element.ActualHeight, ((Grid)element.Parent).ActualWidth-element.ActualWidth, 0);
			Thickness end = targetthickness;

			if (slideto)
			{
				storyboard.AddSlide(duration, start, end);
				storyboard.AddFadeIn(duration);
			}
			else
			{
				storyboard.AddSlide(duration, end, start);
				storyboard.AddFadeOut(duration);
			}
		}
	}
}
