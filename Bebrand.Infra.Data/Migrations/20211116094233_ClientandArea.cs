using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bebrand.Infra.Data.Migrations
{
    public partial class ClientandArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_of_business = table.Column<string>(type: "varchar(100)", nullable: true),
                    Number = table.Column<string>(type: "varchar(100)", nullable: true),
                    Nameofcontact = table.Column<string>(type: "varchar(100)", nullable: true),
                    Position = table.Column<string>(type: "varchar(100)", nullable: true),
                    Completeaddress = table.Column<string>(type: "varchar(100)", nullable: true),
                    AriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Field = table.Column<string>(type: "varchar(100)", nullable: true),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Facebooklink = table.Column<string>(type: "varchar(100)", nullable: true),
                    Instagramlink = table.Column<string>(type: "varchar(100)", nullable: true),
                    Website = table.Column<string>(type: "varchar(100)", nullable: true),
                    Lastfeedback = table.Column<string>(type: "varchar(100)", nullable: true),
                    ServiceProvded = table.Column<string>(type: "varchar(100)", nullable: true),
                    Case = table.Column<string>(type: "varchar(100)", nullable: true),
                    AccountManager = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Areas_AriaId",
                        column: x => x.AriaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_AriaId",
                table: "Client",
                column: "AriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
