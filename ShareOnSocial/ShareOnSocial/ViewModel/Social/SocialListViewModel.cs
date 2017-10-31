using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ShareOnSocial.Model;

namespace ShareOnSocial.ViewModel.Social
{
	/// <summary>
	/// View model for the social list side bar.
	/// </summary>
	public class SocialListViewModel
	{	
		#region Public Properties

		/// <summary>
		/// List of Social List Items
		/// </summary>
		public ObservableCollection<SocialListItemViewModel> Items { get; set; }

		#endregion

		#region Public Commands

		/// <summary>
		/// Command to show the add menu
		/// </summary>
		public ICommand ShowAddMenuCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public SocialListViewModel()
		{
			Items = new ObservableCollection<SocialListItemViewModel>();

			// Create commands
			ShowAddMenuCommand = new RelayCommand(ShowAddMenu);
		}

		
		#endregion

		#region Command Methods

		/// <summary>
		/// This shows the add social menu.
		/// </summary>
		private void ShowAddMenu()
		{
			//TODO: REPLACE THIS - Create functionality to show the add menu
			Items.Add(new SocialListItemViewModel()
			{
				Icon = Icon.RedditAlien
			});
		}


		#endregion
	}
}
