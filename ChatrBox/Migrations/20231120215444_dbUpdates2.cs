using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class dbUpdates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "144cd420-4adc-4400-807a-9007b538e38f", "fd3056c6-48de-4e13-b699-802b036209d3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "23ae6dde-7fa8-4ead-a3dd-f2981c1951cc", "fd3056c6-48de-4e13-b699-802b036209d3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6fc3845f-851c-4e63-8700-9b8f0f0d8671", "fd3056c6-48de-4e13-b699-802b036209d3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bc273a5d-05f5-4eeb-a846-bce0cb19c1c3", "fd3056c6-48de-4e13-b699-802b036209d3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "144cd420-4adc-4400-807a-9007b538e38f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23ae6dde-7fa8-4ead-a3dd-f2981c1951cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fc3845f-851c-4e63-8700-9b8f0f0d8671");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc273a5d-05f5-4eeb-a846-bce0cb19c1c3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd3056c6-48de-4e13-b699-802b036209d3");

            migrationBuilder.DropColumn(
                name: "ActiveUser",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPost",
                table: "Topic",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "CanPost",
                table: "CommunityUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanRead",
                table: "CommunityUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "605b0f67-4936-4bcf-b80a-8911f50a4f8e", "fc057a53-dbb0-43cf-8351-3f10a7b439df", "admin", "admin" },
                    { "895739ca-eb45-464a-86e6-1c599153ee8c", "516fe419-1cc7-4713-a56f-3a3db2e12c25", "moderator", "moderator" },
                    { "9cb9696f-fefc-4975-b5bc-3ff9668d4eca", "07b48cfc-c837-4b28-9c4d-d7f003510e6c", "superAdmin", "superAdmin" },
                    { "eb2c8585-3f1b-482a-a51b-1fe35aff08d5", "53c0f1ef-9db1-4b27-a25d-c31edd3b0923", "user", "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IconId", "LastActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f", 0, "7dbd9dc9-3cfb-4d73-8475-facc8d139277", "Chatr", "admin@example.com", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "admin", "AQAAAAEAACcQAAAAEC3Au9yCE/dr9oJZrSV0fPHakC/T61ORxXrPhOMG350WS97oNdtvPuOoBu7PsMTB1A==", null, false, "c1b2264f-4878-44fc-a3fd-e6f86b0ca894", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "605b0f67-4936-4bcf-b80a-8911f50a4f8e", "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f" },
                    { "895739ca-eb45-464a-86e6-1c599153ee8c", "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f" },
                    { "9cb9696f-fefc-4975-b5bc-3ff9668d4eca", "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f" },
                    { "eb2c8585-3f1b-482a-a51b-1fe35aff08d5", "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "605b0f67-4936-4bcf-b80a-8911f50a4f8e", "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "895739ca-eb45-464a-86e6-1c599153ee8c", "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9cb9696f-fefc-4975-b5bc-3ff9668d4eca", "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eb2c8585-3f1b-482a-a51b-1fe35aff08d5", "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "605b0f67-4936-4bcf-b80a-8911f50a4f8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "895739ca-eb45-464a-86e6-1c599153ee8c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cb9696f-fefc-4975-b5bc-3ff9668d4eca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb2c8585-3f1b-482a-a51b-1fe35aff08d5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59ef7aa6-19e7-453d-ad2a-6fec2fa69e1f");

            migrationBuilder.DropColumn(
                name: "LastPost",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "CanPost",
                table: "CommunityUsers");

            migrationBuilder.DropColumn(
                name: "CanRead",
                table: "CommunityUsers");

            migrationBuilder.DropColumn(
                name: "LastActive",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "ActiveUser",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "144cd420-4adc-4400-807a-9007b538e38f", "41e169cd-1661-4a45-aaeb-48560362a8f8", "moderator", "moderator" },
                    { "23ae6dde-7fa8-4ead-a3dd-f2981c1951cc", "305a56b0-e16b-4444-8e2e-3b4c40578205", "admin", "admin" },
                    { "6fc3845f-851c-4e63-8700-9b8f0f0d8671", "c7346af7-509d-4c64-99b4-4ae9633896f0", "superAdmin", "superAdmin" },
                    { "bc273a5d-05f5-4eeb-a846-bce0cb19c1c3", "4ea541f3-13a8-4fc1-83e9-3947b3aeb681", "user", "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ActiveUser", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IconId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fd3056c6-48de-4e13-b699-802b036209d3", 0, false, "42a8e6cc-a499-42bc-9a71-b436415292a7", "Chatr", "admin@example.com", true, null, false, null, null, "admin", "AQAAAAEAACcQAAAAED78SBiYwU5PXy2hV+/0ZhdvXQcDhxgZq7vebuMtr1IEScjyjLXwXXA0iRZvyeOS+g==", null, false, "80d3c06b-d1e0-467b-80d4-7653c9223c66", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "144cd420-4adc-4400-807a-9007b538e38f", "fd3056c6-48de-4e13-b699-802b036209d3" },
                    { "23ae6dde-7fa8-4ead-a3dd-f2981c1951cc", "fd3056c6-48de-4e13-b699-802b036209d3" },
                    { "6fc3845f-851c-4e63-8700-9b8f0f0d8671", "fd3056c6-48de-4e13-b699-802b036209d3" },
                    { "bc273a5d-05f5-4eeb-a846-bce0cb19c1c3", "fd3056c6-48de-4e13-b699-802b036209d3" }
                });
        }
    }
}
