using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilePhones.Migrations
{
    public partial class AddImagePathToPhones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Phones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Phones");
        }
    }
}
