using ShareOnSocial.Model;

namespace ShareOnSocial.Extensions
{
	public static class IconExtensions
	{
		public static string ToFontAwesomeString(this Icon icon)
		{
			switch (icon)
			{
				case Icon.RedditAlien:
					return "\uf281";
				case Icon.TwitterBird:
					return "\uf099";
				case Icon.FacebookF:
					return "\uf09a";
				case Icon.Add:
					return "\uf055";
				default:
					return null;
			}

		}
	}
}
