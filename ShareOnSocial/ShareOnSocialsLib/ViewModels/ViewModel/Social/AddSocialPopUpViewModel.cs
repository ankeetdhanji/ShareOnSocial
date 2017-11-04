using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareOnSocialsLib.ViewModels.ViewModel.Base;

namespace ShareOnSocialsLib.ViewModels.ViewModel.Social
{
	public class AddSocialPopUpViewModel : BaseViewModel
	{
		#region Private Members

		private bool mIsVisible;
		#endregion

		#region Public Properties

		public bool IsVisible
		{
			get { return mIsVisible; }
			set { mIsVisible = value; }
		}

		#endregion

		public AddSocialPopUpViewModel()
		{
			IsVisible = false;
		}

	}
}
