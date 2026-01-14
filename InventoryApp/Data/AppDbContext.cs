using InventoryApp.Model;
using Microsoft.EntityFrameworkCore;
namespace InventoryApp.Data;

public class AppDbContext : DbContext
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Category>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasIndex(e => e.Name).IsUnique();
			entity.Property(e => e.Name).IsRequired();
		});

		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name).IsRequired();
			entity.Property(e => e.Price).HasPrecision(18, 2);
			entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");

			entity.ToTable("Products", t =>
			{
				t.HasCheckConstraint("Name_Required", "LENGTH(Name) > 0");
				t.HasCheckConstraint("Quantity_Must_Be_Positive", "CAST(Quantity AS INTEGER) > 0");
				t.HasCheckConstraint("Price_Must_Be_Positive", "CAST(Price AS REAL) > 0");

				//t.HasCheckConstraint("Quantity_Must_Be_Integer", "Quantity = CAST(Quantity AS INTEGER)");
				//t.HasCheckConstraint("Quantity_Must_Be_Non_Negative", "Quantity >= 0");
			});

			entity.HasOne(e => e.Category)
					.WithMany(c => c.Products)
					.HasForeignKey(e => e.CategoryId)
					.OnDelete(DeleteBehavior.Restrict);
		});
	}

}