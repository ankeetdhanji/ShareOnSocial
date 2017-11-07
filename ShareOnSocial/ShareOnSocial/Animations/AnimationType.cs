namespace ShareOnSocial.Animations
{
	/// <summary>
	/// The types of animations that can take place.
	/// </summary>
	public enum AnimationType
	{
		/// <summary>
		/// No animation
		/// </summary>
		None = 0,

		/// <summary>
		/// Element slides and fades in from bottom left corner.
		/// </summary>
		SlideAndFadeInFromBottomLeftCorner,

		/// <summary>
		/// Element slides and fades out to bottom left corner.
		/// </summary>
		SlideAndFadeOutToBottomLeftCorner,

		/// <summary>
		/// Element slides and fades in from te left.
		/// </summary>
		SlideAndFadeInFromLeft
	}
}
