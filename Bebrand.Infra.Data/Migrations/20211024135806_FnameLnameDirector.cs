using Microsoft.EntityFrameworkCore.Migrations;

namespace Bebrand.Infra.Data.Migrations
{
    public partial class FnameLnameDirector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SalesDirector",
                newName: "LName");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "SalesDirector",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "SalesDirector");

            migrationBuilder.RenameColumn(
                name: "LName",
                table: "SalesDirector",
                newName: "Name");
        }
    }
}
