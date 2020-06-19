using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApiWithMongo.Migrations
{
    public partial class filvm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "Employees",
                newName: "FileContent");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "FileContent",
                table: "Employees",
                newName: "File");
        }
    }
}
