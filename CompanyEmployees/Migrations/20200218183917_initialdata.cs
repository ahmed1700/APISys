using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyEmployees.Migrations
{
    public partial class initialdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[] { new Guid("7ab9dba8-32ba-4f79-90fd-fccb7465a0eb"), "Nasr City", "Egypt", "ITShare" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[] { new Guid("0bf027a2-9a38-41fd-b389-b73ab290d29b"), "AY Haga", "USA", "ABC" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[] { new Guid("290603c0-d0dd-4559-ba34-7579ca3da866"), 36, new Guid("7ab9dba8-32ba-4f79-90fd-fccb7465a0eb"), "Ahmed rabea", "Manager" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[] { new Guid("0bf027a2-9a38-41fd-b389-b73ab290d29b"), 36, new Guid("7ab9dba8-32ba-4f79-90fd-fccb7465a0eb"), "Mohamed rabea", "Manager" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("0bf027a2-9a38-41fd-b389-b73ab290d29b"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("0bf027a2-9a38-41fd-b389-b73ab290d29b"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("290603c0-d0dd-4559-ba34-7579ca3da866"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("7ab9dba8-32ba-4f79-90fd-fccb7465a0eb"));

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
