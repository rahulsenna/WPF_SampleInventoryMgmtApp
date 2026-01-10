using System.ComponentModel.DataAnnotations;

namespace InventoryApp.Model
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;

		public ICollection<Product> Products { get; set; } = [];
	}
}
