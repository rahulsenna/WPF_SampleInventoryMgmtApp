using Microsoft.EntityFrameworkCore;
using InventoryApp.Model;
using InventoryApp.Data;

namespace InventoryApp.Services;

public class ProductService : IProductService
{
	private readonly AppDbContext _context;
	public ProductService(AppDbContext context)
	{
		_context = context;
	}
	public async Task<List<Product>> GetAllProductsAsync()
	{
		return await _context.Products
				.Include(p => p.Category)
				.OrderByDescending(p => p.CreatedAt)
				.ToListAsync();
	}
	public async Task<List<Category>> GetAllCategoriesAsync()
	{
		return await _context.Categories
				.OrderByDescending(c => c.Name)
				.ToListAsync();
	}
	public async Task<Product?> GetProductByIdAsync(int id)
	{
		return await _context.Products
				.Include(p => p.Category)
				.FirstOrDefaultAsync(p => p.Id == id);
	}
	public async Task AddProductAsync(Product product)
	{
		product.CreatedAt = DateTime.Now;
		await _context.Products.AddAsync(product);
		await _context.SaveChangesAsync();
	}
	public async Task UpdateProductAsync(Product product)
	{
		_context.Products.Update(product);
		await _context.SaveChangesAsync();
	}
	public async Task DeleteProductAsync(int id)
	{
		Product product = await _context.Products.FindAsync(id);
		if (product != null)
		{
			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
		}
	}
}
