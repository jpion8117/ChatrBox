using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class addedCommunityModerators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsModerator",
                table: "CommunityUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsModerator",
                table: "CommunityUsers");
        }
    }
}
