using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class changedTopicToTopics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Topic_TopicId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Topic_Communities_CommunityId",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

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

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "Topics");

            migrationBuilder.RenameIndex(
                name: "IX_Topic_CommunityId",
                table: "Topics",
                newName: "IX_Topics_CommunityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Topics_TopicId",
                table: "Messages",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Communities_CommunityId",
                table: "Topics",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Topics_TopicId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Communities_CommunityId",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

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

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "Topic");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_CommunityId",
                table: "Topic",
                newName: "IX_Topic_CommunityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Topic_TopicId",
                table: "Messages",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_Communities_CommunityId",
                table: "Topic",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
