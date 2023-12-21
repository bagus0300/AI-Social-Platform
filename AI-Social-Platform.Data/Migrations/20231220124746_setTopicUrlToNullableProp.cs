using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class setTopicUrlToNullableProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "topicUrl",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEFwaDDcjzewOCSQ/d9U8lx5tlotMAvLoliVhfbFw7CMp6/DqBVw38KVVH2QO1eSRJg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBXUlfHEXGDgs1Sq6pvf3IXI+1hqtc+3Zop45jzSDMVzo8dxZGx8InIPN3rR4BORqg==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "topicUrl",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEO+k5gpOSjALJ8hzTym7E0shsoInpLL1rXHq7Fzpv3nuqeXZOFAXaTAPW+EqC/vjQQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAENbA3Wci6EwhEOsokfr9BE/S3HHhc4DnSSHAvKIMHr/+inPrzKMeQsvmtEufQBPWZg==");
        }
    }
}
