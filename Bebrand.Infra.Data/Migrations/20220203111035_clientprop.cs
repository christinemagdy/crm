using Microsoft.EntityFrameworkCore.Migrations;

namespace Bebrand.Infra.Data.Migrations
{
    public partial class clientprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Call",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phoneone",
                table: "Client",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phonetwo",
                table: "Client",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Typclient",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Call",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Phoneone",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Phonetwo",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Typclient",
                table: "Client");
        }
    }
}
