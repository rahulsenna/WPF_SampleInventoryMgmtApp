using InventoryApp.Common;
using InventoryApp.Model;

namespace InventoryApp.Services;

public interface IProductService
{
	Task<List<Product>> GetAllProductsAsync();
	Task<List<Category>> GetAllCategoriesAsync();
	Task<Product?> GetProductByIdAsync(int id);
	Task<Result> AddProductAsync(Product product);
	Task<Result> UpdateProductAsync(Product product);
	Task DeleteProductAsync(int id);
}