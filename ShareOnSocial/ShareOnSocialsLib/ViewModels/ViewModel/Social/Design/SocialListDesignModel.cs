using System;
using System.Collections.ObjectModel;
using ShareOnSocialsLib.Models;

namespace ShareOnSocialsLib.ViewModels.ViewModel.Social.Design
{
	/// <summary>
	/// View model for the in the social list side bar.
	/// </summary>
	public class SocialListDesignModel : SocialListViewModel
	{
		#region Singleton
		private static Lazy<SocialListDesignModel> mInstance 
			= new Lazy<SocialListDesignModel>(() => new SocialListDesignModel()); ///< The singleton for the social list design model.

		/// <summary>
		/// The singleton for this the social list design model.
		/// </summary>
		public static SocialListDesignModel Instance { get { return mInstance.Value; } }

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public SocialListDesignModel()
		{
			Items = new ObservableCollection<SocialListItemViewModel>()
			{
				new SocialListItemViewModel()
				{
					Icon = Icon.FacebookF
				},
				new SocialListItemDesignModel()
				{
					Icon = Icon.TwitterBird
				},
				new SocialListItemViewModel()
				{
					Icon = Icon.RedditAlien,
					IsSelected = true
				}

			};
		}

		#endregion
	}
}
