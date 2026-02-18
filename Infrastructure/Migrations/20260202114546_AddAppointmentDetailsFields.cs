using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentDetailsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "Appointments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsRescheduled",
                table: "Appointments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsRescheduled",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "Appointments");
        }
    }
}
