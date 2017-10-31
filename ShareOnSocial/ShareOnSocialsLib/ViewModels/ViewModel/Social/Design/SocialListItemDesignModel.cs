using System;
using ShareOnSocialsLib.Models;

namespace ShareOnSocialsLib.ViewModels.ViewModel.Social.Design
{
	/// <summary>
	/// View model for the items in the social list side bar.
	/// </summary>
	public class SocialListItemDesignModel : SocialListItemViewModel
	{
		#region Singleton
		private static Lazy<SocialListItemDesignModel> mInstance 
			= new Lazy<SocialListItemDesignModel>(() => new SocialListItemDesignModel()); ///< The singleton for the social list item design model.

		/// <summary>
		/// The singleton for this the social list item design model.
		/// </summary>
		public static SocialListItemDesignModel Instance { get { return mInstance.Value; } }

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public SocialListItemDesignModel()
		{
			Icon = Icon.RedditAlien;
		}

		#endregion
	}
}
