using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class fixed_display_order_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "displayOrder",
                table: "Topics",
                newName: "DisplayOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "Topics",
                newName: "displayOrder");
        }
    }
}
