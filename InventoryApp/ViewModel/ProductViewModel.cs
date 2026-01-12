using System.ComponentModel;
using System.Runtime.CompilerServices;
using InventoryApp.Model;

namespace InventoryApp.ViewModel
{
	public class ProductViewModel : INotifyPropertyChanged
	{
		public Product product;
		private List<Category> _categories;
		private string _nameError = string.Empty;
		private string _priceError = string.Empty;
		private string _quantityError = string.Empty;
		private string _categoryError = string.Empty;
		private Category _selectedCategory;

		public ProductViewModel(List<Category> categories, Product? existingProduct = null)
		{
			product = existingProduct ?? new Product();
			_categories = categories;

			if (product.Category != null)
			{
				_selectedCategory = product.Category;
			}
			else if (_categories != null && _categories.Count > 0)
			{
				_selectedCategory = _categories[0];
			}
		}

		public string Name
		{
			get { return product.Name; }
			set
			{
				if (product.Name != value)
				{
					product.Name = value;
					OnPropertyChange();
					ValidateName();
				}
			}
		}

		public decimal Price
		{
			get { return product.Price; }
			set
			{
				if (product.Price != value)
				{
					product.Price = value;
					OnPropertyChange();
					ValidatePrice();
				}
			}
		}

		public int Quantity
		{
			get { return product.Quantity; }
			set
			{
				if (product.Quantity != value)
				{
					product.Quantity = value;
					OnPropertyChange();
					ValidateQuantity();
				}
			}
		}

		public List<Category> Categories
		{
			get { return _categories; }
		}

		public Category SelectedCategory
		{
			get { return _selectedCategory; }
			set
			{
				if (_selectedCategory != value)
				{
					_selectedCategory = value;
					product.Category = _selectedCategory;
					product.CategoryId = _selectedCategory?.Id ?? 0;
					OnPropertyChange();
					OnPropertyChange(nameof(CategoryId));
					ValidateCategory();
				}
			}
		}

		public int CategoryId
		{
			get => product.CategoryId;
			set
			{
				if (product.CategoryId != value)
				{
					product.CategoryId = value;
					OnPropertyChange();
					ValidateCategory();
				}
			}
		}

		public string NameError
		{
			get => _nameError;
			set
			{
				if (_nameError != value)
				{
					_nameError = value;
					OnPropertyChange();
					OnPropertyChange(nameof(IsValid));
				}
			}
		}

		public string PriceError
		{
			get => _priceError;
			set
			{
				if (_priceError != value)
				{
					_priceError = value;
					OnPropertyChange();
					OnPropertyChange(nameof(IsValid));
				}
			}
		}

		public string QuantityError
		{
			get => _quantityError;
			set
			{
				if (_quantityError != value)
				{
					_quantityError = value;
					OnPropertyChange();
					OnPropertyChange(nameof(IsValid));
				}
			}
		}

		public string CategoryError
		{
			get => _categoryError;
			set
			{
				if (_categoryError != value)
				{
					_categoryError = value;
					OnPropertyChange();
					OnPropertyChange(nameof(IsValid));
				}
			}
		}

		public bool IsValid => ValidateAll();

		private void ValidateName()
		{
			NameError = string.IsNullOrWhiteSpace(Name) ? "Product name is required" : string.Empty;
		}

		private void ValidatePrice()
		{
			PriceError = Price <= 0 ? "Price must be non-negative" : string.Empty;
		}

		private void ValidateQuantity()
		{
			QuantityError = Quantity <= 0 ? "Quantity must be non-negative" : string.Empty;
		}

		private void ValidateCategory()
		{
			CategoryError = CategoryId <= 0 ? "Category is required" : string.Empty;
		}

		private bool ValidateAll()
		{
			ValidateName();
			ValidatePrice();
			ValidateQuantity();
			ValidateCategory();

			return string.IsNullOrEmpty(NameError) &&
					 string.IsNullOrEmpty(PriceError) &&
					 string.IsNullOrEmpty(QuantityError) &&
					 string.IsNullOrEmpty(CategoryError);
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChange([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

}