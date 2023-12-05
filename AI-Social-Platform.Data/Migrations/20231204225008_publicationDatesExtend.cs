using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class publicationDatesExtend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("16d55b18-b0fb-4cb0-9de5-4b31abb950f1"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("ac1ec86e-ec94-48bf-9630-44c09fd6b21d"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Publications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCommented",
                table: "Publications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72e4aca8-e4cb-4f92-b36a-b2db3f9b74ac", "AQAAAAEAACcQAAAAEIAkDSw4gA1pzVXqL3tfd0HaOEcahC9oypuAD58W4f89Hpogcj7x5IQz2Qz+JElFsA==", "a047c1a6-fda1-4f4f-90ca-fb9e81844ff0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a4f573f-087a-4654-9dba-ed14bfb4301d", "AQAAAAEAACcQAAAAEABS1bRfIhl+tSKyLWrodL1RasMutv2kE22N3TUHpZ4sZKuGU8oe0Mva9tPPF9Lrbw==", "cd044a54-1374-4d18-9c29-e2857ef77b95" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated", "DateModified", "LastCommented" },
                values: new object[,]
                {
                    { new Guid("8f82ff0f-1d2b-4362-80dd-e79daddfadd4"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 12, 4, 22, 50, 7, 889, DateTimeKind.Utc).AddTicks(7592), null, null },
                    { new Guid("e0b2de99-fe73-4f36-ae7f-d70b496b27d9"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 12, 4, 22, 50, 7, 889, DateTimeKind.Utc).AddTicks(7578), null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("8f82ff0f-1d2b-4362-80dd-e79daddfadd4"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("e0b2de99-fe73-4f36-ae7f-d70b496b27d9"));

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "LastCommented",
                table: "Publications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7379b240-22d6-4813-901b-59fed7bd7884", "AQAAAAEAACcQAAAAEM2XYvGz8xx3ybRJJszczgz9P5DyVweBLAPZKYNudRhgsfNxSeN8y9xXFjBr8FL/Wg==", "29841d63-34b3-4df8-b2b0-d8d2724dd541" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbb4c8a9-8075-4d3d-a62b-3a7b1fd3a0f7", "AQAAAAEAACcQAAAAEJ/9sq0sjf2aTaitiaBXydGQMm5HnT4VqxHUfMBfQWoyRjEJck0Yy/JTIyawiTK8VQ==", "0561d8cf-6588-4626-9952-ae8d767a9031" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("16d55b18-b0fb-4cb0-9de5-4b31abb950f1"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 12, 2, 5, 28, 43, 853, DateTimeKind.Utc).AddTicks(1453) },
                    { new Guid("ac1ec86e-ec94-48bf-9630-44c09fd6b21d"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 12, 2, 5, 28, 43, 853, DateTimeKind.Utc).AddTicks(1482) }
                });
        }
    }
}
