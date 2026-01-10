using System;
using System.Collections.Generic;
using System.Text;
using InventoryApp.Model;

namespace InventoryApp.Data
{
    public static class DbInitializer
    {
      public async static Task InitializeAsync(AppDbContext context)
      {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();
        // Check if there are any categories already
        if (context.Categories.Any())
        {
          return; // DB has been seeded
        }
        // Seed Categories
        var categories = new List<Category>
        {
          new Category { Name = "Electronics" },
          new Category { Name = "Books" },
          new Category { Name = "Clothing" },
          new Category { Name = "Furniture" },
          new Category { Name = "Office Supplies" }
        };
			await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
        // Seed Products
        var products = new List<Product>
        {
          new Product { Name = "Laptop", Price = 999.99m, Quantity = 50, CategoryId = categories[0].Id },
          new Product { Name = "Smartphone", Price = 499.99m, Quantity = 5, CategoryId = categories[0].Id },
          new Product { Name = "Novel", Price = 19.99m, Quantity = 40, CategoryId = categories[1].Id },
          new Product { Name = "T-Shirt", Price = 9.99m, Quantity = 2, CategoryId = categories[2].Id },
					new Product { Name = "Macbook", Price = 1699.99m, Quantity = 15, Category = categories[1], CreatedAt = DateTime.Now.AddDays(-30) },
				  new Product { Name = "Wireless Mouse", Price = 29.99m, Quantity = 50, Category = categories[1], CreatedAt = DateTime.Now.AddDays(-25) },
				  new Product { Name = "Mechanical Keyboard", Price = 89.99m, Quantity = 30, Category = categories[1], CreatedAt = DateTime.Now.AddDays(-20) },
				  new Product { Name = "Office Chair", Price = 249.99m, Quantity = 20, Category = categories[2], CreatedAt = DateTime.Now.AddDays(-15) },
				  new Product { Name = "Standing Desk", Price = 449.99m, Quantity = 10, Category = categories[2], CreatedAt = DateTime.Now.AddDays(-10) },
				  new Product { Name = "Monitor Stand", Price = 39.99m, Quantity = 25, Category = categories[2], CreatedAt = DateTime.Now.AddDays(-8) },
				  new Product { Name = "Notebook Pack", Price = 12.99m, Quantity = 100, Category = categories[3], CreatedAt = DateTime.Now.AddDays(-5) },
				  new Product { Name = "Pen Set", Price = 8.99m, Quantity = 150, Category = categories[3], CreatedAt = DateTime.Now.AddDays(-3) },
				  new Product { Name = "Stapler", Price = 15.99m, Quantity = 40, Category = categories[3], CreatedAt = DateTime.Now.AddDays(-2) },
				  new Product { Name = "Paper Shredder", Price = 79.99m, Quantity = 12, Category = categories[3], CreatedAt = DateTime.Now.AddDays(-1) }
				};
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
		}
	}
}
