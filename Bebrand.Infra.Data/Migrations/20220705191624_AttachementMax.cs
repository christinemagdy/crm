using Microsoft.EntityFrameworkCore.Migrations;

namespace Bebrand.Infra.Data.Migrations
{
    public partial class AttachementMax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Attachement",
                table: "VacanciesMails",
                type: "varchar(8000)",
                maxLength: 8000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Attachement",
                table: "VacanciesMails",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8000)",
                oldMaxLength: 8000,
                oldNullable: true);
        }
    }
}
