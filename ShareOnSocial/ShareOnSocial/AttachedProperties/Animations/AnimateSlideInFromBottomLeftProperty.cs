using System.Windows;
using ShareOnSocial.Animations;

namespace ShareOnSocial.AttachedProperties.Animations
{
	public class AnimateSlideInFromBottomLeftProperty : BaseAnimationProperty<AnimateSlideInFromBottomLeftProperty>
	{
		protected override async void DoAnimation(FrameworkElement element, bool value)
		{
			Thickness targetthickness = (Thickness) (new ThicknessConverter().ConvertFromString((string)element.Tag));
			if (value)
				await element.SlideAndFadeInFromBottomLeftAsync(targetthickness);
			else
				await element.SlideAndFadeOutToBottomLeftAsync(targetthickness);
		}
	}
}
