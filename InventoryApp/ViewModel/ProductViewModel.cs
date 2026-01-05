using System.Collections.ObjectModel;
using System.ComponentModel;
using InventoryApp.Model;
using InventoryApp.MVVM;

namespace InventoryApp.ViewModel
{
	public class ProductViewModel : INotifyPropertyChanged
	{
		private Product _product;
		ObservableCollection<Product> _items;

		public RelayCommand SaveCommand => new(execute => SaveAction());
		private void SaveAction()
		{
			_items.Remove(_product);
			_items.Add(_product);
		}


		public ProductViewModel(Product? product, ObservableCollection<Product> items, List<Category> categories)
		{
			_product = product ?? new Product();
			_items = items;
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


		public event PropertyChangedEventHandler? PropertyChanged;
	};
}
