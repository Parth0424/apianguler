using Microsoft.EntityFrameworkCore.Migrations;

namespace Manage.Migrations
{
    public partial class task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Employees",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "FullName");
        }
    }
}
