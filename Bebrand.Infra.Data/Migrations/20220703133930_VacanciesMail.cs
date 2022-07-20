using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bebrand.Infra.Data.Migrations
{
    public partial class VacanciesMail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobsDescription",
                table: "Jobs");

            migrationBuilder.CreateTable(
                name: "VacanciesMails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueIds = table.Column<string>(type: "varchar(100)", nullable: true),
                    Subject = table.Column<string>(type: "varchar(100)", nullable: true),
                    TextBody = table.Column<string>(type: "varchar(100)", nullable: true),
                    Attachement = table.Column<string>(type: "varchar(100)", nullable: true),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(450)", maxLength: 450, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacanciesMails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacanciesMails_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacanciesMails_JobId",
                table: "VacanciesMails",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacanciesMails");

            migrationBuilder.AddColumn<string>(
                name: "JobsDescription",
                table: "Jobs",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true);
        }
    }
}
