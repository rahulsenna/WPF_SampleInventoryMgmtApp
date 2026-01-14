using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using InventoryApp.Model;
using InventoryApp.MVVM;
using InventoryApp.Services;
using InventoryApp.View;

namespace InventoryApp.ViewModel
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private readonly CollectionViewSource _productsViewSource;
		public List<Category> Categories { get; set; }
		public ObservableCollection<Product> Products { get; set; }
		IProductService _productService;

		public MainWindowViewModel(IProductService productService)
		{
			_productService = productService;
			Products = [];
			Categories = [];

			_productsViewSource = new CollectionViewSource { Source = Products };
			_productsViewSource.Filter += _productsViewSource_Filter;
			_ = LoadDataAsync();
		}

		private async Task LoadDataAsync()
		{
			var products = await _productService.GetAllProductsAsync();
			Products.Clear();
			foreach (var product in products)
			{
				Products.Add(product);
			}
			var categories = await _productService.GetAllCategoriesAsync();
			Categories.Clear();
			Categories.Add(new Category { Id = 0, Name = "All Categories" });
			foreach (var category in categories)
			{
				Categories.Add(category);
			}
			SelectedCategory = Categories.FirstOrDefault();
		}

		private void _productsViewSource_Filter(object sender, FilterEventArgs e)
		{
			if (e.Item is Product product)
			{
				bool matchesSearch = string.IsNullOrWhiteSpace(SearchText) || product.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
				bool matchesCategory = SelectedCategory == null || SelectedCategory.Id == 0 || product.Category?.Id == SelectedCategory.Id;
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
		public RelayCommandAsync EditCommand => new(execute => EditAction(), canExecute => SelectedProduct != null);
		public RelayCommandAsync DeleteCommand => new(execute => DeleteAction(), canExecute => SelectedProduct != null);
		public RelayCommandAsync RefreshCommand => new(execute => LoadDataAsync());

		private async Task DeleteAction()
		{
			var result = MessageBox.Show($"Are you sure you want to delete {SelectedProduct.Name}?", "Confirm Delte", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
				Products.Remove(SelectedProduct);
		}

		public RelayCommandAsync AddCommand => new(execute => AddActionAsync());

		private async Task AddActionAsync()
		{
			ProductViewModel pvm = new(Categories);
			ProductModal modal = new() { Title = "Add", DataContext = pvm, Owner = Application.Current.MainWindow };
			
			if (modal.ShowDialog() == true && pvm.product != null)
			{
				var res = await _productService.AddProductAsync(pvm.product);
				if (!res.IsSuccess)
				{
					MessageBox.Show($"Error adding product: {res.Error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				await LoadDataAsync();
			}
		}

		private async Task EditAction()
		{
			ProductViewModel pvm = new(Categories, SelectedProduct);
			ProductModal modal = new() { Title = "Edit", DataContext = pvm };
			if (modal.ShowDialog() == true && pvm.product != null)
			{
				await _productService.UpdateProductAsync(pvm.product);
				await LoadDataAsync();
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string PropertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
	};
}
