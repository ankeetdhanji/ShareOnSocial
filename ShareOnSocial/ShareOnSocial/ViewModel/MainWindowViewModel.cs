using System;
using System.Windows;
using System.Windows.Input;

namespace ShareOnSocial.ViewModel
{
	public class MainWindowViewModel : BaseViewModel
	{
		#region Private Members
		private int mTitleHeight = 40;
		private Window mWindow;

		#endregion

		#region Public Properties

		public int TitleHeight
		{
			get { return mTitleHeight; }
			set { mTitleHeight = value; }
		}

		#endregion

		#region Public Commands

		public ICommand MinimizeCommand { get; set; }
		public ICommand MaximizeCommand { get; set; }
		public ICommand CloseCommand { get; set; }

		#endregion

		#region Constructor

		public MainWindowViewModel(Window window)
		{
			mWindow = window;
			MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
			MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
			CloseCommand = new RelayCommand(() => mWindow.Close());
		}

		#endregion
	}
}
