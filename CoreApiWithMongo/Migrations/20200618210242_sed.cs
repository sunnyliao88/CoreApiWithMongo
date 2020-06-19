using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApiWithMongo.Migrations
{
    public partial class sed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Department", "DepartmentId", "Email", "Name" },
                values: new object[] { 2, 1, 2, "n2@e.com", "n2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Department", "DepartmentId", "Email", "Name" },
                values: new object[] { 1, 0, 1, "n1@e.com", "n1" });
        }
    }
}
