using System.Windows.Input;
using ShareOnSocialsLib.Commands;
using ShareOnSocialsLib.Models;
using ShareOnSocialsLib.ViewModels.ViewModel.Base;

namespace ShareOnSocialsLib.ViewModels.ViewModel.Social
{
	/// <summary>
	/// View model for the social list item side bar.
	/// </summary>
	public class SocialListItemViewModel : BaseViewModel
	{
		#region Private Member Variables

		protected Icon mIcon; ///< The icon for this item.

		#endregion

		#region Public Properties
		/// <summary>
		/// The icon for this item.
		/// </summary>
		public Icon Icon
		{
			get { return mIcon; }
			set { mIcon = value; }
		}

		/// <summary>
		/// Flag to indicate if the item is selected.
		/// </summary>
		public bool IsSelected { get; set; }

		#endregion

		#region Public Commands

		/// <summary>
		/// Command to open the social setting page for this button.
		/// </summary>
		public ICommand OpenSocialSettingCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public SocialListItemViewModel()
		{
			// Create commands
			OpenSocialSettingCommand = new RelayCommand(OpenSocialSetting);
		}

		#endregion

		#region Command methods

		/// <summary>
		/// Opens the social setting page for this item.
		/// </summary>
		private void OpenSocialSetting()
		{
			IoC.IoC.Get<SocialListViewModel>().ItemSelected = this;
		}

		#endregion
	}
}
