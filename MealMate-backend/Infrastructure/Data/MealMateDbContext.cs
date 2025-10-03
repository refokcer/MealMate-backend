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

        var products = new[]
        {
            new Product { Id = 1, Name = "Яйца куриные", Category = "Молочные и яйца" },
            new Product { Id = 2, Name = "Молоко", Category = "Молочные и яйца" },
            new Product { Id = 3, Name = "Хлеб цельнозерновой", Category = "Выпечка" },
            new Product { Id = 4, Name = "Авокадо", Category = "Овощи и фрукты" },
            new Product { Id = 5, Name = "Помидоры черри", Category = "Овощи и фрукты" },
            new Product { Id = 6, Name = "Шпинат свежий", Category = "Овощи и фрукты" },
            new Product { Id = 7, Name = "Куриное филе", Category = "Мясо и птица" },
            new Product { Id = 8, Name = "Рис жасмин", Category = "Крупы" },
            new Product { Id = 9, Name = "Соевый соус", Category = "Соусы и специи" },
            new Product { Id = 10, Name = "Имбирь свежий", Category = "Овощи и фрукты" },
            new Product { Id = 11, Name = "Чеснок", Category = "Овощи и фрукты" },
            new Product { Id = 12, Name = "Перец болгарский", Category = "Овощи и фрукты" },
            new Product { Id = 13, Name = "Оливковое масло", Category = "Бакалея" },
            new Product { Id = 14, Name = "Лимон", Category = "Овощи и фрукты" },
            new Product { Id = 15, Name = "Лосось", Category = "Рыба и морепродукты" },
            new Product { Id = 16, Name = "Киноа", Category = "Крупы" },
            new Product { Id = 17, Name = "Огурец", Category = "Овощи и фрукты" },
            new Product { Id = 18, Name = "Сыр фета", Category = "Сыры" },
            new Product { Id = 19, Name = "Лук красный", Category = "Овощи и фрукты" },
            new Product { Id = 20, Name = "Йогурт греческий", Category = "Молочные и яйца" },
            new Product { Id = 21, Name = "Овсяные хлопья", Category = "Крупы" },
            new Product { Id = 22, Name = "Банан", Category = "Овощи и фрукты" },
            new Product { Id = 23, Name = "Мёд", Category = "Бакалея" },
            new Product { Id = 24, Name = "Миндаль", Category = "Орехи" },
            new Product { Id = 25, Name = "Морковь", Category = "Овощи и фрукты" },
            new Product { Id = 26, Name = "Брокколи", Category = "Овощи и фрукты" },
            new Product { Id = 27, Name = "Томаты консервированные", Category = "Консервы" },
            new Product { Id = 28, Name = "Макароны пенне", Category = "Бакалея" },
            new Product { Id = 29, Name = "Базилик свежий", Category = "Овощи и фрукты" },
            new Product { Id = 30, Name = "Сыр пармезан", Category = "Сыры" },
            new Product { Id = 31, Name = "Чечевица красная", Category = "Крупы" },
            new Product { Id = 32, Name = "Кокосовое молоко", Category = "Бакалея" },
            new Product { Id = 33, Name = "Карри паста", Category = "Соусы и специи" },
            new Product { Id = 34, Name = "Лепёшки пшеничные", Category = "Выпечка" },
            new Product { Id = 35, Name = "Кукуруза замороженная", Category = "Овощи и фрукты" },
            new Product { Id = 36, Name = "Овощной бульон", Category = "Бакалея" },
            new Product { Id = 37, Name = "Ягоды замороженные", Category = "Овощи и фрукты" },
            new Product { Id = 38, Name = "Лайм", Category = "Овощи и фрукты" },
            new Product { Id = 39, Name = "Маслины", Category = "Консервы" },
            new Product { Id = 40, Name = "Фасоль чёрная консервированная", Category = "Консервы" }
        };

        var dishes = new[]
        {
            new Dish
            {
                Id = 1,
                Name = "Омлет со шпинатом",
                Description = "Воздушный омлет с сочными томатами и свежим шпинатом.",
                Instructions = "Взбейте яйца с молоком, обжарьте шпинат с помидорами на оливковом масле, залейте яичной смесью и готовьте до готовности.",
                PreparationMinutes = 15,
                ImageUrl = "https://static.mealmate.app/images/dishes/spinach-omelet.jpg"
            },
            new Dish
            {
                Id = 2,
                Name = "Тост с авокадо и яйцом",
                Description = "Хрустящий цельнозерновой тост с кремовым авокадо и яйцом пашот.",
                Instructions = "Поджарьте хлеб, разомните авокадо с лимонным соком, выложите на тост и добавьте яйцо пашот, посолите и поперчите по вкусу.",
                PreparationMinutes = 12,
                ImageUrl = "https://static.mealmate.app/images/dishes/avocado-toast.jpg"
            },
            new Dish
            {
                Id = 3,
                Name = "Смузи-болл с бананом",
                Description = "Свежий и питательный завтрак с бананом, йогуртом и ягодами.",
                Instructions = "Смешайте йогурт, банан, ягоды и овсянку в блендере, переложите в миску и украсьте мёдом и миндалём.",
                PreparationMinutes = 8,
                ImageUrl = "https://static.mealmate.app/images/dishes/banana-smoothie-bowl.jpg"
            },
            new Dish
            {
                Id = 4,
                Name = "Курица терияки с рисом",
                Description = "Сочная курица в соусе терияки с ароматным жасминовым рисом.",
                Instructions = "Обжарьте курицу с чесноком и имбирём, добавьте соевый соус, немного мёда и тушите до загустения, подавайте с отварным рисом и овощами.",
                PreparationMinutes = 30,
                ImageUrl = "https://static.mealmate.app/images/dishes/teriyaki-chicken.jpg"
            },
            new Dish
            {
                Id = 5,
                Name = "Салат с лососем и киноа",
                Description = "Питательный салат с запечённым лососем, киноа и свежими овощами.",
                Instructions = "Отварите киноа, запеките лосось, смешайте с овощами и заправьте лимонным соком и оливковым маслом.",
                PreparationMinutes = 25,
                ImageUrl = "https://static.mealmate.app/images/dishes/quinoa-salmon-salad.jpg"
            },
            new Dish
            {
                Id = 6,
                Name = "Паста с томатами и базиликом",
                Description = "Классическая итальянская паста с насыщенным томатным соусом.",
                Instructions = "Отварите пасту, обжарьте чеснок, добавьте томаты и тушите до густоты, смешайте с пастой и свежим базиликом, посыпьте пармезаном.",
                PreparationMinutes = 20,
                ImageUrl = "https://static.mealmate.app/images/dishes/tomato-basil-pasta.jpg"
            },
            new Dish
            {
                Id = 7,
                Name = "Суп из красной чечевицы",
                Description = "Сытный кремовый суп с овощами и специями.",
                Instructions = "Обжарьте лук и морковь, добавьте чеснок, чечевицу, томаты и овощной бульон, варите до мягкости и пробейте блендером.",
                PreparationMinutes = 35,
                ImageUrl = "https://static.mealmate.app/images/dishes/red-lentil-soup.jpg"
            },
            new Dish
            {
                Id = 8,
                Name = "Овсянка с ягодами",
                Description = "Тёплая овсянка с йогуртом, ягодами и мёдом.",
                Instructions = "Сварите овсянку на молоке, переложите в миску, добавьте йогурт, ягоды и мёд, посыпьте миндалём.",
                PreparationMinutes = 10,
                ImageUrl = "https://static.mealmate.app/images/dishes/berry-oatmeal.jpg"
            },
            new Dish
            {
                Id = 9,
                Name = "Тако с курицей карри",
                Description = "Яркие тако с курицей в кокосово-карри соусе и свежими овощами.",
                Instructions = "Приготовьте курицу с пастой карри и кокосовым молоком, подавайте в тёплых лепёшках с овощами и лаймом.",
                PreparationMinutes = 28,
                ImageUrl = "https://static.mealmate.app/images/dishes/curry-chicken-tacos.jpg"
            },
            new Dish
            {
                Id = 10,
                Name = "Запечённый лосось с брокколи",
                Description = "Нежный лосось, запечённый с брокколи и чесноком.",
                Instructions = "Смешайте оливковое масло с чесноком и лимонным соком, полейте лосось и брокколи и запекайте до готовности.",
                PreparationMinutes = 22,
                ImageUrl = "https://static.mealmate.app/images/dishes/baked-salmon-broccoli.jpg"
            },
            new Dish
            {
                Id = 11,
                Name = "Греческий салат",
                Description = "Классический салат с помидорами, огурцом, фетой и маслинами.",
                Instructions = "Нарежьте овощи, добавьте фету и маслины, заправьте оливковым маслом и лимонным соком.",
                PreparationMinutes = 15,
                ImageUrl = "https://static.mealmate.app/images/dishes/greek-salad.jpg"
            },
            new Dish
            {
                Id = 12,
                Name = "Буррито с киноа и овощами",
                Description = "Полезные буррито с киноа, овощами и фасолью.",
                Instructions = "Отварите киноа, обжарьте овощи с фасолью и кукурузой, заверните начинку в лепёшки и подавайте с лаймом.",
                PreparationMinutes = 30,
                ImageUrl = "https://static.mealmate.app/images/dishes/quinoa-veggie-burrito.jpg"
            }
        };

        var mealGroups = new[]
        {
            new MealGroup
            {
                Id = 1,
                Name = "Утренние энергии",
                Description = "Лёгкие и бодрящие варианты для завтрака.",
                AccentColor = "#FFB74D"
            },
            new MealGroup
            {
                Id = 2,
                Name = "Быстрые обеды",
                Description = "Сытные блюда, которые готовятся за полчаса.",
                AccentColor = "#4FC3F7"
            },
            new MealGroup
            {
                Id = 3,
                Name = "Семейные ужины",
                Description = "Тёплые блюда для уютного вечера.",
                AccentColor = "#9575CD"
            },
            new MealGroup
            {
                Id = 4,
                Name = "Здоровый выбор",
                Description = "Баланс белков, жиров и углеводов для активного дня.",
                AccentColor = "#81C784"
            }
        };

        modelBuilder.Entity<Product>().HasData(products);
        modelBuilder.Entity<Dish>().HasData(dishes);
        modelBuilder.Entity<MealGroup>().HasData(mealGroups);

        modelBuilder.Entity<DishProduct>().HasData(
            new DishProduct { DishId = 1, ProductId = 1, Quantity = "3 шт." },
            new DishProduct { DishId = 1, ProductId = 2, Quantity = "60 мл" },
            new DishProduct { DishId = 1, ProductId = 5, Quantity = "6 шт." },
            new DishProduct { DishId = 1, ProductId = 6, Quantity = "2 горсти" },
            new DishProduct { DishId = 1, ProductId = 13, Quantity = "1 ст. л." },
            new DishProduct { DishId = 2, ProductId = 3, Quantity = "2 ломтика" },
            new DishProduct { DishId = 2, ProductId = 4, Quantity = "1 шт." },
            new DishProduct { DishId = 2, ProductId = 1, Quantity = "1 шт." },
            new DishProduct { DishId = 2, ProductId = 14, Quantity = "1 долька" },
            new DishProduct { DishId = 2, ProductId = 13, Quantity = "1 ч. л." },
            new DishProduct { DishId = 3, ProductId = 20, Quantity = "150 г" },
            new DishProduct { DishId = 3, ProductId = 22, Quantity = "1 шт." },
            new DishProduct { DishId = 3, ProductId = 21, Quantity = "30 г" },
            new DishProduct { DishId = 3, ProductId = 37, Quantity = "80 г" },
            new DishProduct { DishId = 3, ProductId = 23, Quantity = "1 ч. л." },
            new DishProduct { DishId = 3, ProductId = 24, Quantity = "1 ст. л." },
            new DishProduct { DishId = 4, ProductId = 7, Quantity = "400 г" },
            new DishProduct { DishId = 4, ProductId = 11, Quantity = "2 зубчика" },
            new DishProduct { DishId = 4, ProductId = 10, Quantity = "2 см" },
            new DishProduct { DishId = 4, ProductId = 9, Quantity = "4 ст. л." },
            new DishProduct { DishId = 4, ProductId = 8, Quantity = "200 г" },
            new DishProduct { DishId = 4, ProductId = 12, Quantity = "1 шт." },
            new DishProduct { DishId = 4, ProductId = 26, Quantity = "150 г" },
            new DishProduct { DishId = 5, ProductId = 15, Quantity = "300 г" },
            new DishProduct { DishId = 5, ProductId = 16, Quantity = "150 г" },
            new DishProduct { DishId = 5, ProductId = 6, Quantity = "1 горсть" },
            new DishProduct { DishId = 5, ProductId = 17, Quantity = "1 шт." },
            new DishProduct { DishId = 5, ProductId = 5, Quantity = "8 шт." },
            new DishProduct { DishId = 5, ProductId = 14, Quantity = "1 шт." },
            new DishProduct { DishId = 5, ProductId = 13, Quantity = "2 ст. л." },
            new DishProduct { DishId = 6, ProductId = 28, Quantity = "250 г" },
            new DishProduct { DishId = 6, ProductId = 27, Quantity = "400 г" },
            new DishProduct { DishId = 6, ProductId = 11, Quantity = "2 зубчика" },
            new DishProduct { DishId = 6, ProductId = 29, Quantity = "1 горсть" },
            new DishProduct { DishId = 6, ProductId = 30, Quantity = "30 г" },
            new DishProduct { DishId = 6, ProductId = 13, Quantity = "1 ст. л." },
            new DishProduct { DishId = 7, ProductId = 31, Quantity = "200 г" },
            new DishProduct { DishId = 7, ProductId = 19, Quantity = "1 шт." },
            new DishProduct { DishId = 7, ProductId = 25, Quantity = "2 шт." },
            new DishProduct { DishId = 7, ProductId = 11, Quantity = "2 зубчика" },
            new DishProduct { DishId = 7, ProductId = 27, Quantity = "200 г" },
            new DishProduct { DishId = 7, ProductId = 36, Quantity = "700 мл" },
            new DishProduct { DishId = 8, ProductId = 21, Quantity = "60 г" },
            new DishProduct { DishId = 8, ProductId = 2, Quantity = "200 мл" },
            new DishProduct { DishId = 8, ProductId = 20, Quantity = "100 г" },
            new DishProduct { DishId = 8, ProductId = 37, Quantity = "80 г" },
            new DishProduct { DishId = 8, ProductId = 23, Quantity = "1 ст. л." },
            new DishProduct { DishId = 8, ProductId = 24, Quantity = "1 ст. л." },
            new DishProduct { DishId = 9, ProductId = 7, Quantity = "350 г" },
            new DishProduct { DishId = 9, ProductId = 33, Quantity = "2 ст. л." },
            new DishProduct { DishId = 9, ProductId = 32, Quantity = "200 мл" },
            new DishProduct { DishId = 9, ProductId = 12, Quantity = "1 шт." },
            new DishProduct { DishId = 9, ProductId = 35, Quantity = "100 г" },
            new DishProduct { DishId = 9, ProductId = 19, Quantity = "1 шт." },
            new DishProduct { DishId = 9, ProductId = 38, Quantity = "1 шт." },
            new DishProduct { DishId = 9, ProductId = 34, Quantity = "4 шт." },
            new DishProduct { DishId = 10, ProductId = 15, Quantity = "400 г" },
            new DishProduct { DishId = 10, ProductId = 26, Quantity = "200 г" },
            new DishProduct { DishId = 10, ProductId = 11, Quantity = "2 зубчика" },
            new DishProduct { DishId = 10, ProductId = 14, Quantity = "1 шт." },
            new DishProduct { DishId = 10, ProductId = 13, Quantity = "2 ст. л." },
            new DishProduct { DishId = 11, ProductId = 5, Quantity = "10 шт." },
            new DishProduct { DishId = 11, ProductId = 17, Quantity = "1 шт." },
            new DishProduct { DishId = 11, ProductId = 18, Quantity = "120 г" },
            new DishProduct { DishId = 11, ProductId = 19, Quantity = "1/2 шт." },
            new DishProduct { DishId = 11, ProductId = 39, Quantity = "12 шт." },
            new DishProduct { DishId = 11, ProductId = 13, Quantity = "2 ст. л." },
            new DishProduct { DishId = 11, ProductId = 14, Quantity = "1/2 шт." },
            new DishProduct { DishId = 12, ProductId = 16, Quantity = "180 г" },
            new DishProduct { DishId = 12, ProductId = 6, Quantity = "1 горсть" },
            new DishProduct { DishId = 12, ProductId = 12, Quantity = "1 шт." },
            new DishProduct { DishId = 12, ProductId = 35, Quantity = "120 г" },
            new DishProduct { DishId = 12, ProductId = 40, Quantity = "200 г" },
            new DishProduct { DishId = 12, ProductId = 34, Quantity = "4 шт." },
            new DishProduct { DishId = 12, ProductId = 38, Quantity = "1 шт." }
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
