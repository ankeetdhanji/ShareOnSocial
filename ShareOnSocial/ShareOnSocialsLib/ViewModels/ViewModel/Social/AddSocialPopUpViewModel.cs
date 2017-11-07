using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ShareOnSocialsLib.Commands;
using ShareOnSocialsLib.Models;
using ShareOnSocialsLib.ViewModels.ViewModel.Base;

namespace ShareOnSocialsLib.ViewModels.ViewModel.Social
{
	public class AddSocialPopUpViewModel : BaseViewModel
	{
		#region Private Members

		/// <summary>
		/// Indiciates if the buttons are visible.
		/// </summary>
		private bool mIsVisible;
		#endregion

		#region Public Properties

		/// <summary>
		/// Indicates if the buttons are visible.
		/// </summary>
		public bool IsVisible
		{
			get { return mIsVisible; }
			set { mIsVisible = value; }
		}

		#endregion

		#region Commands

		/// <summary>
		/// Add reddit to the side menu list.
		/// </summary>
		public ICommand AddRedditCommand { get; set; }

		/// <summary>
		/// Add facebook to the side menu.
		/// </summary>
		public ICommand AddFacebookCommand { get; set; }

		/// <summary>
		///  Add twitter to the side menu.
		/// </summary>
		public ICommand AddTwitterCommand { get; set; }


		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public AddSocialPopUpViewModel()
		{
			IsVisible = false;

			// Create commands
			AddRedditCommand = new RelayCommand(AddReddit);
			AddFacebookCommand = new RelayCommand(AddFacebook);
			AddTwitterCommand = new RelayCommand(AddTwitter);
		}

		#endregion

		#region Command Methods

		/// <summary>
		/// Add reddit to the menu.
		/// </summary>
		public void AddReddit()
		{
			IoC.IoC.SocialListViewModel.Items.Add(new SocialListItemViewModel()
			{
				Icon = Icon.RedditAlien
			});
			HidePopUpMenu();
		}

		/// <summary>
		/// Add facebook to the menu.
		/// </summary>
		public void AddFacebook()
		{
			IoC.IoC.SocialListViewModel.Items.Add(new SocialListItemViewModel()
			{
				Icon = Icon.FacebookF
			});
			HidePopUpMenu();
		}

		/// <summary>
		/// Add twitter to the menu.
		/// </summary>
		public void AddTwitter()
		{
			IoC.IoC.SocialListViewModel.Items.Add(new SocialListItemViewModel()
			{
				Icon = Icon.TwitterBird
			});
			HidePopUpMenu();
		}

		#endregion

		#region Private Helpers

		/// <summary>
		/// Hides the pop up menu.
		/// </summary>
		private void HidePopUpMenu()
		{
			IsVisible = false;
			IoC.IoC.Get<SocialListViewModel>().AddSocialPopUpMenuVisible = false;
		}

		#endregion
	}
}
