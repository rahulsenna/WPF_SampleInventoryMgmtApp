using System.Windows;
using InventoryApp.Data;
using InventoryApp.Services;
using InventoryApp.View;
using InventoryApp.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InventoryApp
{
	public partial class App : Application
	{
		private readonly IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
		{
			services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=inventory.db"));
			services.AddScoped<IProductService, ProductService>();
			services.AddTransient<MainWindowViewModel>();
			services.AddSingleton<MainWindow>();
		})
		.Build();
		private async void OnStartup(object sender, StartupEventArgs e)
		{
			await _host.StartAsync();
			await DbInitializer.InitializeAsync(_host.Services.GetRequiredService<AppDbContext>());

			var mainWindow = _host.Services.GetRequiredService<MainWindow>();
			var mainViewModel = _host.Services.GetRequiredService<MainWindowViewModel>();
			mainWindow.DataContext = mainViewModel;
			mainWindow.Show();

			/*AppDbContext appDbContext = new(new DbContextOptionsBuilder<AppDbContext>().UseSqlite("Data Source=inventory.db").Options);
			await DbInitializer.InitializeAsync(appDbContext);
			IProductService productService = new ProductService(appDbContext);
			MainWindowViewModel mainViewModel = new(productService);
			MainWindow mainWindow = new();
			mainWindow.DataContext = mainViewModel;
			mainWindow.Show();*/
		}
	}

}