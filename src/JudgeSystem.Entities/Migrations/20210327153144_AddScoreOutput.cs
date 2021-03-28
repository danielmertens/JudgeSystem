using Microsoft.EntityFrameworkCore.Migrations;

namespace JudgeSystem.Entities.Migrations
{
    public partial class AddScoreOutput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da2f9cd9-c076-4709-9df9-355b5568906a");

            migrationBuilder.AddColumn<string>(
                name: "ScoreOutput",
                table: "Solutions",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4149297a-79d5-462b-aeb3-02e4e885ff3d", 0, "40183104-4798-4b9e-95f1-e32aae77132b", null, true, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEIAAebpl+bQXRlqKoDlTo4F03U/+HoDZJ6C7gzKzO9R3tqVG+Utwe5yCBvbbUvalOQ==", null, false, "4c30bd36-5446-4a7a-a266-9673f72b8a21", false, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4149297a-79d5-462b-aeb3-02e4e885ff3d");

            migrationBuilder.DropColumn(
                name: "ScoreOutput",
                table: "Solutions");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "da2f9cd9-c076-4709-9df9-355b5568906a", 0, "eae6f489-ca3c-4f4d-9679-93a752919790", null, true, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEDj3sIhvYfUebm9Xed+cijFlBy8OZxZ8Kt2sVw/qcQAhCLl0hshtGIfW0qOCLG+VCQ==", null, false, "601bed52-32ff-4309-a548-6309e88b0626", false, "Admin" });
        }
    }
}
