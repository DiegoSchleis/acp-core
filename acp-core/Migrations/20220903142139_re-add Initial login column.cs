using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace acp_core.Migrations
{
    public partial class readdInitiallogincolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InitialLogin",
                schema: "Identity",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialLogin",
                schema: "Identity",
                table: "User");
        }
    }
}
