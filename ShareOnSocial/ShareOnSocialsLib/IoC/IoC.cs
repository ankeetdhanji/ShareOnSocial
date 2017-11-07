using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using ShareOnSocialsLib.ViewModels.ViewModel.Social;

namespace ShareOnSocialsLib.IoC
{
	public class IoC : NinjectModule
	{
		private static IKernel mKernel = new StandardKernel(new IoC());

		public static IKernel Kernel { get { return mKernel; } }

		public static SocialListViewModel SocialListViewModel { get { return Get<SocialListViewModel>(); } }

		public override void Load()
		{
			Bind<SocialListViewModel>().ToConstant(new SocialListViewModel());
			Bind<AddSocialPopUpViewModel>().ToConstant(new AddSocialPopUpViewModel());
		}

		public static T Get<T>()
		{
			return Kernel.Get<T>();
		}
	}
}
