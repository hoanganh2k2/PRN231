using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ODataASPNetCoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gadgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gadgets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Gadgets",
                columns: new[] { "Id", "Brand", "Cost", "ProductName", "Type" },
                values: new object[,]
                {
                    { 1, "Nhat", 10m, "Product1", "Fruit" },
                    { 2, "Trung", 20m, "Product2", "Sea food" },
                    { 3, "Han", 30m, "Product3", "Food" },
                    { 4, "Nhat", 15m, "Product4", "Sea food" },
                    { 5, "Nhat", 17m, "Product5", "Fruit" },
                    { 6, "Trung", 40m, "Product6", "Food" },
                    { 7, "Han", 12m, "Product7", "Food" },
                    { 8, "Nhat", 18m, "Product8", "Sea food" },
                    { 9, "Trung", 50m, "Product9", "Fruit" },
                    { 10, "Viet Nam", 90m, "Product10", "Sea food" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gadgets");
        }
    }
}
