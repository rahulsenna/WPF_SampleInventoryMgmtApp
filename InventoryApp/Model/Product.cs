using System.ComponentModel.DataAnnotations;

namespace InventoryApp.Model
{
	public class Product
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Product name is required")]
		[StringLength(100, MinimumLength = 5, ErrorMessage = "Product name cannot exceed 100 characters")]
		public string Name { get; set; } = string.Empty;

		[Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative")]
		public decimal Price { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative")]
		public int Quantity { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "Product name is required")]
		[Range(1, int.MaxValue, ErrorMessage = "Select a valid Cateogry")]
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
	}
}
