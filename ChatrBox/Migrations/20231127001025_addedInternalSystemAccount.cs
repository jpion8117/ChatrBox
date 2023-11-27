using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class addedInternalSystemAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f629ebb-ad6e-4229-bf31-96df7777d787", "a57ea8bc-c64b-44e3-8efe-c97f354d5b52", "InternalSystem", "INTERNALSYSTEM" },
                    { "3ba6dd14-b9e9-4409-9941-793247d3071a", "acc67f93-d4ae-498e-b645-3407c0c31085", "admin", "ADMIN" },
                    { "629c15ba-ee9e-41ab-bcf6-651556ae72e2", "12c054d3-7a68-40f1-96f0-f354a1d8fdd0", "moderator", "MODERATOR" },
                    { "94cffa0e-c44a-436a-b39c-5f8e5fd66dd3", "f2b8b426-2e5c-4f1c-9f11-391ff36694e2", "superAdmin", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "ImageHash", "ImageUrl", "LastActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "04ce9b7e-1005-459b-be94-fff2f7557a85", 0, "d38daf8c-6106-412a-b1ab-7cb13cbba0c3", "Chatr", "example@example.com", true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEPPb9u1EpRmfFzJtUt6u3nYiODR+voXL3M7eUzTg1eWQ/ogjoBv6cJGWqHEnQdeyNA==", null, false, "f6f4cb94-6f53-4572-829b-c9c8f73f29c4", false, "admin" },
                    { "eeee8f55-4282-4821-956d-58dd1f8b9246", 0, "dd2611cc-c264-43f7-b7f5-bdc342efd327", "Chatr", "", true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "CHEDDAR_CHATR", "AQAAAAEAACcQAAAAEKrMFLiOnPHSpvmByqswoWrCtBrJ7BfQ+v7GhemjzoeVNO9tE0WnOge9qDU0Iyra5Q==", null, false, "f4a642b9-5f9f-4b90-ac49-b44eb874e2c8", false, "Cheddar_Chatr" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3ba6dd14-b9e9-4409-9941-793247d3071a", "04ce9b7e-1005-459b-be94-fff2f7557a85" },
                    { "629c15ba-ee9e-41ab-bcf6-651556ae72e2", "04ce9b7e-1005-459b-be94-fff2f7557a85" },
                    { "94cffa0e-c44a-436a-b39c-5f8e5fd66dd3", "04ce9b7e-1005-459b-be94-fff2f7557a85" },
                    { "2f629ebb-ad6e-4229-bf31-96df7777d787", "eeee8f55-4282-4821-956d-58dd1f8b9246" },
                    { "3ba6dd14-b9e9-4409-9941-793247d3071a", "eeee8f55-4282-4821-956d-58dd1f8b9246" },
                    { "629c15ba-ee9e-41ab-bcf6-651556ae72e2", "eeee8f55-4282-4821-956d-58dd1f8b9246" },
                    { "94cffa0e-c44a-436a-b39c-5f8e5fd66dd3", "eeee8f55-4282-4821-956d-58dd1f8b9246" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3ba6dd14-b9e9-4409-9941-793247d3071a", "04ce9b7e-1005-459b-be94-fff2f7557a85" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "629c15ba-ee9e-41ab-bcf6-651556ae72e2", "04ce9b7e-1005-459b-be94-fff2f7557a85" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "94cffa0e-c44a-436a-b39c-5f8e5fd66dd3", "04ce9b7e-1005-459b-be94-fff2f7557a85" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2f629ebb-ad6e-4229-bf31-96df7777d787", "eeee8f55-4282-4821-956d-58dd1f8b9246" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3ba6dd14-b9e9-4409-9941-793247d3071a", "eeee8f55-4282-4821-956d-58dd1f8b9246" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "629c15ba-ee9e-41ab-bcf6-651556ae72e2", "eeee8f55-4282-4821-956d-58dd1f8b9246" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "94cffa0e-c44a-436a-b39c-5f8e5fd66dd3", "eeee8f55-4282-4821-956d-58dd1f8b9246" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f629ebb-ad6e-4229-bf31-96df7777d787");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ba6dd14-b9e9-4409-9941-793247d3071a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "629c15ba-ee9e-41ab-bcf6-651556ae72e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94cffa0e-c44a-436a-b39c-5f8e5fd66dd3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04ce9b7e-1005-459b-be94-fff2f7557a85");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeee8f55-4282-4821-956d-58dd1f8b9246");

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
        }
    }
}
