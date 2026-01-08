using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using InventoryApp.Model;
using InventoryApp.MVVM;
using InventoryApp.View;

namespace InventoryApp.ViewModel
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private readonly CollectionViewSource _productsViewSource;
		public List<Category> Categories { get; set; }
		public ObservableCollection<Product> Products { get; set; }

		public MainWindowViewModel()
		{
			Categories = [
				new() { Id=0, Name = "All Categories" },
				new() { Id=1, Name = "Electronics" },
				new() { Id=2, Name = "Furniture" },
				new() { Id=3, Name = "Office Supplies" }
				];
			slectedCategory = Categories[0];

			Products = [
				new Product { Name = "Laptop", Price = 1299.99m, Quantity = 15, Category = Categories[1], CreatedAt = DateTime.Now.AddDays(-30) },
				new Product { Name = "Wireless Mouse", Price = 29.99m, Quantity = 50, Category = Categories[1], CreatedAt = DateTime.Now.AddDays(-25) },
				new Product { Name = "Mechanical Keyboard", Price = 89.99m, Quantity = 30, Category = Categories[1], CreatedAt = DateTime.Now.AddDays(-20) },
				new Product { Name = "Office Chair", Price = 249.99m, Quantity = 20, Category = Categories[2], CreatedAt = DateTime.Now.AddDays(-15) },
				new Product { Name = "Standing Desk", Price = 449.99m, Quantity = 10, Category = Categories[2], CreatedAt = DateTime.Now.AddDays(-10) },
				new Product { Name = "Monitor Stand", Price = 39.99m, Quantity = 25, Category = Categories[2], CreatedAt = DateTime.Now.AddDays(-8) },
				new Product { Name = "Notebook Pack", Price = 12.99m, Quantity = 100, Category = Categories[3], CreatedAt = DateTime.Now.AddDays(-5) },
				new Product { Name = "Pen Set", Price = 8.99m, Quantity = 150, Category = Categories[3], CreatedAt = DateTime.Now.AddDays(-3) },
				new Product { Name = "Stapler", Price = 15.99m, Quantity = 40, Category = Categories[3], CreatedAt = DateTime.Now.AddDays(-2) },
				new Product { Name = "Paper Shredder", Price = 79.99m, Quantity = 12, Category = Categories[3], CreatedAt = DateTime.Now.AddDays(-1) }
				];

			_productsViewSource = new CollectionViewSource { Source = Products };
			_productsViewSource.Filter += _productsViewSource_Filter;
		}

		private void _productsViewSource_Filter(object sender, FilterEventArgs e)
		{
			if (e.Item is Product product)
			{
				bool matchesSearch = string.IsNullOrWhiteSpace(SearchText) || product.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
				bool matchesCategory = SelectedCategory == null || SelectedCategory.Id == 0 || product.CategoryId == SelectedCategory.Id;
				e.Accepted = matchesSearch && matchesCategory;
			}
		}

		public ICollectionView ProductsView => _productsViewSource.View;

		private Product slectedProduct;
		public Product SelectedProduct
		{
			get { return slectedProduct; }
			set
			{
				slectedProduct = value;
				OnPropertyChange();
			}
		}

		private Category slectedCategory;
		public Category SelectedCategory
		{
			get { return slectedCategory; }
			set
			{
				slectedCategory = value;
				OnPropertyChange();
				_productsViewSource.View.Refresh();
			}
		}

		private string searchText;
		public string SearchText
		{
			get { return searchText; }
			set
			{
				searchText = value;
				OnPropertyChange();
				_productsViewSource.View.Refresh();
			}
		}


		public RelayCommand EditCommand => new(execute => EditAction(), canExecute => SelectedProduct != null);
		public RelayCommand DeleteCommand => new(execute => DeleteAction(), canExecute => SelectedProduct != null);

		private void DeleteAction()
		{
			var result = MessageBox.Show($"Are you sure you want to delete {SelectedProduct.Name}?", "Confirm Delte", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
				Products.Remove(SelectedProduct);
		}

		public RelayCommand AddCommand => new(execute => AddAction());

		private void AddAction()
		{
			ProductViewModel pvm = new(null, Products, Categories);
			ProductModal modal = new() { Title = "Add", DataContext = pvm };
			modal.ShowDialog();
		}

		public void EditAction()
		{
			ProductViewModel pvm = new(SelectedProduct, Products, Categories);
			ProductModal modal = new() { Title = "Edit", DataContext = pvm };
			modal.ShowDialog();
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string PropertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
	};
}
