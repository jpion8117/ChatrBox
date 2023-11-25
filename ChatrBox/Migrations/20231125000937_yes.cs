using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class yes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1bf54f1a-0222-4ea9-9f36-ba297806f56f", "0c6a57fc-2dbe-42d9-89cd-099ceb02ac4e", "user", "user" },
                    { "722dcfd3-f197-4a97-b266-a8c8d460b4dd", "bf6a7f0d-4f83-4f94-b722-605e1d188b92", "superAdmin", "superAdmin" },
                    { "75217ef1-f65f-4816-9667-5b791ad3618e", "3b06e8e7-6fed-4ff2-b898-709ddae4c212", "admin", "admin" },
                    { "ad84670d-6f01-4d1d-aabe-7e79745504d4", "881d9242-a07c-4915-ad1e-4971e20f7654", "moderator", "moderator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "ImageHash", "ImageUrl", "LastActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8c19c9dc-3278-467a-a475-7c2618aaa0b8", 0, "502e9841-6cb8-4935-b6f6-ba8ff1e053cf", "Chatr", "example@example.com", true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "admin", "AQAAAAEAACcQAAAAEJsRlZPcMCzGcvE9dQk7Bve5wa89r95dcinwY/hABKxFhyYHLkd+V+c+QkxFxQMVag==", null, false, "6c3b0988-2983-4b54-9c33-9e495a418c3b", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1bf54f1a-0222-4ea9-9f36-ba297806f56f", "8c19c9dc-3278-467a-a475-7c2618aaa0b8" },
                    { "722dcfd3-f197-4a97-b266-a8c8d460b4dd", "8c19c9dc-3278-467a-a475-7c2618aaa0b8" },
                    { "75217ef1-f65f-4816-9667-5b791ad3618e", "8c19c9dc-3278-467a-a475-7c2618aaa0b8" },
                    { "ad84670d-6f01-4d1d-aabe-7e79745504d4", "8c19c9dc-3278-467a-a475-7c2618aaa0b8" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1bf54f1a-0222-4ea9-9f36-ba297806f56f", "8c19c9dc-3278-467a-a475-7c2618aaa0b8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "722dcfd3-f197-4a97-b266-a8c8d460b4dd", "8c19c9dc-3278-467a-a475-7c2618aaa0b8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "75217ef1-f65f-4816-9667-5b791ad3618e", "8c19c9dc-3278-467a-a475-7c2618aaa0b8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ad84670d-6f01-4d1d-aabe-7e79745504d4", "8c19c9dc-3278-467a-a475-7c2618aaa0b8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bf54f1a-0222-4ea9-9f36-ba297806f56f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "722dcfd3-f197-4a97-b266-a8c8d460b4dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75217ef1-f65f-4816-9667-5b791ad3618e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad84670d-6f01-4d1d-aabe-7e79745504d4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8c19c9dc-3278-467a-a475-7c2618aaa0b8");

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
    }
}
