using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingHTTPie.Migrations
{
    /// <inheritdoc />
    public partial class DateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Persons",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Persons",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Hobbies",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Hobbies",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Hobbies");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Hobbies");
        }
    }
}
