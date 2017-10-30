using System.Windows;

namespace ShareOnSocial
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			Setup();

			Current.MainWindow = new MainWindow();
			Current.MainWindow.Show();
		}

		/// <summary>
		/// Setup anything that is required for the app
		/// </summary>
		private void Setup()
		{

		}
	}
}
