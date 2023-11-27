using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class literallyDontKnow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

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

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MessagePlain",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13ef48fc-b252-4cf1-9d9c-f53d85e76880", "cc082b71-9845-4064-a0d0-d5558c7818c1", "user", "user" },
                    { "2795cd94-8cc2-45cb-93b8-5a5d409c317a", "0094d697-44bf-4a18-92a0-2b6ac9d1c32d", "admin", "admin" },
                    { "42173181-db4d-4df4-8146-9753e073ce22", "963128fa-e686-4934-b7ed-6cd7fd9f9e2a", "moderator", "moderator" },
                    { "e48bd7da-3292-48ba-bda7-3ffe9c863c7e", "2206cb43-3d7e-4012-8423-16fa73a1f590", "superAdmin", "superAdmin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "ImageHash", "ImageUrl", "LastActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337", 0, "88e1e5f0-09c6-419d-8e5d-3d90441969c5", "Chatr", "example@example.com", true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "admin", "AQAAAAEAACcQAAAAEAjmZWv/WFSNElC3j5/HbF0FbPXGNt1sYEwb/jAFOedcHxMeS/zeiNGNeoMkOgQlNQ==", null, false, "498515c5-a2a6-4b14-8a58-f7796027cbec", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "13ef48fc-b252-4cf1-9d9c-f53d85e76880", "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337" },
                    { "2795cd94-8cc2-45cb-93b8-5a5d409c317a", "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337" },
                    { "42173181-db4d-4df4-8146-9753e073ce22", "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337" },
                    { "e48bd7da-3292-48ba-bda7-3ffe9c863c7e", "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "13ef48fc-b252-4cf1-9d9c-f53d85e76880", "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2795cd94-8cc2-45cb-93b8-5a5d409c317a", "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "42173181-db4d-4df4-8146-9753e073ce22", "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e48bd7da-3292-48ba-bda7-3ffe9c863c7e", "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13ef48fc-b252-4cf1-9d9c-f53d85e76880");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2795cd94-8cc2-45cb-93b8-5a5d409c317a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42173181-db4d-4df4-8146-9753e073ce22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e48bd7da-3292-48ba-bda7-3ffe9c863c7e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3be47a4b-c7fe-49e8-a378-1a0a9d5d2337");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessagePlain",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
