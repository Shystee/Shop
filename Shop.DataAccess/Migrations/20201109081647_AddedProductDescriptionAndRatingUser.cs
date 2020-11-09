using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.DataAccess.Migrations
{
    public partial class AddedProductDescriptionAndRatingUser : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Ratings_AspNetUsers_UserId", "Ratings");

            migrationBuilder.DropIndex("IX_Ratings_UserId", "Ratings");

            migrationBuilder.DropColumn("UserId", "Ratings");

            migrationBuilder.DropColumn("Description", "Products");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>("UserId", "Ratings", nullable: false, defaultValue: "");

            migrationBuilder.AddColumn<string>("Description", "Products", nullable: false, defaultValue: "");

            migrationBuilder.CreateIndex("IX_Ratings_UserId", "Ratings", "UserId");

            migrationBuilder.AddForeignKey("FK_Ratings_AspNetUsers_UserId",
                "Ratings",
                "UserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}