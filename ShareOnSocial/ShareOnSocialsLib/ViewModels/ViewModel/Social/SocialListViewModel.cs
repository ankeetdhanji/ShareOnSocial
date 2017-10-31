using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ShareOnSocialsLib.Commands;
using ShareOnSocialsLib.Models;

namespace ShareOnSocialsLib.ViewModels.ViewModel.Social
{
	/// <summary>
	/// View model for the social list side bar.
	/// </summary>
	public class SocialListViewModel
	{
		private SocialListItemViewModel mItemSelected;

		#region Public Properties

		/// <summary>
		/// List of Social List Items
		/// </summary>
		public ObservableCollection<SocialListItemViewModel> Items { get; set; }

		/// <summary>
		/// The current item that is selected from the list.
		/// </summary>
		public SocialListItemViewModel ItemSelected
		{
			get { return mItemSelected; }
			set
			{
				foreach (SocialListItemViewModel item in Items)
				{
					if (item.IsSelected)
						item.IsSelected = false;
				}
				value.IsSelected = true;
				mItemSelected = value;
			}
		}

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
			
		}


		#endregion
	}
}
