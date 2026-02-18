using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientAgeLastVisitStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastVisit",
                table: "Patients",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Patients",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastVisit",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Patients");
        }
    }
}
