using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilePhones.Migrations
{
    public partial class ChangeFileModelFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "FilesOnFileSystem");

            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "FilesOnDatabase");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FilesOnFileSystem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FilesOnDatabase",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilesOnFileSystem_UserId",
                table: "FilesOnFileSystem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesOnDatabase_UserId",
                table: "FilesOnDatabase",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilesOnDatabase_AspNetUsers_UserId",
                table: "FilesOnDatabase",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FilesOnFileSystem_AspNetUsers_UserId",
                table: "FilesOnFileSystem",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilesOnDatabase_AspNetUsers_UserId",
                table: "FilesOnDatabase");

            migrationBuilder.DropForeignKey(
                name: "FK_FilesOnFileSystem_AspNetUsers_UserId",
                table: "FilesOnFileSystem");

            migrationBuilder.DropIndex(
                name: "IX_FilesOnFileSystem_UserId",
                table: "FilesOnFileSystem");

            migrationBuilder.DropIndex(
                name: "IX_FilesOnDatabase_UserId",
                table: "FilesOnDatabase");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FilesOnFileSystem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FilesOnDatabase");

            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "FilesOnFileSystem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "FilesOnDatabase",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
