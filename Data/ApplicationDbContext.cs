using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

   
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatID);
            entity.Property(e => e.CatID).HasColumnType("int");
            entity.Property(e => e.CatName).HasColumnType("nvarchar(50)");
            entity.Property(e => e.Description).HasColumnType("nvarchar(max)");
            entity.Property(e => e.Thumb).HasColumnType("nvarchar(50)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductID);
            entity.Property(e => e.ProductID).HasColumnType("int");
            entity.Property(e => e.ProductName).HasColumnType("nvarchar(255)");
            entity.Property(e => e.CatName).HasColumnType("nvarchar(50)");
            entity.Property(e => e.CatID).HasColumnType("int");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Thumb).HasColumnType("nvarchar(255)");

            entity.HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CatID);
        });
    }
}
