using System.Windows;
using InventoryApp.Data;
using InventoryApp.Services;
using InventoryApp.View;
using InventoryApp.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp
{
	public partial class App : Application
	{
		private async void OnStartup(object sender, StartupEventArgs e)
		{
			AppDbContext appDbContext = new(new DbContextOptionsBuilder<AppDbContext>().UseSqlite("Data Source=inventory.db").Options);
			await DbInitializer.InitializeAsync(appDbContext);
			IProductService productService = new ProductService(appDbContext);
			MainWindowViewModel mainViewModel = new(productService);
			MainWindow mainWindow = new();
			mainWindow.DataContext = mainViewModel;
			mainWindow.Show();
		}
	}

}