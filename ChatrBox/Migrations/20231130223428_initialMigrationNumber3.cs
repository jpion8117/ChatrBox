using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatrBox.Migrations
{
    public partial class initialMigrationNumber3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastActive = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Visibility = table.Column<int>(type: "int", nullable: false),
                    ContentFilter = table.Column<int>(type: "int", nullable: false),
                    ImageHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communities_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommunityUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatrId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommunityId = table.Column<int>(type: "int", nullable: false),
                    CanPost = table.Column<bool>(type: "bit", nullable: false),
                    CanRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunityUsers_AspNetUsers_ChatrId",
                        column: x => x.ChatrId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityUsers_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostPermission = table.Column<int>(type: "int", nullable: false),
                    LastPost = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CommunityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessagePlain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2cff87de-1389-4ef1-99cb-8da994816a60", "c367ad35-a4e5-4e97-bf2d-51e212de81cf", "admin", "ADMIN" },
                    { "3fa0fda4-4a86-4eb2-b18e-e503a3ffa5b9", "affbeb71-fba0-45a3-bfed-5f2aba165c6d", "InternalSystem", "INTERNALSYSTEM" },
                    { "a4b7313a-317e-498b-8002-f65430ee638e", "dbea61dc-6f71-4bb2-9ac7-1257ecdbef54", "superAdmin", "SUPERADMIN" },
                    { "ba374366-a3ae-42bd-8254-a452d1de860a", "dff1aab9-3afe-4e29-a368-b71689f8ff8f", "moderator", "MODERATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "ImageHash", "ImageUrl", "LastActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1209cf85-72db-45f8-bd6c-c86f6dc346c4", 0, "578e3c7a-0545-43e3-8f35-cc55e74e6441", "Chatr", "example@example.com", true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEH/Jmvxt5H6Wu/pZDsnnqOArFy24B4r8hHhTQpyD7J6L+MEQNbhT1vab3724uYt0CQ==", null, false, "659990fa-2c72-49a9-a02e-c319b0f958f4", false, "admin" },
                    { "b0af2ed0-5f6c-48f3-9e4e-10bcaaa00ace", 0, "f83dba94-c5d9-4f6b-9c28-da8ec10ff2ba", "Chatr", "", true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "CHEDDAR_CHATR", "AQAAAAEAACcQAAAAEFz3cKigapCO+J4Xiy7fDIq+lpg2/MPphgPkwhKHqBrgS47xKXF9/QefUvvtAUg2tQ==", null, false, "f283f4f9-ac26-4496-8445-d2ed00b66e8d", false, "Cheddar_Chatr" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2cff87de-1389-4ef1-99cb-8da994816a60", "1209cf85-72db-45f8-bd6c-c86f6dc346c4" },
                    { "a4b7313a-317e-498b-8002-f65430ee638e", "1209cf85-72db-45f8-bd6c-c86f6dc346c4" },
                    { "ba374366-a3ae-42bd-8254-a452d1de860a", "1209cf85-72db-45f8-bd6c-c86f6dc346c4" },
                    { "2cff87de-1389-4ef1-99cb-8da994816a60", "b0af2ed0-5f6c-48f3-9e4e-10bcaaa00ace" },
                    { "3fa0fda4-4a86-4eb2-b18e-e503a3ffa5b9", "b0af2ed0-5f6c-48f3-9e4e-10bcaaa00ace" },
                    { "a4b7313a-317e-498b-8002-f65430ee638e", "b0af2ed0-5f6c-48f3-9e4e-10bcaaa00ace" },
                    { "ba374366-a3ae-42bd-8254-a452d1de860a", "b0af2ed0-5f6c-48f3-9e4e-10bcaaa00ace" }
                });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "ContentFilter", "Description", "ImageHash", "ImageUrl", "Name", "OwnerId", "Tags", "Visibility" },
                values: new object[] { 1, 0, "System generated community used for testing layouts", "", "", "Loreum Ipsum", "b0af2ed0-5f6c-48f3-9e4e-10bcaaa00ace", null, 0 });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CommunityId", "Description", "DisplayOrder", "LastPost", "Name", "PostPermission" },
                values: new object[] { 1, 1, "System generated topic used for layout testing", 0, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testing", 1 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "IsEdited", "MessagePlain", "SenderId", "Timestamp", "TopicId" },
                values: new object[,]
                {
                    { 1, false, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vel semper risus. Aenean vel malesuada dui, semper rutrum tortor. Nulla ex mi, aliquet nec aliquet vitae, interdum vitae lorem. Nulla semper varius sem. Maecenas placerat erat mattis, tempor orci vitae, tincidunt nisl. Donec justo sem, fringilla quis bibendum nec, commodo quis nisl. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi molestie vitae neque ut bibendum.", "b0af2ed0-5f6c-48f3-9e4e-10bcaaa00ace", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, false, "Curabitur eu condimentum eros. Cras vel sodales sapien, vitae rutrum dui. Donec eros erat, cursus vel congue sed, venenatis sit amet augue. Curabitur eget mollis diam, non scelerisque velit. Sed tincidunt risus vitae erat tempus consequat. Fusce nulla velit, cursus sed efficitur sed, sagittis id leo. Etiam volutpat urna purus, id scelerisque lacus gravida ut. Integer neque nunc, placerat sit amet tincidunt ut, scelerisque a velit. Aliquam id leo commodo, vehicula felis in, posuere mi. Fusce ut magna eu dui laoreet blandit quis quis ipsum.", "b0af2ed0-5f6c-48f3-9e4e-10bcaaa00ace", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                  });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_OwnerId",
                table: "Communities",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityUsers_ChatrId",
                table: "CommunityUsers",
                column: "ChatrId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityUsers_CommunityId",
                table: "CommunityUsers",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TopicId",
                table: "Messages",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CommunityId",
                table: "Topics",
                column: "CommunityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CommunityUsers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
