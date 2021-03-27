using Microsoft.EntityFrameworkCore.Migrations;

namespace JudgeSystem.Entities.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "da2f9cd9-c076-4709-9df9-355b5568906a", 0, "eae6f489-ca3c-4f4d-9679-93a752919790", null, true, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEDj3sIhvYfUebm9Xed+cijFlBy8OZxZ8Kt2sVw/qcQAhCLl0hshtGIfW0qOCLG+VCQ==", null, false, "601bed52-32ff-4309-a548-6309e88b0626", false, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da2f9cd9-c076-4709-9df9-355b5568906a");
        }
    }
}
