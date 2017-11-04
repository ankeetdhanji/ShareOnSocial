using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace ShareOnSocial.Extensions
{
	/// <summary>
	/// Some extension methods that will help with sliding
	/// animations on storyboards.
	/// </summary>
	public static class StoryboardExtensions
	{

		/// <summary>
		/// Add slide in from bottom corner animation to a storyboard.
		/// </summary>
		/// <param name="storyboard">Storyboard to add the animation too.</param>
		/// <param name="duration">Duration of the animation in seconds.</param>
		/// <param name="start">Starting position of the element to animate from.</param>
		/// <param name="end">Ending position of the element to animate to.</param>
		/// <param name="decelerationratio">Deceleration ratio.</param>
		/// <param name="keepmargin">Flag to indicate if the left margin should be kept.</param>
		public static void AddSlide(this Storyboard storyboard, float duration, Thickness start, Thickness end, float decelerationratio = 0.9f, bool keepmargin = true)
		{
			Slide(storyboard, duration, decelerationratio, start, end, true);
		}

		/// <summary>
		/// Slide the framework element to desired location.
		/// </summary>
		/// <param name="storyboard">Storyboard to add the animation too.</param>
		/// <param name="duration">Duration of the animation in seconds.</param>
		/// <param name="decelerationratio">Deceleration ratio</param>					 
		/// <param name="start">Starting position of the element to animate from.</param>
		/// <param name="end">Ending position of the element to animate to.</param>
		/// <param name="slideto">Flag to indicate if we're sliding to or away from the desired thickness.</param>
		private static void Slide(Storyboard storyboard, float duration, float decelerationratio, Thickness start, Thickness end,
			bool slideto)
		{
			// Create slide animation.
			ThicknessAnimation animation = new ThicknessAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(duration)),
				From = start,
				To = end,
				DecelerationRatio = decelerationratio
			};

			// Set target to margin.
			Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

			// Add animation to storyboard.
			storyboard.Children.Add(animation);
		}

		/// <summary>
		/// Add fade in effect to the storyboard.
		/// </summary>				 
		/// <param name="storyboard">Storyboard to add fade in effect to.</param>
		/// <param name="duration">Duration of the animation in seconds.</param>
		public static void AddFadeIn(this Storyboard storyboard, float duration)
		{
			// Create fade in effect.
			DoubleAnimation animation = new DoubleAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(duration)),
				From = 0,
				To = 1
			};

			// Set target to opacity
			Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

			// Add animation to storyboard.
			storyboard.Children.Add(animation);
		}

		/// <summary>
		/// Add fade out effect to the storyboard.
		/// </summary>				 
		/// <param name="storyboard">Storyboard to add fade out effect to.</param>
		/// <param name="duration">Duration of the animation in seconds.</param>
		public static void AddFadeOut(this Storyboard storyboard, float duration)
		{
			// Create fade out animation
			DoubleAnimation animation = new DoubleAnimation()
			{
				Duration = new Duration(TimeSpan.FromSeconds(duration)),
				From = 1,
				To = 0
			};

			// Set target to opacity.
			Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

			// Add animation to storyboard.
			storyboard.Children.Add(animation);
		}
	}
}
