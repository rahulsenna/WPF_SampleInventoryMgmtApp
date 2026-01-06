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
		public MainWindowViewModel()
		{
			Categories = [
				new() { Name = "Category 1" },
				new() { Name = "Category 2" },
				new() { Name = "Category 3" },
				];

			Products = [
				new Product { Name = "Product1", Price = 3.3m, Quantity = 43, Category=Categories[0] },
				new Product { Name = "Product2", Price = 2.6m, Quantity = 60, Category=Categories[1] },
				new Product { Name = "Product3", Price = 500.2m, Quantity = 2, Category=Categories[2] }
				];

			_productsViewSource = new CollectionViewSource { Source = Products };
			_productsViewSource.Filter += _productsViewSource_Filter;
		}

		private void _productsViewSource_Filter(object sender, FilterEventArgs e)
		{
			if (e.Item is Product product)
			{
				bool matchesSearch = string.IsNullOrWhiteSpace(SearchText) || product.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
				e.Accepted = matchesSearch;
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
		public ObservableCollection<Product> Products { get; set; }

		private string searchText;

		public string SearchText
		{
			get { return searchText; }
			set
			{
				searchText = value; OnPropertyChange();
				_productsViewSource.View.Refresh();
			}
		}

		public List<Product> GetAllProducts
		{
			get { return Products.Where(p => string.IsNullOrEmpty(SearchText) || p.Name.Contains(SearchText)).ToList(); }
		}
		public List<Category> Categories { get; set; }

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
