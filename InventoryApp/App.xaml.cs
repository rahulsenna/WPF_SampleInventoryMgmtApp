using System.Windows;
using InventoryApp.View;
using InventoryApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InventoryApp
{
	public partial class App : Application
	{
		private readonly IHost _host;

		public App()
		{
			_host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
			{
				services.AddTransient<MainWindowViewModel>();
				services.AddSingleton<MainWindow>();
			}).Build();

		}
		private void OnStartup(object sender, StartupEventArgs e)
		{
			_host.Start();
			var mainWindow = _host.Services.GetRequiredService<MainWindow>();
			mainWindow.DataContext = _host.Services.GetRequiredService<MainWindowViewModel>();
			mainWindow.Show();
		}
	}

}
