using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class AddedLoginAndRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f38a1b1-4879-4ee8-a16d-6d983f8b3947", "AQAAAAEAACcQAAAAEILpqkKjxXCPKdlNeGK8q8MlCoNXL1RIllV9PioAM3fX63Z+CT/+zfr+2f2OHdozzQ==", "86da032c-abc5-4030-b041-66e0432d64cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "810518c0-4c40-4813-bcb6-7436a8e6b0b2", "AQAAAAEAACcQAAAAEHupO5f0lbj2MutFOhKRF8djc3pROHO7vfGYBKn7eUDyrjDkVkVD3pBcHDnNnPARLw==", "95e43c07-4269-4f82-a804-1bad5e68eb26" });
        }
    }
}
