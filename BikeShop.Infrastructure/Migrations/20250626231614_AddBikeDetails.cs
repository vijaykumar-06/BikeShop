using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBikeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Colour",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Ref",
                table: "Bikes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "Colour",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "Ref",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Bikes");
        }
    }
}
