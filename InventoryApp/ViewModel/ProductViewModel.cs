using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Linq;
using InventoryApp.Model;

namespace InventoryApp.ViewModel
{
	public class ProductViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
	{
		public Product product;
		private List<Category> _categories;
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
				CategoryId = _categories[0];
			}
		}

		public string Name
		{
			get => product.Name;
			set
			{
				if (product.Name != value)
				{
					product.Name = value;
					OnPropertyChange();
					ValidateProperty(value);
				}
			}
		}

		public decimal Price
		{
			get => product.Price;
			set
			{
				if (product.Price != value)
				{
					product.Price = value;
					OnPropertyChange();
					ValidateProperty(value);
				}
			}
		}

		public int Quantity
		{
			get => product.Quantity;
			set
			{
				if (product.Quantity != value)
				{
					product.Quantity = value;
					OnPropertyChange();
					ValidateProperty(value);
				}
			}
		}

		public List<Category> Categories => _categories;

		public Category CategoryId
		{
			get => _selectedCategory;
			set
			{
				if (_selectedCategory != value)
				{
					_selectedCategory = value;
					product.Category = _selectedCategory;
					product.CategoryId = _selectedCategory?.Id ?? 0;
					OnPropertyChange();
					ValidateProperty(product.CategoryId);
				}
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChange([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private readonly Dictionary<string, List<string>> _errors = new();
		public IEnumerable GetErrors(string? propertyName) => propertyName != null ? _errors.GetValueOrDefault(propertyName) ?? Enumerable.Empty<string>() : Enumerable.Empty<string>();
		public bool HasErrors => _errors.Any();
		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

		private void ValidateProperty<T>(T value, [CallerMemberName] string propertyName = null!)
		{
			_errors.Remove(propertyName);
			var context = new ValidationContext(product) { MemberName = propertyName };
			var results = new List<ValidationResult>();

			Validator.TryValidateProperty(value, context, results);
			if (results.Any())
			{
				_errors[propertyName] = results.Select(r => r.ErrorMessage).ToList();
			}

			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
			OnPropertyChange(nameof(IsValid));
		}
		public bool IsValid => !HasErrors && ValidateAll();

		private bool ValidateAll()
		{
			var context = new ValidationContext(product);
			var results = new List<ValidationResult>();
			return Validator.TryValidateObject(product, context, results, true);
		}

	}

}