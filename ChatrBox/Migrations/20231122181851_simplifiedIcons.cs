using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class simplifiedIcons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ChatrIcons_IconId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Communities_CommunityIcons_IconId",
                table: "Communities");

            migrationBuilder.DropTable(
                name: "ChatrIcons");

            migrationBuilder.DropTable(
                name: "CommunityIcons");

            migrationBuilder.DropIndex(
                name: "IX_Communities_IconId",
                table: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IconId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "24760299-123c-4ff7-b862-bc89d6a3eee4", "e9d5600a-9b17-477d-9a2b-0828e6c2d98e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4194ae1e-cb3e-4a28-be60-42b1c2a4d3d1", "e9d5600a-9b17-477d-9a2b-0828e6c2d98e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8fc8437b-d674-4ee4-acda-8531d4e139ef", "e9d5600a-9b17-477d-9a2b-0828e6c2d98e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a9e1865c-26c6-4372-bf83-b3ec1d8e190c", "e9d5600a-9b17-477d-9a2b-0828e6c2d98e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24760299-123c-4ff7-b862-bc89d6a3eee4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4194ae1e-cb3e-4a28-be60-42b1c2a4d3d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fc8437b-d674-4ee4-acda-8531d4e139ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9e1865c-26c6-4372-bf83-b3ec1d8e190c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9d5600a-9b17-477d-9a2b-0828e6c2d98e");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImageHash",
                table: "Communities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Communities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4de6d288-d971-4dfc-92a1-a59391447714", "0c1c4778-67c8-470f-86c2-074403ab1542", "moderator", "moderator" },
                    { "946c123c-2955-4409-9244-354bad79d15c", "be1a34de-e410-4c8e-9f72-7ea391413e07", "superAdmin", "superAdmin" },
                    { "e3ccb298-8f66-43ad-928c-76b4059c9ac1", "a5008d99-8ca5-4327-a6c8-98d4c80f94a8", "user", "user" },
                    { "f251fc8f-8f3e-4e29-8b21-5380bdeb0ba1", "b3e066fd-8320-4347-8a17-b9bd44b21b20", "admin", "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "ImageHash", "ImageUrl", "LastActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ec65fd25-9a4f-472c-bb38-ac5a313e48d0", 0, "a2089a4b-1300-44c2-be5c-cd21f26413bb", "Chatr", "example@example.com", true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "admin", "AQAAAAEAACcQAAAAEJTkeZmbZI4s0pB7LqIw3guMZqt0dnIU6kwzDIcBwX69HwXBK8ZGU+9zw+JMrVgBMA==", null, false, "89429d61-27b1-43a5-b4bf-fac9e050f1da", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4de6d288-d971-4dfc-92a1-a59391447714", "ec65fd25-9a4f-472c-bb38-ac5a313e48d0" },
                    { "946c123c-2955-4409-9244-354bad79d15c", "ec65fd25-9a4f-472c-bb38-ac5a313e48d0" },
                    { "e3ccb298-8f66-43ad-928c-76b4059c9ac1", "ec65fd25-9a4f-472c-bb38-ac5a313e48d0" },
                    { "f251fc8f-8f3e-4e29-8b21-5380bdeb0ba1", "ec65fd25-9a4f-472c-bb38-ac5a313e48d0" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4de6d288-d971-4dfc-92a1-a59391447714", "ec65fd25-9a4f-472c-bb38-ac5a313e48d0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "946c123c-2955-4409-9244-354bad79d15c", "ec65fd25-9a4f-472c-bb38-ac5a313e48d0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e3ccb298-8f66-43ad-928c-76b4059c9ac1", "ec65fd25-9a4f-472c-bb38-ac5a313e48d0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f251fc8f-8f3e-4e29-8b21-5380bdeb0ba1", "ec65fd25-9a4f-472c-bb38-ac5a313e48d0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4de6d288-d971-4dfc-92a1-a59391447714");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "946c123c-2955-4409-9244-354bad79d15c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3ccb298-8f66-43ad-928c-76b4059c9ac1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f251fc8f-8f3e-4e29-8b21-5380bdeb0ba1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ec65fd25-9a4f-472c-bb38-ac5a313e48d0");

            migrationBuilder.DropColumn(
                name: "ImageHash",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "ImageHash",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Communities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatrIcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatrId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatrIcons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunityIcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunityId = table.Column<int>(type: "int", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityIcons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24760299-123c-4ff7-b862-bc89d6a3eee4", "34e3b405-d3c1-4e82-bcd5-bd1c8c340810", "superAdmin", "superAdmin" },
                    { "4194ae1e-cb3e-4a28-be60-42b1c2a4d3d1", "d7aa8369-6d16-4a86-b977-a480d88a86de", "moderator", "moderator" },
                    { "8fc8437b-d674-4ee4-acda-8531d4e139ef", "4216853b-23e9-42b2-a7da-dc321ad6f2f3", "user", "user" },
                    { "a9e1865c-26c6-4372-bf83-b3ec1d8e190c", "008d0152-dc89-4be6-b181-94a2e5e0bc89", "admin", "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IconId", "LastActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e9d5600a-9b17-477d-9a2b-0828e6c2d98e", 0, "b0173780-39ed-437f-aaa0-95b2d0ca4b7b", "Chatr", "admin@example.com", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "admin", "AQAAAAEAACcQAAAAEI8zmFO1ROpwh+EVMoQiM1K881GbgJtlNKw1yNHE32245AACX1IJTTURwP3faswg8A==", null, false, "bbc7a07a-4e83-4324-979f-8eb337bd301b", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "24760299-123c-4ff7-b862-bc89d6a3eee4", "e9d5600a-9b17-477d-9a2b-0828e6c2d98e" },
                    { "4194ae1e-cb3e-4a28-be60-42b1c2a4d3d1", "e9d5600a-9b17-477d-9a2b-0828e6c2d98e" },
                    { "8fc8437b-d674-4ee4-acda-8531d4e139ef", "e9d5600a-9b17-477d-9a2b-0828e6c2d98e" },
                    { "a9e1865c-26c6-4372-bf83-b3ec1d8e190c", "e9d5600a-9b17-477d-9a2b-0828e6c2d98e" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Communities_IconId",
                table: "Communities",
                column: "IconId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IconId",
                table: "AspNetUsers",
                column: "IconId",
                unique: true,
                filter: "[IconId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ChatrIcons_IconId",
                table: "AspNetUsers",
                column: "IconId",
                principalTable: "ChatrIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_CommunityIcons_IconId",
                table: "Communities",
                column: "IconId",
                principalTable: "CommunityIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
