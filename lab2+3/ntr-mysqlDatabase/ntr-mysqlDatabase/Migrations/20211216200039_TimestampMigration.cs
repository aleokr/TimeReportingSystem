using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ntr_mysqlDatabase.Migrations
{
    public partial class TimestampMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                nullable: true);
            migrationBuilder.AddColumn<DateTime>(
               name: "CreatedAt",
               table: "Projects",
               nullable: true);
            migrationBuilder.AddColumn<DateTime>(
               name: "CreatedAt",
               table: "Activities",
               nullable: true);
            migrationBuilder.AddColumn<DateTime>(
               name: "CreatedAt",
               table: "Subprojects",
               nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Projects");
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Activities");
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Subprojects");
        }
    }
}
