using MealMate_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealMate_backend.Infrastructure.Data;

public class MealMateDbContext : DbContext
{
    public MealMateDbContext(DbContextOptions<MealMateDbContext> options)
        : base(options)
    {
    }

    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<DishProduct> DishProducts => Set<DishProduct>();
    public DbSet<MealGroup> MealGroups => Set<MealGroup>();
    public DbSet<MealGroupDish> MealGroupDishes => Set<MealGroupDish>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DishProduct>(entity =>
        {
            entity.HasKey(dp => new { dp.DishId, dp.ProductId });

            entity.HasOne(dp => dp.Dish)
                .WithMany(d => d.DishProducts)
                .HasForeignKey(dp => dp.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(dp => dp.Product)
                .WithMany(p => p.DishProducts)
                .HasForeignKey(dp => dp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MealGroupDish>(entity =>
        {
            entity.HasKey(mgd => new { mgd.MealGroupId, mgd.DishId });

            entity.HasOne(mgd => mgd.MealGroup)
                .WithMany(mg => mg.MealGroupDishes)
                .HasForeignKey(mgd => mgd.MealGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(mgd => mgd.Dish)
                .WithMany(d => d.MealGroupDishes)
                .HasForeignKey(mgd => mgd.DishId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
