﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HierarchicalCatalogsView.Migrations
{
    /// <inheritdoc />
    public partial class DbInitializedWithPrimaryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "CatalogId", "CatalogName", "ParentId" },
                values: new object[,]
                {
                    { 1, "Creating Digital Images", null },
                    { 2, "Resources", 1 },
                    { 3, "Evidence", 1 },
                    { 4, "Graphic Products", 1 },
                    { 5, "Primary Sources", 2 },
                    { 6, "Secondary Sources", 2 },
                    { 7, "Process", 4 },
                    { 8, "Final Product", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: 1);
        }
    }
}
