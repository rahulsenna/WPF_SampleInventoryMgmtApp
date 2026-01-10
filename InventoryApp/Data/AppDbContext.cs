using InventoryApp.Model;
using Microsoft.EntityFrameworkCore;
namespace InventoryApp.Data;

public class AppDbContext : DbContext
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	/*protected override void OnModelCreating(ModelBuilder modelBuilder)
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
			//entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");

			entity.HasOne(e => e.Category)
					.WithMany(c => c.Products)
					.HasForeignKey(e => e.CategoryId)
					.OnDelete(DeleteBehavior.Restrict);
		});
	}
	*/
}