using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealMate_backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Instructions = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PreparationMinutes = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AccentColor = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Description", "ImageUrl", "Instructions", "Name", "PreparationMinutes" },
                values: new object[,]
                {
                    { 1, "Воздушный омлет с сочными томатами и свежим шпинатом.", "https://static.mealmate.app/images/dishes/spinach-omelet.jpg", "Взбейте яйца с молоком, обжарьте шпинат с помидорами на оливковом масле, залейте яичной смесью и готовьте до готовности.", "Омлет со шпинатом", 15 },
                    { 2, "Хрустящий цельнозерновой тост с кремовым авокадо и яйцом пашот.", "https://static.mealmate.app/images/dishes/avocado-toast.jpg", "Поджарьте хлеб, разомните авокадо с лимонным соком, выложите на тост и добавьте яйцо пашот, посолите и поперчите по вкусу.", "Тост с авокадо и яйцом", 12 },
                    { 3, "Свежий и питательный завтрак с бананом, йогуртом и ягодами.", "https://static.mealmate.app/images/dishes/banana-smoothie-bowl.jpg", "Смешайте йогурт, банан, ягоды и овсянку в блендере, переложите в миску и украсьте мёдом и миндалём.", "Смузи-болл с бананом", 8 },
                    { 4, "Сочная курица в соусе терияки с ароматным жасминовым рисом.", "https://static.mealmate.app/images/dishes/teriyaki-chicken.jpg", "Обжарьте курицу с чесноком и имбирём, добавьте соевый соус, немного мёда и тушите до загустения, подавайте с отварным рисом и овощами.", "Курица терияки с рисом", 30 },
                    { 5, "Питательный салат с запечённым лососем, киноа и свежими овощами.", "https://static.mealmate.app/images/dishes/quinoa-salmon-salad.jpg", "Отварите киноа, запеките лосось, смешайте с овощами и заправьте лимонным соком и оливковым маслом.", "Салат с лососем и киноа", 25 },
                    { 6, "Классическая итальянская паста с насыщенным томатным соусом.", "https://static.mealmate.app/images/dishes/tomato-basil-pasta.jpg", "Отварите пасту, обжарьте чеснок, добавьте томаты и тушите до густоты, смешайте с пастой и свежим базиликом, посыпьте пармезаном.", "Паста с томатами и базиликом", 20 },
                    { 7, "Сытный кремовый суп с овощами и специями.", "https://static.mealmate.app/images/dishes/red-lentil-soup.jpg", "Обжарьте лук и морковь, добавьте чеснок, чечевицу, томаты и овощной бульон, варите до мягкости и пробейте блендером.", "Суп из красной чечевицы", 35 },
                    { 8, "Тёплая овсянка с йогуртом, ягодами и мёдом.", "https://static.mealmate.app/images/dishes/berry-oatmeal.jpg", "Сварите овсянку на молоке, переложите в миску, добавьте йогурт, ягоды и мёд, посыпьте миндалём.", "Овсянка с ягодами", 10 },
                    { 9, "Яркие тако с курицей в кокосово-карри соусе и свежими овощами.", "https://static.mealmate.app/images/dishes/curry-chicken-tacos.jpg", "Приготовьте курицу с пастой карри и кокосовым молоком, подавайте в тёплых лепёшках с овощами и лаймом.", "Тако с курицей карри", 28 },
                    { 10, "Нежный лосось, запечённый с брокколи и чесноком.", "https://static.mealmate.app/images/dishes/baked-salmon-broccoli.jpg", "Смешайте оливковое масло с чесноком и лимонным соком, полейте лосось и брокколи и запекайте до готовности.", "Запечённый лосось с брокколи", 22 },
                    { 11, "Классический салат с помидорами, огурцом, фетой и маслинами.", "https://static.mealmate.app/images/dishes/greek-salad.jpg", "Нарежьте овощи, добавьте фету и маслины, заправьте оливковым маслом и лимонным соком.", "Греческий салат", 15 },
                    { 12, "Полезные буррито с киноа, овощами и фасолью.", "https://static.mealmate.app/images/dishes/quinoa-veggie-burrito.jpg", "Отварите киноа, обжарьте овощи с фасолью и кукурузой, заверните начинку в лепёшки и подавайте с лаймом.", "Буррито с киноа и овощами", 30 }
                });

            migrationBuilder.InsertData(
                table: "MealGroups",
                columns: new[] { "Id", "AccentColor", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "#FFB74D", "Лёгкие и бодрящие варианты для завтрака.", "Утренние энергии" },
                    { 2, "#4FC3F7", "Сытные блюда, которые готовятся за полчаса.", "Быстрые обеды" },
                    { 3, "#9575CD", "Тёплые блюда для уютного вечера.", "Семейные ужины" },
                    { 4, "#81C784", "Баланс белков, жиров и углеводов для активного дня.", "Здоровый выбор" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Name", "Notes" },
                values: new object[,]
                {
                    { 1, "Молочные и яйца", "Яйца куриные", null },
                    { 2, "Молочные и яйца", "Молоко", null },
                    { 3, "Выпечка", "Хлеб цельнозерновой", null },
                    { 4, "Овощи и фрукты", "Авокадо", null },
                    { 5, "Овощи и фрукты", "Помидоры черри", null },
                    { 6, "Овощи и фрукты", "Шпинат свежий", null },
                    { 7, "Мясо и птица", "Куриное филе", null },
                    { 8, "Крупы", "Рис жасмин", null },
                    { 9, "Соусы и специи", "Соевый соус", null },
                    { 10, "Овощи и фрукты", "Имбирь свежий", null },
                    { 11, "Овощи и фрукты", "Чеснок", null },
                    { 12, "Овощи и фрукты", "Перец болгарский", null },
                    { 13, "Бакалея", "Оливковое масло", null },
                    { 14, "Овощи и фрукты", "Лимон", null },
                    { 15, "Рыба и морепродукты", "Лосось", null },
                    { 16, "Крупы", "Киноа", null },
                    { 17, "Овощи и фрукты", "Огурец", null },
                    { 18, "Сыры", "Сыр фета", null },
                    { 19, "Овощи и фрукты", "Лук красный", null },
                    { 20, "Молочные и яйца", "Йогурт греческий", null },
                    { 21, "Крупы", "Овсяные хлопья", null },
                    { 22, "Овощи и фрукты", "Банан", null },
                    { 23, "Бакалея", "Мёд", null },
                    { 24, "Орехи", "Миндаль", null },
                    { 25, "Овощи и фрукты", "Морковь", null },
                    { 26, "Овощи и фрукты", "Брокколи", null },
                    { 27, "Консервы", "Томаты консервированные", null },
                    { 28, "Бакалея", "Макароны пенне", null },
                    { 29, "Овощи и фрукты", "Базилик свежий", null },
                    { 30, "Сыры", "Сыр пармезан", null },
                    { 31, "Крупы", "Чечевица красная", null },
                    { 32, "Бакалея", "Кокосовое молоко", null },
                    { 33, "Соусы и специи", "Карри паста", null },
                    { 34, "Выпечка", "Лепёшки пшеничные", null },
                    { 35, "Овощи и фрукты", "Кукуруза замороженная", null },
                    { 36, "Бакалея", "Овощной бульон", null },
                    { 37, "Овощи и фрукты", "Ягоды замороженные", null },
                    { 38, "Овощи и фрукты", "Лайм", null },
                    { 39, "Консервы", "Маслины", null },
                    { 40, "Консервы", "Фасоль чёрная консервированная", null }
                });

            migrationBuilder.CreateTable(
                name: "MealGroupDishes",
                columns: table => new
                {
                    MealGroupId = table.Column<int>(type: "int", nullable: false),
                    DishId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealGroupDishes", x => new { x.MealGroupId, x.DishId });
                    table.ForeignKey(
                        name: "FK_MealGroupDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealGroupDishes_MealGroups_MealGroupId",
                        column: x => x.MealGroupId,
                        principalTable: "MealGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishProducts",
                columns: table => new
                {
                    DishId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishProducts", x => new { x.DishId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_DishProducts_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishProducts_ProductId",
                table: "DishProducts",
                column: "ProductId");

            migrationBuilder.InsertData(
                table: "DishProducts",
                columns: new[] { "DishId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "3 шт." },
                    { 1, 2, "60 мл" },
                    { 1, 5, "6 шт." },
                    { 1, 6, "2 горсти" },
                    { 1, 13, "1 ст. л." },
                    { 2, 1, "1 шт." },
                    { 2, 3, "2 ломтика" },
                    { 2, 4, "1 шт." },
                    { 2, 13, "1 ч. л." },
                    { 2, 14, "1 долька" },
                    { 3, 20, "150 г" },
                    { 3, 21, "30 г" },
                    { 3, 22, "1 шт." },
                    { 3, 23, "1 ч. л." },
                    { 3, 24, "1 ст. л." },
                    { 3, 37, "80 г" },
                    { 4, 7, "400 г" },
                    { 4, 8, "200 г" },
                    { 4, 9, "4 ст. л." },
                    { 4, 10, "2 см" },
                    { 4, 11, "2 зубчика" },
                    { 4, 12, "1 шт." },
                    { 4, 26, "150 г" },
                    { 5, 5, "8 шт." },
                    { 5, 6, "1 горсть" },
                    { 5, 13, "2 ст. л." },
                    { 5, 14, "1 шт." },
                    { 5, 15, "300 г" },
                    { 5, 16, "150 г" },
                    { 5, 17, "1 шт." },
                    { 6, 11, "2 зубчика" },
                    { 6, 13, "1 ст. л." },
                    { 6, 27, "400 г" },
                    { 6, 28, "250 г" },
                    { 6, 29, "1 горсть" },
                    { 6, 30, "30 г" },
                    { 7, 11, "2 зубчика" },
                    { 7, 19, "1 шт." },
                    { 7, 25, "2 шт." },
                    { 7, 27, "200 г" },
                    { 7, 31, "200 г" },
                    { 7, 36, "700 мл" },
                    { 8, 2, "200 мл" },
                    { 8, 20, "100 г" },
                    { 8, 21, "60 г" },
                    { 8, 23, "1 ст. л." },
                    { 8, 24, "1 ст. л." },
                    { 8, 37, "80 г" },
                    { 9, 7, "350 г" },
                    { 9, 12, "1 шт." },
                    { 9, 19, "1 шт." },
                    { 9, 32, "200 мл" },
                    { 9, 33, "2 ст. л." },
                    { 9, 34, "4 шт." },
                    { 9, 35, "100 г" },
                    { 9, 38, "1 шт." },
                    { 10, 11, "2 зубчика" },
                    { 10, 13, "2 ст. л." },
                    { 10, 14, "1 шт." },
                    { 10, 15, "400 г" },
                    { 10, 26, "200 г" },
                    { 11, 5, "10 шт." },
                    { 11, 13, "2 ст. л." },
                    { 11, 14, "1/2 шт." },
                    { 11, 17, "1 шт." },
                    { 11, 18, "120 г" },
                    { 11, 19, "1/2 шт." },
                    { 11, 39, "12 шт." },
                    { 12, 6, "1 горсть" },
                    { 12, 12, "1 шт." },
                    { 12, 16, "180 г" },
                    { 12, 34, "4 шт." },
                    { 12, 35, "120 г" },
                    { 12, 38, "1 шт." },
                    { 12, 40, "200 г" }
                });

            migrationBuilder.InsertData(
                table: "MealGroupDishes",
                columns: new[] { "MealGroupId", "DishId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 8 },
                    { 2, 4 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 11 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 10 },
                    { 3, 12 },
                    { 4, 3 },
                    { 4, 5 },
                    { 4, 7 },
                    { 4, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealGroupDishes_DishId",
                table: "MealGroupDishes",
                column: "DishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishProducts");

            migrationBuilder.DropTable(
                name: "MealGroupDishes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "MealGroups");
        }
    }
}
