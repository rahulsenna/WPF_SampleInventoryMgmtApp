using System.Windows;
using InventoryApp.Common;
using InventoryApp.Data;
using InventoryApp.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

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
	public async Task<Result> SaveChangesAsync(Product product)
	{
		try
		{
			await _context.SaveChangesAsync();
			return Result.Success();
		}
		catch (DbUpdateException ex) when (ex.InnerException is SqliteException)
		{
			_context.Entry(product).State = EntityState.Detached;
			return Result.Failure("Database validation failed: " + ex.InnerException?.Message);
		}
		catch (Exception ex)
		{
			return Result.Failure("Unexpected error: " + ex.Message);
		}
	}
	public async Task<Result> AddProductAsync(Product product)
	{
		product.CreatedAt = DateTime.Now;
		await _context.Products.AddAsync(product);
		return await SaveChangesAsync(product);
	}

	public async Task<Result> UpdateProductAsync(Product product)
	{
		_context.Products.Update(product);
		return await SaveChangesAsync(product);
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
