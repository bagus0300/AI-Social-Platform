using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class publicationmediarelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("9edb1c1c-995a-4ab8-a76d-8f70c1d286cf"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("dca69b1f-aa1a-4e58-b2da-dda9b81d72cd"));

            migrationBuilder.AddColumn<Guid>(
                name: "PublicationId",
                table: "MediaFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a26e6d3-f1b6-46eb-a86c-5bdeec4b3c1f", "AQAAAAEAACcQAAAAEICmbdM7Xu8YtZ8E/0d/hH7QZo8iLMMdp1T5ZfY1ZNL1D5aNwh3ZQkFQeYKPAZMqcg==", "8d62f6d6-b34f-4395-925f-babde48a6f32" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80febb55-40a7-4ee2-add5-67998f6dad4c", "AQAAAAEAACcQAAAAEMhMk4eaLof/Xw16XcNrlNYBjBvVUS8ng+wJF5xRNos4+aF4AkN3bnjbIruTAfDVwA==", "00bfe1b4-d450-4746-8865-04ab8b0cf05a" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("1c319084-4239-4663-9f40-f61622481225"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 11, 30, 16, 32, 50, 863, DateTimeKind.Utc).AddTicks(6402) },
                    { new Guid("2b9fe1d4-16e9-4c50-be7e-87ec586134a6"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 11, 30, 16, 32, 50, 863, DateTimeKind.Utc).AddTicks(6381) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_PublicationId",
                table: "MediaFiles",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_Publications_PublicationId",
                table: "MediaFiles",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_Publications_PublicationId",
                table: "MediaFiles");

            migrationBuilder.DropIndex(
                name: "IX_MediaFiles_PublicationId",
                table: "MediaFiles");

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("1c319084-4239-4663-9f40-f61622481225"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("2b9fe1d4-16e9-4c50-be7e-87ec586134a6"));

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "MediaFiles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1bc27295-4c52-49ef-80b1-c022aa1fb182", "AQAAAAEAACcQAAAAECub/fB1NIQc+c97bZ47wEbGkIcWCh2WdNhs/Oeak3jli/R3t2QO3pNeUN+7+OpzaQ==", "bd10e1e3-69d9-497e-9340-9ea494e5fc4f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d735695d-df18-4dbd-9751-e059883c8e1f", "AQAAAAEAACcQAAAAEKovpOCr8UY7WrDC5fhCVSawxZi5yigl63rVIEHcwTcCtcpZhpmiWzbkyK8VSocSfw==", "3afef2c8-ed0e-4b05-93f4-aeedcc241e91" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("9edb1c1c-995a-4ab8-a76d-8f70c1d286cf"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 11, 28, 10, 21, 43, 392, DateTimeKind.Utc).AddTicks(6124) },
                    { new Guid("dca69b1f-aa1a-4e58-b2da-dda9b81d72cd"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 11, 28, 10, 21, 43, 392, DateTimeKind.Utc).AddTicks(6106) }
                });
        }
    }
}
