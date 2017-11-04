using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareOnSocialsLib.ViewModels.ViewModel.Base;

namespace ShareOnSocialsLib.ViewModels.ViewModel.Social
{
	public class AddSocialPopUpDesignModel : AddSocialPopUpViewModel
	{
		#region Singleton

		private static Lazy<AddSocialPopUpDesignModel> mInstance
			= new Lazy<AddSocialPopUpDesignModel>(() => new AddSocialPopUpDesignModel()); ///< The singleton for the social list design model.

		/// <summary>
		/// The singleton for this the social list design model.
		/// </summary>
		public static AddSocialPopUpDesignModel Instance { get { return mInstance.Value; } }

		#endregion

		public AddSocialPopUpDesignModel()
		{
			IsVisible = true;
		}

	}
}
