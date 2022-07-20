using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bebrand.Infra.Data.Migrations
{
    public partial class EntityInfoTeamLeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TeamLeaders",
                type: "varchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TeamLeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TeamLeaders");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TeamLeaders");
        }
    }
}
