using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class mediaPublicationSetToNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("1c319084-4239-4663-9f40-f61622481225"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("2b9fe1d4-16e9-4c50-be7e-87ec586134a6"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b05d953-19d9-4ca5-9d25-53f227db279c", "AQAAAAEAACcQAAAAEOkHSF1FfrbK4T93h0x33wLUoImg1PH47+tZutIX9MztZLZ9CHS3RhXRcJX6LzvzQg==", "c6ca99f6-8c7c-41fc-b524-07f214556a3a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb026b94-8655-4181-a0dc-a174b1194164", "AQAAAAEAACcQAAAAEJW/FKzExSsF+uVRCAScoFV6PeFGEM9aEQF9ZcvwiI1KJ/ULTlrXnLTHsP7bHPie+w==", "df9c8039-cd5e-4732-ac1c-5739323a4622" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("a9aa5a5d-fc74-471e-aa77-44800de8cbd5"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 11, 30, 17, 28, 39, 776, DateTimeKind.Utc).AddTicks(2087) },
                    { new Guid("b1f46acb-3591-4489-80ef-dc02670f5dd1"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 11, 30, 17, 28, 39, 776, DateTimeKind.Utc).AddTicks(2114) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("a9aa5a5d-fc74-471e-aa77-44800de8cbd5"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("b1f46acb-3591-4489-80ef-dc02670f5dd1"));

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
        }
    }
}
