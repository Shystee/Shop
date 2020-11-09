using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.DataAccess.Migrations
{
    public partial class AddedProductNameRequiredAndRatingComment : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Comment", "Ratings");

            migrationBuilder.AlterColumn<decimal>("Price",
                "Products",
                "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<string>("Name",
                "Products",
                "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>("Comment", "Ratings", nullable: false, defaultValue: "");

            migrationBuilder.AlterColumn<decimal>("Price",
                "Products",
                "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>("Name",
                "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}