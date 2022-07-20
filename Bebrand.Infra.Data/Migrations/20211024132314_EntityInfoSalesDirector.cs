using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bebrand.Infra.Data.Migrations
{
    public partial class EntityInfoSalesDirector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "SalesDirector",
                type: "varchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "SalesDirector",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "SalesDirector");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "SalesDirector");
        }
    }
}
