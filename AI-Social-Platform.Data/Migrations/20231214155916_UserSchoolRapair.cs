using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class UserSchoolRapair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSchools");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEE8uyY1oSBUUN9JezWy/SgYWKWR941ien4lHSEBelPXSG5WgT3QY94CP61FfiDzgiw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEMKTXNrIwm7Whm0k6spR5+yUdfPlmhA7krwZNwybbT6jOsuh1FFMmqMMnbal98GUJA==");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SchoolId",
                table: "AspNetUsers",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Schools_SchoolId",
                table: "AspNetUsers",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Schools_SchoolId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SchoolId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserSchools",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSchools", x => new { x.UserId, x.SchoolId });
                    table.ForeignKey(
                        name: "FK_UserSchools_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSchools_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBczom69OStP7fAse8CjlW3f18CsS9RW8HeDEPagbwvi2TJYl5ENXFYxo6Q07HLejw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOnWr5fteQfYNXOt1Mx1X3L7uZy6QDCl9eawgd8+zxwYg0OYzAnJSUq50XphIDorpQ==");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchools_SchoolId",
                table: "UserSchools",
                column: "SchoolId");
        }
    }
}
