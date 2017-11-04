using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShareOnSocialsLib.IoC;
using ShareOnSocialsLib.ViewModels.ViewModel.Social;

namespace ShareOnSocial.Controls
{
	/// <summary>
	/// Interaction logic for SocialListControl.xaml
	/// </summary>
	public partial class SocialListControl : UserControl
	{
		public SocialListViewModel ViewModel { get; set; }
		public SocialListControl()
		{
			InitializeComponent();
			ViewModel = IoC.Get<SocialListViewModel>();
			DataContext = ViewModel;
		}
	}
}
