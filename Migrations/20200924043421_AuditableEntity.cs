using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace design_pattern_repository.Migrations
{
    public partial class AuditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Star",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Star",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Star",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Star",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Planet",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Planet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Planet",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Planet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Star");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Star");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Star");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Star");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Planet");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Planet");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Planet");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Planet");
        }
    }
}
