using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealMate_backend.Migrations
{
    /// <inheritdoc />
    public partial class ProductUnitsAndNumericQuantities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "шт.");

            migrationBuilder.Sql("""
                UPDATE Products
                SET Unit = CASE Id
                    WHEN 1 THEN N'шт.'
                    WHEN 2 THEN N'мл'
                    WHEN 3 THEN N'ломтик'
                    WHEN 4 THEN N'шт.'
                    WHEN 5 THEN N'шт.'
                    WHEN 6 THEN N'горсть'
                    WHEN 7 THEN N'г'
                    WHEN 8 THEN N'г'
                    WHEN 9 THEN N'ст. л.'
                    WHEN 10 THEN N'см'
                    WHEN 11 THEN N'зубчик'
                    WHEN 12 THEN N'шт.'
                    WHEN 13 THEN N'ст. л.'
                    WHEN 14 THEN N'шт.'
                    WHEN 15 THEN N'г'
                    WHEN 16 THEN N'г'
                    WHEN 17 THEN N'шт.'
                    WHEN 18 THEN N'г'
                    WHEN 19 THEN N'шт.'
                    WHEN 20 THEN N'г'
                    WHEN 21 THEN N'г'
                    WHEN 22 THEN N'шт.'
                    WHEN 23 THEN N'ст. л.'
                    WHEN 24 THEN N'ст. л.'
                    WHEN 25 THEN N'шт.'
                    WHEN 26 THEN N'г'
                    WHEN 27 THEN N'г'
                    WHEN 28 THEN N'г'
                    WHEN 29 THEN N'горсть'
                    WHEN 30 THEN N'г'
                    WHEN 31 THEN N'г'
                    WHEN 32 THEN N'мл'
                    WHEN 33 THEN N'ст. л.'
                    WHEN 34 THEN N'шт.'
                    WHEN 35 THEN N'г'
                    WHEN 36 THEN N'мл'
                    WHEN 37 THEN N'г'
                    WHEN 38 THEN N'шт.'
                    WHEN 39 THEN N'шт.'
                    WHEN 40 THEN N'г'
                    ELSE Unit
                END
                """);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityNumeric",
                table: "DishProducts",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE DishProducts
                SET QuantityNumeric = CASE
                    WHEN Quantity IS NULL OR LTRIM(RTRIM(Quantity)) = '' THEN NULL
                    WHEN CHARINDEX('/', Quantity) > 0 THEN
                        TRY_CONVERT(decimal(10,2), LEFT(LTRIM(RTRIM(Quantity)), CHARINDEX('/', LTRIM(RTRIM(Quantity))) - 1)) /
                        NULLIF(TRY_CONVERT(decimal(10,2), SUBSTRING(
                            LTRIM(RTRIM(Quantity)),
                            CHARINDEX('/', LTRIM(RTRIM(Quantity))) + 1,
                            CHARINDEX(' ', LTRIM(RTRIM(Quantity)) + ' ', CHARINDEX('/', LTRIM(RTRIM(Quantity)))) - CHARINDEX('/', LTRIM(RTRIM(Quantity))) - 1
                        )), 0)
                    ELSE TRY_CONVERT(decimal(10,2), REPLACE(LEFT(LTRIM(RTRIM(Quantity)), CHARINDEX(' ', LTRIM(RTRIM(Quantity)) + ' ') - 1), ',', '.'))
                END
                """);

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DishProducts");

            migrationBuilder.RenameColumn(
                name: "QuantityNumeric",
                table: "DishProducts",
                newName: "Quantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuantityText",
                table: "DishProducts",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE DishProducts
                SET QuantityText = CASE
                    WHEN Quantity IS NULL THEN NULL
                    WHEN Quantity = FLOOR(Quantity) THEN CONVERT(nvarchar(80), CONVERT(decimal(18,0), Quantity))
                    ELSE CONVERT(nvarchar(80), Quantity)
                END
                """);

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DishProducts");

            migrationBuilder.RenameColumn(
                name: "QuantityText",
                table: "DishProducts",
                newName: "Quantity");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Products");
        }    }
}
