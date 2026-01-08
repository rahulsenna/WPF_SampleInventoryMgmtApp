using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InventoryApp.Model;
using InventoryApp.MVVM;

namespace InventoryApp.ViewModel
{
	public class ProductViewModel : INotifyPropertyChanged
	{
		private Product _product;
		private List<Category> _categories;
		ObservableCollection<Product> _items;

		public RelayCommand SaveCommand => new(execute => SaveAction());
		private void SaveAction()
		{
			_items.Remove(_product);
			_product.Category = SelectedCategory;
			_items.Add(_product);
		}


		public ProductViewModel(Product? product, ObservableCollection<Product> items, List<Category> categories)
		{
			_product = product ?? new Product();
			_items = items;
			_categories = categories;
			SelectedCategory = _product.Category ?? _categories[1];
		}

		public string Name
		{
			get { return _product.Name; }
			set { _product.Name = value; }
		}

		public decimal Price
		{
			get { return _product.Price; }
			set { _product.Price = value; }
		}

		public int Quantity
		{
			get { return _product.Quantity; }
			set { _product.Quantity = value; }
		}

		public List<Category> Categories
		{
			get { return _categories; }
		}

		private Category selectedCategory;

		public Category SelectedCategory
		{
			get { return selectedCategory; }
			set { selectedCategory = value; OnPropertyChange(); }
		}
		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChange([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		
	};
}
