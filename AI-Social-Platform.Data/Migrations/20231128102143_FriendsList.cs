using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class FriendsList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("000bf091-aab7-4b90-9ced-da755a3f6e71"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("0345340c-c108-4e78-a0ea-f5077e57c78c"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("9edb1c1c-995a-4ab8-a76d-8f70c1d286cf"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("dca69b1f-aa1a-4e58-b2da-dda9b81d72cd"));

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f1c84d6-02a3-407a-8130-6573216a4348", "AQAAAAEAACcQAAAAEKp1BcOYnZQsSlc+MQhYawUge9D+OayyKAZKaFn0uTbC0NgzQg0gjNCIQwX3Q4GROQ==", "b3fd3689-7d22-4939-8168-54fe750782a7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b72623c-f588-48ad-a610-07f38abf7e89", "AQAAAAEAACcQAAAAEEgJLS7gNJhFhf6mOXkd8NhZl6M3MgeTEk++VBpXM5tFlTm80XfKfqaoQHicCOg8gw==", "9ecb28ab-b1a3-46ed-b63a-57db8fa84ffe" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("000bf091-aab7-4b90-9ced-da755a3f6e71"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 11, 26, 3, 27, 31, 882, DateTimeKind.Utc).AddTicks(7113) },
                    { new Guid("0345340c-c108-4e78-a0ea-f5077e57c78c"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 11, 26, 3, 27, 31, 882, DateTimeKind.Utc).AddTicks(7101) }
                });
        }
    }
}
