using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class addingLatestActivityColumnIntoPublications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("8f82ff0f-1d2b-4362-80dd-e79daddfadd4"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("e0b2de99-fe73-4f36-ae7f-d70b496b27d9"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LatestActivity",
                table: "Publications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f5aa852-1bf6-4675-90c3-3dd34c219c74", "AQAAAAEAACcQAAAAEMUELlyPwHHHxl0mfl6+43GJ560Qe+q+r+QKlO7U17rlHaiGGPZcJKALrh3F2OYtvw==", "383a5741-488d-4d37-9577-cc91177d5e9a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c334a97d-eb15-4243-998c-ffe2cd3f3815", "AQAAAAEAACcQAAAAEF7+NLhX1m/eSCGmPGThhdXC1E9DU5vp89CvFfPX+1fh+kQt3WAqFZLHeq6tYSDuqA==", "2bfd45cf-9db9-4aae-bb11-37ad33aeed3e" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated", "DateModified", "LastCommented", "LatestActivity" },
                values: new object[,]
                {
                    { new Guid("140548e3-2cf6-4dee-84d8-92e766851a51"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 12, 5, 1, 20, 55, 230, DateTimeKind.Utc).AddTicks(7155), null, null, new DateTime(2023, 12, 5, 1, 20, 55, 230, DateTimeKind.Utc).AddTicks(7155) },
                    { new Guid("e2611d6c-8bb0-4ce1-9be7-bfdce8541bc8"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 12, 5, 1, 20, 55, 230, DateTimeKind.Utc).AddTicks(7121), null, null, new DateTime(2023, 12, 5, 1, 20, 55, 230, DateTimeKind.Utc).AddTicks(7123) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("140548e3-2cf6-4dee-84d8-92e766851a51"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("e2611d6c-8bb0-4ce1-9be7-bfdce8541bc8"));

            migrationBuilder.DropColumn(
                name: "LatestActivity",
                table: "Publications");

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
    }
}
