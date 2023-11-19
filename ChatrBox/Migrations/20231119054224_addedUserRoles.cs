using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class addedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "615a6106-c8d7-4f13-8594-e662278e8a3a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ef92074-2360-42d5-b143-76d4b2ac700b", "93ae9b3d-69c2-4a7d-b64a-2e426dfb3841", "superAdmin", "superAdmin" },
                    { "8e1c7b6b-acfb-4b12-9537-f3a9d5645921", "dd2344f3-7a56-4409-b579-d9d71a22a646", "admin", "admin" },
                    { "bc440625-2ab1-4673-a7c6-d65898cff9da", "dd5e0d25-aa7d-49d1-87a4-6ba284ccb664", "user", "user" },
                    { "df18ae19-9142-4fee-bf98-550bda70c266", "02d66c4d-2f73-483f-8602-443322231afe", "moderator", "moderator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ActiveUser", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IconId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fb18a647-aac5-4a96-9fd6-867c7beb2fec", 0, false, "90ace983-0903-469d-962b-b6ab28864c42", "Chatr", "admin@example.com", true, null, false, null, null, "admin", "AQAAAAEAACcQAAAAECK41qilwAx9CBa5+5NDGelD0DUtuwOKD/usa5OE1IQcH3aQZmGY3fpSpAB3+w22lQ==", null, false, "d9458859-0ce0-4622-aa34-ea667801a1ea", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2ef92074-2360-42d5-b143-76d4b2ac700b", "fb18a647-aac5-4a96-9fd6-867c7beb2fec" },
                    { "8e1c7b6b-acfb-4b12-9537-f3a9d5645921", "fb18a647-aac5-4a96-9fd6-867c7beb2fec" },
                    { "bc440625-2ab1-4673-a7c6-d65898cff9da", "fb18a647-aac5-4a96-9fd6-867c7beb2fec" },
                    { "df18ae19-9142-4fee-bf98-550bda70c266", "fb18a647-aac5-4a96-9fd6-867c7beb2fec" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2ef92074-2360-42d5-b143-76d4b2ac700b", "fb18a647-aac5-4a96-9fd6-867c7beb2fec" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e1c7b6b-acfb-4b12-9537-f3a9d5645921", "fb18a647-aac5-4a96-9fd6-867c7beb2fec" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bc440625-2ab1-4673-a7c6-d65898cff9da", "fb18a647-aac5-4a96-9fd6-867c7beb2fec" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "df18ae19-9142-4fee-bf98-550bda70c266", "fb18a647-aac5-4a96-9fd6-867c7beb2fec" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ef92074-2360-42d5-b143-76d4b2ac700b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e1c7b6b-acfb-4b12-9537-f3a9d5645921");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc440625-2ab1-4673-a7c6-d65898cff9da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df18ae19-9142-4fee-bf98-550bda70c266");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb18a647-aac5-4a96-9fd6-867c7beb2fec");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ActiveUser", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IconId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "615a6106-c8d7-4f13-8594-e662278e8a3a", 0, false, "e48bf105-aad2-4f44-901a-46b7ea387d5c", "Chatr", "admin@example.com", true, null, false, null, null, "admin", "AQAAAAEAACcQAAAAEDp0e5AAJAjGyLwdKNKqCD1XtFa9ujVVddODORHFzSjA4Fjai1gj0tIpuX28FiMy8g==", null, false, "b8eaa6c7-e0bd-4d8d-8085-d9bb6b9648f8", false, "admin" });
        }
    }
}
