using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class n8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0eecb374-ddcd-4a45-8b3d-9476b264fc86"), "Regular user role", "User" },
                    { new Guid("441b8bbe-ccaa-4f68-bb46-dc2182ed34d8"), "Worker Role", "Worker" },
                    { new Guid("97abc384-e98b-4b03-83eb-6afd11501f7c"), "Administrator role", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0eecb374-ddcd-4a45-8b3d-9476b264fc86"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("441b8bbe-ccaa-4f68-bb46-dc2182ed34d8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("97abc384-e98b-4b03-83eb-6afd11501f7c"));
        }
    }
}
