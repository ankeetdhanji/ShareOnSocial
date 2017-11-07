using System.Windows;
using ShareOnSocial.Animations;

namespace ShareOnSocial.AttachedProperties.Animations
{
	public class AnimateSlideInFromLeftProperty : BaseAnimationProperty<AnimateSlideInFromLeftProperty>
	{
		protected override async void DoAnimation(FrameworkElement element, bool value)
		{
			if (value)
				await element.SlideAndFadeInFromLeftAsync();
		}
	}
}
