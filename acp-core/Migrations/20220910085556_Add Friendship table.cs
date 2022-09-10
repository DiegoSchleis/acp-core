using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace acp_core.Migrations
{
    public partial class AddFriendshiptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friendship",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowedAthleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowingAthleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FriendShipStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendship",
                schema: "Identity");
        }
    }
}
