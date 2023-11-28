using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class updateDeleteBehaviorOnPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("4d3921a7-0312-4ea4-a3d9-1742cfe34a93"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("5aab2881-2ee8-48f4-9438-3ca29cc53b0c"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("000bf091-aab7-4b90-9ced-da755a3f6e71"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("0345340c-c108-4e78-a0ea-f5077e57c78c"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1728f2b3-00f7-40af-b6a5-c53b1a9df8ee", "AQAAAAEAACcQAAAAEKplQhprWD7nSVisJeVhJzMA6LpMD6K41SWNMESyyyYQmcvBNPgs4NxKr8MC9LKh4g==", "0587a909-ba16-4964-8a5f-b95f0e6a169a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3874bf05-723b-496e-89df-5e30389a0c7c", "AQAAAAEAACcQAAAAEHA8dvENAtxm3WmlzQMOKU3M5fSrws4BzrV3pQ/j+orksoQtKYNYt8Iwm758/a3ccQ==", "6742573c-c9ea-4d6a-beb5-b512e38ec7c5" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("4d3921a7-0312-4ea4-a3d9-1742cfe34a93"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 11, 15, 16, 26, 43, 368, DateTimeKind.Utc).AddTicks(4148) },
                    { new Guid("5aab2881-2ee8-48f4-9438-3ca29cc53b0c"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 11, 15, 16, 26, 43, 368, DateTimeKind.Utc).AddTicks(4136) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id");
        }
    }
}
