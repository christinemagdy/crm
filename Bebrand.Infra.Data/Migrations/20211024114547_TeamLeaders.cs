using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bebrand.Infra.Data.Migrations
{
    public partial class TeamLeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "SalesDirector");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesDirector",
                table: "SalesDirector",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TeamLeaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FName = table.Column<string>(type: "varchar(100)", nullable: true),
                    LName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalesDirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamLeaders_SalesDirector_SalesDirectorId",
                        column: x => x.SalesDirectorId,
                        principalTable: "SalesDirector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamLeaders_SalesDirectorId",
                table: "TeamLeaders",
                column: "SalesDirectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamLeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesDirector",
                table: "SalesDirector");

            migrationBuilder.RenameTable(
                name: "SalesDirector",
                newName: "Customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");
        }
    }
}
