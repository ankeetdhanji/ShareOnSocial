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
		/// <summary>
		/// Animation to slide and fade in the element from the bottom left.
		/// </summary>
		/// <param name="element">Element to animate</param>
		/// <param name="targetposition">Margin the element should end up with.</param>
		/// <param name="duration">Duration of the element</param>
		/// <returns>Task - async function</returns>
		public static async Task SlideAndFadeInFromBottomLeftAsync(this FrameworkElement element, Thickness targetposition, float duration = 0.3f)
		{
			await DoAnimation(AnimationType.SlideAndFadeInFromBottomLeftCorner, element, duration, targetposition);
		}

		/// <summary>
		/// Animation to slide and fade out the element to the bottom left.
		/// </summary>
		/// <param name="element">Element to animate</param>
		/// <param name="targetposition">Margin the element should end up with.</param>
		/// <param name="duration">Duration of the element</param>
		/// <returns>Task - async function</returns>
		public static async Task SlideAndFadeOutToBottomLeftAsync(this FrameworkElement element, Thickness targetposition, float duration = 0.3f)
		{
			await DoAnimation(AnimationType.SlideAndFadeOutToBottomLeftCorner, element, duration, targetposition);
		}

		/// <summary>
		/// Animation to slide and fade in the element from the left.
		/// </summary>
		/// <param name="element">Element to animate</param>
		/// <param name="targetposition">Margin the element should end up with.</param>
		/// <param name="duration">Duration of the element</param>
		/// <returns>Task - async function</returns>
		public static async Task SlideAndFadeInFromLeftAsync(this FrameworkElement element, float duration = 0.3f)
		{
			await DoAnimation(AnimationType.SlideAndFadeInFromLeft, element, duration, new Thickness(0));
		}

		/// <summary>
		/// Animation to slide and fade in the element from the left.
		/// </summary>
		/// <param name="element">Element to animate</param>
		/// <param name="targetposition">Margin the element should end up with.</param>
		/// <param name="duration">Duration of the element</param>
		/// <returns>Task - async function</returns>
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
				case AnimationType.SlideAndFadeInFromLeft:
					Thickness end = new Thickness(-element.ActualWidth, 0, element.ActualWidth, 0);
					storyboard.AddSlide(duration,  end, new Thickness(0));
					storyboard.AddFadeIn(duration);
					element.Visibility = Visibility.Visible;
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
