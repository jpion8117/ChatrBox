using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class addedContentfilterToCommunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ContentFilter",
                table: "Communities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04e4c9f4-8143-45a5-82f4-4950ec67e755", "6d1698c9-7693-4e02-9a7d-8bf59b1b1071", "superAdmin", "superAdmin" },
                    { "3a0b507a-2cd9-46c9-b221-54df540bf64c", "404895a7-0d93-47aa-be44-4266f1d71e62", "admin", "admin" },
                    { "79dc14de-1a17-4f98-8bbd-ff6ae459660f", "1719209d-b8b0-477a-9ae5-0b788d2f4681", "moderator", "moderator" },
                    { "d384dfa3-ea4e-4692-9f17-6feb9751dce2", "3c500698-e600-4c95-be45-5733853ab58f", "user", "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "ImageHash", "ImageUrl", "LastActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9a69966a-b7a8-49a0-8d7c-8f120211f2a0", 0, "8712f301-8fd1-4afb-8b58-ec509ec464f9", "Chatr", "example@example.com", true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "admin", "AQAAAAEAACcQAAAAEMtqvhGXOCSScPQG2zDX5JR4hwTzC32as1LODhkHV2zbzU4Aq28cULD/LWvXyb5Dqg==", null, false, "88e9f0a0-9cbd-42f0-882b-4c5b849a6ce7", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "04e4c9f4-8143-45a5-82f4-4950ec67e755", "9a69966a-b7a8-49a0-8d7c-8f120211f2a0" },
                    { "3a0b507a-2cd9-46c9-b221-54df540bf64c", "9a69966a-b7a8-49a0-8d7c-8f120211f2a0" },
                    { "79dc14de-1a17-4f98-8bbd-ff6ae459660f", "9a69966a-b7a8-49a0-8d7c-8f120211f2a0" },
                    { "d384dfa3-ea4e-4692-9f17-6feb9751dce2", "9a69966a-b7a8-49a0-8d7c-8f120211f2a0" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "04e4c9f4-8143-45a5-82f4-4950ec67e755", "9a69966a-b7a8-49a0-8d7c-8f120211f2a0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3a0b507a-2cd9-46c9-b221-54df540bf64c", "9a69966a-b7a8-49a0-8d7c-8f120211f2a0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "79dc14de-1a17-4f98-8bbd-ff6ae459660f", "9a69966a-b7a8-49a0-8d7c-8f120211f2a0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d384dfa3-ea4e-4692-9f17-6feb9751dce2", "9a69966a-b7a8-49a0-8d7c-8f120211f2a0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04e4c9f4-8143-45a5-82f4-4950ec67e755");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a0b507a-2cd9-46c9-b221-54df540bf64c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79dc14de-1a17-4f98-8bbd-ff6ae459660f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d384dfa3-ea4e-4692-9f17-6feb9751dce2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a69966a-b7a8-49a0-8d7c-8f120211f2a0");

            migrationBuilder.DropColumn(
                name: "ContentFilter",
                table: "Communities");

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
    }
}
