using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class setnullMediaPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_Publications_PublicationId",
                table: "MediaFiles");

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("a9aa5a5d-fc74-471e-aa77-44800de8cbd5"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("b1f46acb-3591-4489-80ef-dc02670f5dd1"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicationId",
                table: "MediaFiles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f870b1d-f6b4-4533-b557-54163c041b54", "AQAAAAEAACcQAAAAEOiPf5PEmsHvUV+pmjdA/AgbY9bwoFKB6LlagiCZzu1CWfdOpd51y25fAhh6M8Rffw==", "5ae03fe2-94e4-4501-bf6e-ca04eafff18a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f42b186-bbf8-4db4-a8d9-1ce53140ee9e", "AQAAAAEAACcQAAAAEIY3VQU7YV+vPkFe5btG31QKza27DuKLISkenbYqXxHg7StqbhetamhNEjjvax8OQQ==", "2063e480-0d27-4468-8581-272b7ea920d2" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("4c6f2908-ee71-495e-b855-ff2eb4fe8d63"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 11, 30, 17, 31, 56, 169, DateTimeKind.Utc).AddTicks(4114) },
                    { new Guid("d529d306-afb9-4922-8899-ae34490d1e40"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 11, 30, 17, 31, 56, 169, DateTimeKind.Utc).AddTicks(4086) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_Publications_PublicationId",
                table: "MediaFiles",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_Publications_PublicationId",
                table: "MediaFiles");

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("4c6f2908-ee71-495e-b855-ff2eb4fe8d63"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("d529d306-afb9-4922-8899-ae34490d1e40"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicationId",
                table: "MediaFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_Publications_PublicationId",
                table: "MediaFiles",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
