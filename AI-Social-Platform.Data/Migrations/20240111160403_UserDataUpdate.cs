using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class UserDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJPzUL9k575A6C3IyH1no/Xx4Rb8hQlxq7hDRieCfly7/1yNQ6i70wrNRVVDk2Sg4w==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJYnURFRY+vi19lyd+xMKeKzOT3w66bYM87g2Rm5wp/XPgP9jzBe20yLv6fcQ40dbA==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEI957glX2g2hXCnvXbTTZAflCHmmjhQyQqn0ialtbyRlGMSyKodLiWpSl9EwPlGuxQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEC86xsiCtdt3TWFZUyTp1lhzu9g1lpHehyukNSZWAei+sYNXFD0ydDZyiWcR7aNi8g==");
        }
    }
}
