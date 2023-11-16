using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class addMediaFilesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DataFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaFiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "662633b3-9cc5-4c02-ab71-e06cd8d47dd7", "AQAAAAEAACcQAAAAEGYn4aHKMsu/5/1sFDhSexcENv/E/pifrDQjigUdTjgSxWV6T9V65utucTfPG0Gjjw==", "d48ebbb0-baac-4ad0-90d2-7b4daf70830c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a030fa8-9e1d-4707-966a-d3cd927a680d", "AQAAAAEAACcQAAAAEN3vLVOh43JgivqdzHreuqJ5DLrQAS4CPZ1TIH78Jlo7ZYmaf2/LGPHuDBfLlU4biw==", "c3249bbf-573c-4f77-a385-661a11299bc3" });

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_UserId",
                table: "MediaFiles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaFiles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "189744e7-5114-41c5-932a-9481827640e1", "AQAAAAEAACcQAAAAEIju37Us2zVzc0xQ/B8+UgKjEZmUUyg67KYeznMGstfYLUGNWtJ/zsTKTAbl3CAdhA==", "968e325c-e790-40d3-941a-a5fafc59ac7a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9bd310b-3bac-434b-81db-352bd8619a0b", "AQAAAAEAACcQAAAAEC8xr8mLXqWcPmMIkL3Lgi6ESdNyJOfzNGrbjfsCCMJF2pnMvNTYZfBfkcc7fZ+5HA==", "4d06bef0-d76b-4226-9b20-31f4fec33de1" });
        }
    }
}
