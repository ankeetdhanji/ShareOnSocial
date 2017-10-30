using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShareOnSocial.Expressions;

namespace ShareOnSocial.ViewModel
{
	/// <summary>
	/// Based view model, Fody weaver triggers property changed events.
	/// </summary>
	public class BaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// This is triggered when any child properties value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

		/// <summary>
		/// When this is called <see cref="PropertyChanged"/> is triggered.
		/// </summary>
		/// <param name="name"></param>
		public void OnPropertyChanged(string name)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		/// <summary>
		/// This runs a command only if the updating flag is not set.
		/// </summary>
		/// <param name="updatingflag">Flag that defines if the command is running</param>
		/// <param name="action">Action to run is command is not running</param>
		/// <returns>Task to keep an eye on the command running.</returns>
		protected async Task RunCommandAsync(Expression<Func<bool>> updatingflag, Func<Task> action)
		{
			if (updatingflag.GetPropertyValue())
				return;

			updatingflag.SetPropertyValue(true);

			try
			{
				await action();
			}
			finally
			{
				updatingflag.SetPropertyValue(false);
			}
		}
	}
}
