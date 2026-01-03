using System.Collections.ObjectModel;
using System.ComponentModel;
using InventoryApp.Model;

namespace InventoryApp.ViewModel
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Product> Products { get; set; }

		public MainWindowViewModel()
		{

			Category cat1 = new() { Name = "Category 1"};
			Category cat2 = new() { Name = "Category 2"};
			Category cat3 = new() { Name = "Category 3"};
			Products = [
				new Product { Name = "Product1", Price = 3.3m, Quantity = 43, Category=cat1 },
				new Product { Name = "Product2", Price = 2.6m, Quantity = 60, Category=cat2 },
				new Product { Name = "Product3", Price = 500.2m, Quantity = 2, Category=cat3 }
				];
		}

		public event PropertyChangedEventHandler? PropertyChanged;
	};
}
