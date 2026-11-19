using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class n9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("441b8bbe-ccaa-4f68-bb46-dc2182ed34d8"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Administrator role", "Admin" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("97abc384-e98b-4b03-83eb-6afd11501f7c"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Worker Role", "Worker" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("441b8bbe-ccaa-4f68-bb46-dc2182ed34d8"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Worker Role", "Worker" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("97abc384-e98b-4b03-83eb-6afd11501f7c"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Administrator role", "Admin" });
        }
    }
}
