using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace acp_core.Migrations
{
    public partial class addavatarproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                schema: "Identity",
                table: "User",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                schema: "Identity",
                table: "User");
        }
    }
}
