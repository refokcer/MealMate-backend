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
    public DbSet<ShoppingListItem> ShoppingListItems => Set<ShoppingListItem>();
    public DbSet<BaseShoppingProduct> BaseShoppingProducts => Set<BaseShoppingProduct>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DishProduct>()
            .Property(dp => dp.Quantity)
            .HasPrecision(10, 2);

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

        modelBuilder.Entity<ShoppingListItem>(entity =>
        {
            entity.Property(item => item.Quantity).HasPrecision(10, 2);

            entity.HasIndex(item => item.ProductId).IsUnique();

            entity.HasOne(item => item.Product)
                .WithMany(product => product.ShoppingListItems)
                .HasForeignKey(item => item.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BaseShoppingProduct>(entity =>
        {
            entity.Property(item => item.Quantity).HasPrecision(10, 2);

            entity.HasIndex(item => item.ProductId).IsUnique();

            entity.HasOne(item => item.Product)
                .WithMany(product => product.BaseShoppingProducts)
                .HasForeignKey(item => item.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        var products = new[]
        {
            new Product { Id = 1, Unit = "шт.", Name = "РЇР№С†Р° РєСѓСЂРёРЅС‹Рµ", Category = "РњРѕР»РѕС‡РЅС‹Рµ Рё СЏР№С†Р°" },
            new Product { Id = 2, Unit = "мл", Name = "РњРѕР»РѕРєРѕ", Category = "РњРѕР»РѕС‡РЅС‹Рµ Рё СЏР№С†Р°" },
            new Product { Id = 3, Unit = "ломтик", Name = "РҐР»РµР± С†РµР»СЊРЅРѕР·РµСЂРЅРѕРІРѕР№", Category = "Р’С‹РїРµС‡РєР°" },
            new Product { Id = 4, Unit = "шт.", Name = "РђРІРѕРєР°РґРѕ", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 5, Unit = "шт.", Name = "РџРѕРјРёРґРѕСЂС‹ С‡РµСЂСЂРё", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 6, Unit = "горсть", Name = "РЁРїРёРЅР°С‚ СЃРІРµР¶РёР№", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 7, Unit = "г", Name = "РљСѓСЂРёРЅРѕРµ С„РёР»Рµ", Category = "РњСЏСЃРѕ Рё РїС‚РёС†Р°" },
            new Product { Id = 8, Unit = "г", Name = "Р РёСЃ Р¶Р°СЃРјРёРЅ", Category = "РљСЂСѓРїС‹" },
            new Product { Id = 9, Unit = "ст. л.", Name = "РЎРѕРµРІС‹Р№ СЃРѕСѓСЃ", Category = "РЎРѕСѓСЃС‹ Рё СЃРїРµС†РёРё" },
            new Product { Id = 10, Unit = "см", Name = "РРјР±РёСЂСЊ СЃРІРµР¶РёР№", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 11, Unit = "зубчик", Name = "Р§РµСЃРЅРѕРє", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 12, Unit = "шт.", Name = "РџРµСЂРµС† Р±РѕР»РіР°СЂСЃРєРёР№", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 13, Unit = "ст. л.", Name = "РћР»РёРІРєРѕРІРѕРµ РјР°СЃР»Рѕ", Category = "Р‘Р°РєР°Р»РµСЏ" },
            new Product { Id = 14, Unit = "шт.", Name = "Р›РёРјРѕРЅ", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 15, Unit = "г", Name = "Р›РѕСЃРѕСЃСЊ", Category = "Р С‹Р±Р° Рё РјРѕСЂРµРїСЂРѕРґСѓРєС‚С‹" },
            new Product { Id = 16, Unit = "г", Name = "РљРёРЅРѕР°", Category = "РљСЂСѓРїС‹" },
            new Product { Id = 17, Unit = "шт.", Name = "РћРіСѓСЂРµС†", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 18, Unit = "г", Name = "РЎС‹СЂ С„РµС‚Р°", Category = "РЎС‹СЂС‹" },
            new Product { Id = 19, Unit = "шт.", Name = "Р›СѓРє РєСЂР°СЃРЅС‹Р№", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 20, Unit = "г", Name = "Р™РѕРіСѓСЂС‚ РіСЂРµС‡РµСЃРєРёР№", Category = "РњРѕР»РѕС‡РЅС‹Рµ Рё СЏР№С†Р°" },
            new Product { Id = 21, Unit = "г", Name = "РћРІСЃСЏРЅС‹Рµ С…Р»РѕРїСЊСЏ", Category = "РљСЂСѓРїС‹" },
            new Product { Id = 22, Unit = "шт.", Name = "Р‘Р°РЅР°РЅ", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 23, Unit = "ст. л.", Name = "РњС‘Рґ", Category = "Р‘Р°РєР°Р»РµСЏ" },
            new Product { Id = 24, Unit = "ст. л.", Name = "РњРёРЅРґР°Р»СЊ", Category = "РћСЂРµС…Рё" },
            new Product { Id = 25, Unit = "шт.", Name = "РњРѕСЂРєРѕРІСЊ", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 26, Unit = "г", Name = "Р‘СЂРѕРєРєРѕР»Рё", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 27, Unit = "г", Name = "РўРѕРјР°С‚С‹ РєРѕРЅСЃРµСЂРІРёСЂРѕРІР°РЅРЅС‹Рµ", Category = "РљРѕРЅСЃРµСЂРІС‹" },
            new Product { Id = 28, Unit = "г", Name = "РњР°РєР°СЂРѕРЅС‹ РїРµРЅРЅРµ", Category = "Р‘Р°РєР°Р»РµСЏ" },
            new Product { Id = 29, Unit = "горсть", Name = "Р‘Р°Р·РёР»РёРє СЃРІРµР¶РёР№", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 30, Unit = "г", Name = "РЎС‹СЂ РїР°СЂРјРµР·Р°РЅ", Category = "РЎС‹СЂС‹" },
            new Product { Id = 31, Unit = "г", Name = "Р§РµС‡РµРІРёС†Р° РєСЂР°СЃРЅР°СЏ", Category = "РљСЂСѓРїС‹" },
            new Product { Id = 32, Unit = "мл", Name = "РљРѕРєРѕСЃРѕРІРѕРµ РјРѕР»РѕРєРѕ", Category = "Р‘Р°РєР°Р»РµСЏ" },
            new Product { Id = 33, Unit = "ст. л.", Name = "РљР°СЂСЂРё РїР°СЃС‚Р°", Category = "РЎРѕСѓСЃС‹ Рё СЃРїРµС†РёРё" },
            new Product { Id = 34, Unit = "шт.", Name = "Р›РµРїС‘С€РєРё РїС€РµРЅРёС‡РЅС‹Рµ", Category = "Р’С‹РїРµС‡РєР°" },
            new Product { Id = 35, Unit = "г", Name = "РљСѓРєСѓСЂСѓР·Р° Р·Р°РјРѕСЂРѕР¶РµРЅРЅР°СЏ", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 36, Unit = "мл", Name = "РћРІРѕС‰РЅРѕР№ Р±СѓР»СЊРѕРЅ", Category = "Р‘Р°РєР°Р»РµСЏ" },
            new Product { Id = 37, Unit = "г", Name = "РЇРіРѕРґС‹ Р·Р°РјРѕСЂРѕР¶РµРЅРЅС‹Рµ", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 38, Unit = "шт.", Name = "Р›Р°Р№Рј", Category = "РћРІРѕС‰Рё Рё С„СЂСѓРєС‚С‹" },
            new Product { Id = 39, Unit = "шт.", Name = "РњР°СЃР»РёРЅС‹", Category = "РљРѕРЅСЃРµСЂРІС‹" },
            new Product { Id = 40, Unit = "г", Name = "Р¤Р°СЃРѕР»СЊ С‡С‘СЂРЅР°СЏ РєРѕРЅСЃРµСЂРІРёСЂРѕРІР°РЅРЅР°СЏ", Category = "РљРѕРЅСЃРµСЂРІС‹" }
        };

        var dishes = new[]
        {
            new Dish
            {
                Id = 1,
                Name = "РћРјР»РµС‚ СЃРѕ С€РїРёРЅР°С‚РѕРј",
                Description = "Р’РѕР·РґСѓС€РЅС‹Р№ РѕРјР»РµС‚ СЃ СЃРѕС‡РЅС‹РјРё С‚РѕРјР°С‚Р°РјРё Рё СЃРІРµР¶РёРј С€РїРёРЅР°С‚РѕРј.",
                Instructions = "Р’Р·Р±РµР№С‚Рµ СЏР№С†Р° СЃ РјРѕР»РѕРєРѕРј, РѕР±Р¶Р°СЂСЊС‚Рµ С€РїРёРЅР°С‚ СЃ РїРѕРјРёРґРѕСЂР°РјРё РЅР° РѕР»РёРІРєРѕРІРѕРј РјР°СЃР»Рµ, Р·Р°Р»РµР№С‚Рµ СЏРёС‡РЅРѕР№ СЃРјРµСЃСЊСЋ Рё РіРѕС‚РѕРІСЊС‚Рµ РґРѕ РіРѕС‚РѕРІРЅРѕСЃС‚Рё.",
                PreparationMinutes = 15,
                ImageUrl = "https://static.mealmate.app/images/dishes/spinach-omelet.jpg"
            },
            new Dish
            {
                Id = 2,
                Name = "РўРѕСЃС‚ СЃ Р°РІРѕРєР°РґРѕ Рё СЏР№С†РѕРј",
                Description = "РҐСЂСѓСЃС‚СЏС‰РёР№ С†РµР»СЊРЅРѕР·РµСЂРЅРѕРІРѕР№ С‚РѕСЃС‚ СЃ РєСЂРµРјРѕРІС‹Рј Р°РІРѕРєР°РґРѕ Рё СЏР№С†РѕРј РїР°С€РѕС‚.",
                Instructions = "РџРѕРґР¶Р°СЂСЊС‚Рµ С…Р»РµР±, СЂР°Р·РѕРјРЅРёС‚Рµ Р°РІРѕРєР°РґРѕ СЃ Р»РёРјРѕРЅРЅС‹Рј СЃРѕРєРѕРј, РІС‹Р»РѕР¶РёС‚Рµ РЅР° С‚РѕСЃС‚ Рё РґРѕР±Р°РІСЊС‚Рµ СЏР№С†Рѕ РїР°С€РѕС‚, РїРѕСЃРѕР»РёС‚Рµ Рё РїРѕРїРµСЂС‡РёС‚Рµ РїРѕ РІРєСѓСЃСѓ.",
                PreparationMinutes = 12,
                ImageUrl = "https://static.mealmate.app/images/dishes/avocado-toast.jpg"
            },
            new Dish
            {
                Id = 3,
                Name = "РЎРјСѓР·Рё-Р±РѕР»Р» СЃ Р±Р°РЅР°РЅРѕРј",
                Description = "РЎРІРµР¶РёР№ Рё РїРёС‚Р°С‚РµР»СЊРЅС‹Р№ Р·Р°РІС‚СЂР°Рє СЃ Р±Р°РЅР°РЅРѕРј, Р№РѕРіСѓСЂС‚РѕРј Рё СЏРіРѕРґР°РјРё.",
                Instructions = "РЎРјРµС€Р°Р№С‚Рµ Р№РѕРіСѓСЂС‚, Р±Р°РЅР°РЅ, СЏРіРѕРґС‹ Рё РѕРІСЃСЏРЅРєСѓ РІ Р±Р»РµРЅРґРµСЂРµ, РїРµСЂРµР»РѕР¶РёС‚Рµ РІ РјРёСЃРєСѓ Рё СѓРєСЂР°СЃСЊС‚Рµ РјС‘РґРѕРј Рё РјРёРЅРґР°Р»С‘Рј.",
                PreparationMinutes = 8,
                ImageUrl = "https://static.mealmate.app/images/dishes/banana-smoothie-bowl.jpg"
            },
            new Dish
            {
                Id = 4,
                Name = "РљСѓСЂРёС†Р° С‚РµСЂРёСЏРєРё СЃ СЂРёСЃРѕРј",
                Description = "РЎРѕС‡РЅР°СЏ РєСѓСЂРёС†Р° РІ СЃРѕСѓСЃРµ С‚РµСЂРёСЏРєРё СЃ Р°СЂРѕРјР°С‚РЅС‹Рј Р¶Р°СЃРјРёРЅРѕРІС‹Рј СЂРёСЃРѕРј.",
                Instructions = "РћР±Р¶Р°СЂСЊС‚Рµ РєСѓСЂРёС†Сѓ СЃ С‡РµСЃРЅРѕРєРѕРј Рё РёРјР±РёСЂС‘Рј, РґРѕР±Р°РІСЊС‚Рµ СЃРѕРµРІС‹Р№ СЃРѕСѓСЃ, РЅРµРјРЅРѕРіРѕ РјС‘РґР° Рё С‚СѓС€РёС‚Рµ РґРѕ Р·Р°РіСѓСЃС‚РµРЅРёСЏ, РїРѕРґР°РІР°Р№С‚Рµ СЃ РѕС‚РІР°СЂРЅС‹Рј СЂРёСЃРѕРј Рё РѕРІРѕС‰Р°РјРё.",
                PreparationMinutes = 30,
                ImageUrl = "https://static.mealmate.app/images/dishes/teriyaki-chicken.jpg"
            },
            new Dish
            {
                Id = 5,
                Name = "РЎР°Р»Р°С‚ СЃ Р»РѕСЃРѕСЃРµРј Рё РєРёРЅРѕР°",
                Description = "РџРёС‚Р°С‚РµР»СЊРЅС‹Р№ СЃР°Р»Р°С‚ СЃ Р·Р°РїРµС‡С‘РЅРЅС‹Рј Р»РѕСЃРѕСЃРµРј, РєРёРЅРѕР° Рё СЃРІРµР¶РёРјРё РѕРІРѕС‰Р°РјРё.",
                Instructions = "РћС‚РІР°СЂРёС‚Рµ РєРёРЅРѕР°, Р·Р°РїРµРєРёС‚Рµ Р»РѕСЃРѕСЃСЊ, СЃРјРµС€Р°Р№С‚Рµ СЃ РѕРІРѕС‰Р°РјРё Рё Р·Р°РїСЂР°РІСЊС‚Рµ Р»РёРјРѕРЅРЅС‹Рј СЃРѕРєРѕРј Рё РѕР»РёРІРєРѕРІС‹Рј РјР°СЃР»РѕРј.",
                PreparationMinutes = 25,
                ImageUrl = "https://static.mealmate.app/images/dishes/quinoa-salmon-salad.jpg"
            },
            new Dish
            {
                Id = 6,
                Name = "РџР°СЃС‚Р° СЃ С‚РѕРјР°С‚Р°РјРё Рё Р±Р°Р·РёР»РёРєРѕРј",
                Description = "РљР»Р°СЃСЃРёС‡РµСЃРєР°СЏ РёС‚Р°Р»СЊСЏРЅСЃРєР°СЏ РїР°СЃС‚Р° СЃ РЅР°СЃС‹С‰РµРЅРЅС‹Рј С‚РѕРјР°С‚РЅС‹Рј СЃРѕСѓСЃРѕРј.",
                Instructions = "РћС‚РІР°СЂРёС‚Рµ РїР°СЃС‚Сѓ, РѕР±Р¶Р°СЂСЊС‚Рµ С‡РµСЃРЅРѕРє, РґРѕР±Р°РІСЊС‚Рµ С‚РѕРјР°С‚С‹ Рё С‚СѓС€РёС‚Рµ РґРѕ РіСѓСЃС‚РѕС‚С‹, СЃРјРµС€Р°Р№С‚Рµ СЃ РїР°СЃС‚РѕР№ Рё СЃРІРµР¶РёРј Р±Р°Р·РёР»РёРєРѕРј, РїРѕСЃС‹РїСЊС‚Рµ РїР°СЂРјРµР·Р°РЅРѕРј.",
                PreparationMinutes = 20,
                ImageUrl = "https://static.mealmate.app/images/dishes/tomato-basil-pasta.jpg"
            },
            new Dish
            {
                Id = 7,
                Name = "РЎСѓРї РёР· РєСЂР°СЃРЅРѕР№ С‡РµС‡РµРІРёС†С‹",
                Description = "РЎС‹С‚РЅС‹Р№ РєСЂРµРјРѕРІС‹Р№ СЃСѓРї СЃ РѕРІРѕС‰Р°РјРё Рё СЃРїРµС†РёСЏРјРё.",
                Instructions = "РћР±Р¶Р°СЂСЊС‚Рµ Р»СѓРє Рё РјРѕСЂРєРѕРІСЊ, РґРѕР±Р°РІСЊС‚Рµ С‡РµСЃРЅРѕРє, С‡РµС‡РµРІРёС†Сѓ, С‚РѕРјР°С‚С‹ Рё РѕРІРѕС‰РЅРѕР№ Р±СѓР»СЊРѕРЅ, РІР°СЂРёС‚Рµ РґРѕ РјСЏРіРєРѕСЃС‚Рё Рё РїСЂРѕР±РµР№С‚Рµ Р±Р»РµРЅРґРµСЂРѕРј.",
                PreparationMinutes = 35,
                ImageUrl = "https://static.mealmate.app/images/dishes/red-lentil-soup.jpg"
            },
            new Dish
            {
                Id = 8,
                Name = "РћРІСЃСЏРЅРєР° СЃ СЏРіРѕРґР°РјРё",
                Description = "РўС‘РїР»Р°СЏ РѕРІСЃСЏРЅРєР° СЃ Р№РѕРіСѓСЂС‚РѕРј, СЏРіРѕРґР°РјРё Рё РјС‘РґРѕРј.",
                Instructions = "РЎРІР°СЂРёС‚Рµ РѕРІСЃСЏРЅРєСѓ РЅР° РјРѕР»РѕРєРµ, РїРµСЂРµР»РѕР¶РёС‚Рµ РІ РјРёСЃРєСѓ, РґРѕР±Р°РІСЊС‚Рµ Р№РѕРіСѓСЂС‚, СЏРіРѕРґС‹ Рё РјС‘Рґ, РїРѕСЃС‹РїСЊС‚Рµ РјРёРЅРґР°Р»С‘Рј.",
                PreparationMinutes = 10,
                ImageUrl = "https://static.mealmate.app/images/dishes/berry-oatmeal.jpg"
            },
            new Dish
            {
                Id = 9,
                Name = "РўР°РєРѕ СЃ РєСѓСЂРёС†РµР№ РєР°СЂСЂРё",
                Description = "РЇСЂРєРёРµ С‚Р°РєРѕ СЃ РєСѓСЂРёС†РµР№ РІ РєРѕРєРѕСЃРѕРІРѕ-РєР°СЂСЂРё СЃРѕСѓСЃРµ Рё СЃРІРµР¶РёРјРё РѕРІРѕС‰Р°РјРё.",
                Instructions = "РџСЂРёРіРѕС‚РѕРІСЊС‚Рµ РєСѓСЂРёС†Сѓ СЃ РїР°СЃС‚РѕР№ РєР°СЂСЂРё Рё РєРѕРєРѕСЃРѕРІС‹Рј РјРѕР»РѕРєРѕРј, РїРѕРґР°РІР°Р№С‚Рµ РІ С‚С‘РїР»С‹С… Р»РµРїС‘С€РєР°С… СЃ РѕРІРѕС‰Р°РјРё Рё Р»Р°Р№РјРѕРј.",
                PreparationMinutes = 28,
                ImageUrl = "https://static.mealmate.app/images/dishes/curry-chicken-tacos.jpg"
            },
            new Dish
            {
                Id = 10,
                Name = "Р—Р°РїРµС‡С‘РЅРЅС‹Р№ Р»РѕСЃРѕСЃСЊ СЃ Р±СЂРѕРєРєРѕР»Рё",
                Description = "РќРµР¶РЅС‹Р№ Р»РѕСЃРѕСЃСЊ, Р·Р°РїРµС‡С‘РЅРЅС‹Р№ СЃ Р±СЂРѕРєРєРѕР»Рё Рё С‡РµСЃРЅРѕРєРѕРј.",
                Instructions = "РЎРјРµС€Р°Р№С‚Рµ РѕР»РёРІРєРѕРІРѕРµ РјР°СЃР»Рѕ СЃ С‡РµСЃРЅРѕРєРѕРј Рё Р»РёРјРѕРЅРЅС‹Рј СЃРѕРєРѕРј, РїРѕР»РµР№С‚Рµ Р»РѕСЃРѕСЃСЊ Рё Р±СЂРѕРєРєРѕР»Рё Рё Р·Р°РїРµРєР°Р№С‚Рµ РґРѕ РіРѕС‚РѕРІРЅРѕСЃС‚Рё.",
                PreparationMinutes = 22,
                ImageUrl = "https://static.mealmate.app/images/dishes/baked-salmon-broccoli.jpg"
            },
            new Dish
            {
                Id = 11,
                Name = "Р“СЂРµС‡РµСЃРєРёР№ СЃР°Р»Р°С‚",
                Description = "РљР»Р°СЃСЃРёС‡РµСЃРєРёР№ СЃР°Р»Р°С‚ СЃ РїРѕРјРёРґРѕСЂР°РјРё, РѕРіСѓСЂС†РѕРј, С„РµС‚РѕР№ Рё РјР°СЃР»РёРЅР°РјРё.",
                Instructions = "РќР°СЂРµР¶СЊС‚Рµ РѕРІРѕС‰Рё, РґРѕР±Р°РІСЊС‚Рµ С„РµС‚Сѓ Рё РјР°СЃР»РёРЅС‹, Р·Р°РїСЂР°РІСЊС‚Рµ РѕР»РёРІРєРѕРІС‹Рј РјР°СЃР»РѕРј Рё Р»РёРјРѕРЅРЅС‹Рј СЃРѕРєРѕРј.",
                PreparationMinutes = 15,
                ImageUrl = "https://static.mealmate.app/images/dishes/greek-salad.jpg"
            },
            new Dish
            {
                Id = 12,
                Name = "Р‘СѓСЂСЂРёС‚Рѕ СЃ РєРёРЅРѕР° Рё РѕРІРѕС‰Р°РјРё",
                Description = "РџРѕР»РµР·РЅС‹Рµ Р±СѓСЂСЂРёС‚Рѕ СЃ РєРёРЅРѕР°, РѕРІРѕС‰Р°РјРё Рё С„Р°СЃРѕР»СЊСЋ.",
                Instructions = "РћС‚РІР°СЂРёС‚Рµ РєРёРЅРѕР°, РѕР±Р¶Р°СЂСЊС‚Рµ РѕРІРѕС‰Рё СЃ С„Р°СЃРѕР»СЊСЋ Рё РєСѓРєСѓСЂСѓР·РѕР№, Р·Р°РІРµСЂРЅРёС‚Рµ РЅР°С‡РёРЅРєСѓ РІ Р»РµРїС‘С€РєРё Рё РїРѕРґР°РІР°Р№С‚Рµ СЃ Р»Р°Р№РјРѕРј.",
                PreparationMinutes = 30,
                ImageUrl = "https://static.mealmate.app/images/dishes/quinoa-veggie-burrito.jpg"
            }
        };

        var mealGroups = new[]
        {
            new MealGroup
            {
                Id = 1,
                Name = "РЈС‚СЂРµРЅРЅРёРµ СЌРЅРµСЂРіРёРё",
                Description = "Р›С‘РіРєРёРµ Рё Р±РѕРґСЂСЏС‰РёРµ РІР°СЂРёР°РЅС‚С‹ РґР»СЏ Р·Р°РІС‚СЂР°РєР°.",
                AccentColor = "#FFB74D"
            },
            new MealGroup
            {
                Id = 2,
                Name = "Р‘С‹СЃС‚СЂС‹Рµ РѕР±РµРґС‹",
                Description = "РЎС‹С‚РЅС‹Рµ Р±Р»СЋРґР°, РєРѕС‚РѕСЂС‹Рµ РіРѕС‚РѕРІСЏС‚СЃСЏ Р·Р° РїРѕР»С‡Р°СЃР°.",
                AccentColor = "#4FC3F7"
            },
            new MealGroup
            {
                Id = 3,
                Name = "РЎРµРјРµР№РЅС‹Рµ СѓР¶РёРЅС‹",
                Description = "РўС‘РїР»С‹Рµ Р±Р»СЋРґР° РґР»СЏ СѓСЋС‚РЅРѕРіРѕ РІРµС‡РµСЂР°.",
                AccentColor = "#9575CD"
            },
            new MealGroup
            {
                Id = 4,
                Name = "Р—РґРѕСЂРѕРІС‹Р№ РІС‹Р±РѕСЂ",
                Description = "Р‘Р°Р»Р°РЅСЃ Р±РµР»РєРѕРІ, Р¶РёСЂРѕРІ Рё СѓРіР»РµРІРѕРґРѕРІ РґР»СЏ Р°РєС‚РёРІРЅРѕРіРѕ РґРЅСЏ.",
                AccentColor = "#81C784"
            }
        };

        modelBuilder.Entity<Product>().HasData(products);
        modelBuilder.Entity<Dish>().HasData(dishes);
        modelBuilder.Entity<MealGroup>().HasData(mealGroups);

        modelBuilder.Entity<DishProduct>().HasData(
            new DishProduct { DishId = 1, ProductId = 1, Quantity = 3 },
            new DishProduct { DishId = 1, ProductId = 2, Quantity = 60 },
            new DishProduct { DishId = 1, ProductId = 5, Quantity = 6 },
            new DishProduct { DishId = 1, ProductId = 6, Quantity = 2 },
            new DishProduct { DishId = 1, ProductId = 13, Quantity = 1 },
            new DishProduct { DishId = 2, ProductId = 3, Quantity = 2 },
            new DishProduct { DishId = 2, ProductId = 4, Quantity = 1 },
            new DishProduct { DishId = 2, ProductId = 1, Quantity = 1 },
            new DishProduct { DishId = 2, ProductId = 14, Quantity = 1 },
            new DishProduct { DishId = 2, ProductId = 13, Quantity = 1 },
            new DishProduct { DishId = 3, ProductId = 20, Quantity = 150 },
            new DishProduct { DishId = 3, ProductId = 22, Quantity = 1 },
            new DishProduct { DishId = 3, ProductId = 21, Quantity = 30 },
            new DishProduct { DishId = 3, ProductId = 37, Quantity = 80 },
            new DishProduct { DishId = 3, ProductId = 23, Quantity = 1 },
            new DishProduct { DishId = 3, ProductId = 24, Quantity = 1 },
            new DishProduct { DishId = 4, ProductId = 7, Quantity = 400 },
            new DishProduct { DishId = 4, ProductId = 11, Quantity = 2 },
            new DishProduct { DishId = 4, ProductId = 10, Quantity = 2 },
            new DishProduct { DishId = 4, ProductId = 9, Quantity = 4 },
            new DishProduct { DishId = 4, ProductId = 8, Quantity = 200 },
            new DishProduct { DishId = 4, ProductId = 12, Quantity = 1 },
            new DishProduct { DishId = 4, ProductId = 26, Quantity = 150 },
            new DishProduct { DishId = 5, ProductId = 15, Quantity = 300 },
            new DishProduct { DishId = 5, ProductId = 16, Quantity = 150 },
            new DishProduct { DishId = 5, ProductId = 6, Quantity = 1 },
            new DishProduct { DishId = 5, ProductId = 17, Quantity = 1 },
            new DishProduct { DishId = 5, ProductId = 5, Quantity = 8 },
            new DishProduct { DishId = 5, ProductId = 14, Quantity = 1 },
            new DishProduct { DishId = 5, ProductId = 13, Quantity = 2 },
            new DishProduct { DishId = 6, ProductId = 28, Quantity = 250 },
            new DishProduct { DishId = 6, ProductId = 27, Quantity = 400 },
            new DishProduct { DishId = 6, ProductId = 11, Quantity = 2 },
            new DishProduct { DishId = 6, ProductId = 29, Quantity = 1 },
            new DishProduct { DishId = 6, ProductId = 30, Quantity = 30 },
            new DishProduct { DishId = 6, ProductId = 13, Quantity = 1 },
            new DishProduct { DishId = 7, ProductId = 31, Quantity = 200 },
            new DishProduct { DishId = 7, ProductId = 19, Quantity = 1 },
            new DishProduct { DishId = 7, ProductId = 25, Quantity = 2 },
            new DishProduct { DishId = 7, ProductId = 11, Quantity = 2 },
            new DishProduct { DishId = 7, ProductId = 27, Quantity = 200 },
            new DishProduct { DishId = 7, ProductId = 36, Quantity = 700 },
            new DishProduct { DishId = 8, ProductId = 21, Quantity = 60 },
            new DishProduct { DishId = 8, ProductId = 2, Quantity = 200 },
            new DishProduct { DishId = 8, ProductId = 20, Quantity = 100 },
            new DishProduct { DishId = 8, ProductId = 37, Quantity = 80 },
            new DishProduct { DishId = 8, ProductId = 23, Quantity = 1 },
            new DishProduct { DishId = 8, ProductId = 24, Quantity = 1 },
            new DishProduct { DishId = 9, ProductId = 7, Quantity = 350 },
            new DishProduct { DishId = 9, ProductId = 33, Quantity = 2 },
            new DishProduct { DishId = 9, ProductId = 32, Quantity = 200 },
            new DishProduct { DishId = 9, ProductId = 12, Quantity = 1 },
            new DishProduct { DishId = 9, ProductId = 35, Quantity = 100 },
            new DishProduct { DishId = 9, ProductId = 19, Quantity = 1 },
            new DishProduct { DishId = 9, ProductId = 38, Quantity = 1 },
            new DishProduct { DishId = 9, ProductId = 34, Quantity = 4 },
            new DishProduct { DishId = 10, ProductId = 15, Quantity = 400 },
            new DishProduct { DishId = 10, ProductId = 26, Quantity = 200 },
            new DishProduct { DishId = 10, ProductId = 11, Quantity = 2 },
            new DishProduct { DishId = 10, ProductId = 14, Quantity = 1 },
            new DishProduct { DishId = 10, ProductId = 13, Quantity = 2 },
            new DishProduct { DishId = 11, ProductId = 5, Quantity = 10 },
            new DishProduct { DishId = 11, ProductId = 17, Quantity = 1 },
            new DishProduct { DishId = 11, ProductId = 18, Quantity = 120 },
            new DishProduct { DishId = 11, ProductId = 19, Quantity = 0.5m },
            new DishProduct { DishId = 11, ProductId = 39, Quantity = 12 },
            new DishProduct { DishId = 11, ProductId = 13, Quantity = 2 },
            new DishProduct { DishId = 11, ProductId = 14, Quantity = 0.5m },
            new DishProduct { DishId = 12, ProductId = 16, Quantity = 180 },
            new DishProduct { DishId = 12, ProductId = 6, Quantity = 1 },
            new DishProduct { DishId = 12, ProductId = 12, Quantity = 1 },
            new DishProduct { DishId = 12, ProductId = 35, Quantity = 120 },
            new DishProduct { DishId = 12, ProductId = 40, Quantity = 200 },
            new DishProduct { DishId = 12, ProductId = 34, Quantity = 4 },
            new DishProduct { DishId = 12, ProductId = 38, Quantity = 1 }
        );

        modelBuilder.Entity<MealGroupDish>().HasData(
            new MealGroupDish { MealGroupId = 1, DishId = 1 },
            new MealGroupDish { MealGroupId = 1, DishId = 2 },
            new MealGroupDish { MealGroupId = 1, DishId = 3 },
            new MealGroupDish { MealGroupId = 1, DishId = 8 },
            new MealGroupDish { MealGroupId = 2, DishId = 4 },
            new MealGroupDish { MealGroupId = 2, DishId = 6 },
            new MealGroupDish { MealGroupId = 2, DishId = 7 },
            new MealGroupDish { MealGroupId = 2, DishId = 11 },
            new MealGroupDish { MealGroupId = 3, DishId = 4 },
            new MealGroupDish { MealGroupId = 3, DishId = 5 },
            new MealGroupDish { MealGroupId = 3, DishId = 10 },
            new MealGroupDish { MealGroupId = 3, DishId = 12 },
            new MealGroupDish { MealGroupId = 4, DishId = 3 },
            new MealGroupDish { MealGroupId = 4, DishId = 5 },
            new MealGroupDish { MealGroupId = 4, DishId = 7 },
            new MealGroupDish { MealGroupId = 4, DishId = 11 }
        );
    }
}
