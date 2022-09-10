using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace acp_core.Migrations
{
    public partial class PrivacyOptionstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "privacyoptions_athlete",
                schema: "Identity",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PrivacyOptions",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicActivity = table.Column<bool>(type: "bit", nullable: false),
                    PrivateProfile = table.Column<bool>(type: "bit", nullable: false),
                    PendingFriendrequests = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivacyOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_privacyoptions_athlete",
                schema: "Identity",
                table: "User",
                column: "privacyoptions_athlete");

            migrationBuilder.AddForeignKey(
                name: "FK_User_PrivacyOptions_privacyoptions_athlete",
                schema: "Identity",
                table: "User",
                column: "privacyoptions_athlete",
                principalSchema: "Identity",
                principalTable: "PrivacyOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_PrivacyOptions_privacyoptions_athlete",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropTable(
                name: "PrivacyOptions",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_User_privacyoptions_athlete",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "privacyoptions_athlete",
                schema: "Identity",
                table: "User");
        }
    }
}
