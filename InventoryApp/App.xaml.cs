using System.Windows;
using InventoryApp.View;
using InventoryApp.ViewModel;

namespace InventoryApp
{
	public partial class App : Application
	{
		private void OnStartup(object sender, StartupEventArgs e)
		{
			var mainWindow = new MainWindow();
			var mainViewModel = new MainWindowViewModel();
			mainWindow.DataContext = mainViewModel;
			mainWindow.Show();
		}
	}

}
