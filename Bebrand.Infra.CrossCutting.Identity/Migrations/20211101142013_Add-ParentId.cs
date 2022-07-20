using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bebrand.Infra.CrossCutting.Identity.Migrations
{
    public partial class AddParentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentUserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentUserId",
                table: "AspNetUsers");
        }
    }
}
