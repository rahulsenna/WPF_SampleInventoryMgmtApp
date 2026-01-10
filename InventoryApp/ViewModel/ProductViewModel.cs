using System.ComponentModel;
using System.Runtime.CompilerServices;
using InventoryApp.Model;

namespace InventoryApp.ViewModel
{
	public class ProductViewModel : INotifyPropertyChanged
	{
		public Product product;
		private List<Category> _categories;

		public ProductViewModel(List<Category> categories, Product? existingProduct = null)
		{
			product = existingProduct ?? new Product();
			_categories = categories;
			SelectedCategory = product.Category ?? _categories[1];
		}

		public string Name
		{
			get { return product.Name; }
			set { product.Name = value; }
		}

		public decimal Price
		{
			get { return product.Price; }
			set { product.Price = value; }
		}

		public int Quantity
		{
			get { return product.Quantity; }
			set { product.Quantity = value; }
		}

		public List<Category> Categories
		{
			get { return _categories; }
		}

		private Category selectedCategory;

		public Category SelectedCategory
		{
			get { return selectedCategory; }
			set
			{
				selectedCategory = value;
				product.Category = selectedCategory;
				product.CategoryId = selectedCategory.Id;
				OnPropertyChange();
			}
		}
		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChange([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	};
}
