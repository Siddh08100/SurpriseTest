using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddDateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createddate",
                table: "orders",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<string>(
                name: "updatedby",
                table: "orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateddate",
                table: "orders",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "now()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdby",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "createddate",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "updatedby",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "updateddate",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
