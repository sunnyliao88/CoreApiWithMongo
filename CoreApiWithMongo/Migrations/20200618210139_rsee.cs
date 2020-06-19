using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApiWithMongo.Migrations
{
    public partial class rsee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 201);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Department", "DepartmentId", "Email", "Name" },
                values: new object[] { 1, 0, 1, "n1@e.com", "n1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Department", "DepartmentId", "Email", "Name" },
                values: new object[] { 100, 0, 1, "n1@e.com", "n1" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Department", "DepartmentId", "Email", "Name" },
                values: new object[] { 201, 0, 1, "n1@e.com", "n1" });
        }
    }
}
