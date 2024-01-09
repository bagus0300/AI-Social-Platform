using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class deleteTopicFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Topics");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEP4YbXq0v40Ra2HOOBEz7ssTY6eY4Ue+ojoGaqCLud2tRnYtXWPZMNfAGmMoO5K8iA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEKRT1rPmdyKtofpYb45G9pxnBxNLlSB12ol6cd9cL3s5aCytmzYLvJmDtlykzIZyNw==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Topics",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEF5q5cp9XmZiQp4pFZMeXYhoOi1ctdUmRzuzSiHHJtofiRbHsIaG2feUCDksbtLWiQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEAU6+kFqc8au3HstUP7oIkzNYdgO3Bk/dH8zANasjPfcQ2vRYv1r+Co5UsgIFtDz9A==");
        }
    }
}
