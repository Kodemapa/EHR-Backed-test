using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLabOperatorFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthStatus",
                table: "LabOperators");

            migrationBuilder.DropColumn(
                name: "Supervisor",
                table: "LabOperators");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentType",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Cancellation",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ModeOfVisit",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "RescheduledFromAppointmentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "HealthStatus",
                table: "LabOperators",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "LabOperators",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supervisor",
                table: "LabOperators",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledAt",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
