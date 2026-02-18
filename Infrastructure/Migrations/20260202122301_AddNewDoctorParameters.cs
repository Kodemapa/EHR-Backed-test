using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDoctorParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "CancellationReason",
            //     table: "Appointments");

            // migrationBuilder.DropColumn(
            //     name: "DoctorName",
            //     table: "Appointments");

            // migrationBuilder.DropColumn(
            //     name: "IsRescheduled",
            //     table: "Appointments");

            // migrationBuilder.DropColumn(
            //     name: "PatientName",
            //     table: "Appointments");

            // migrationBuilder.DropColumn(
            //     name: "Prescription",
            //     table: "Appointments");

            // migrationBuilder.DropColumn(
            //     name: "RegisterNumber",
            //     table: "Appointments");

            // migrationBuilder.AddColumn<string>(
            //     name: "Area",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "City",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Degree",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Document",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "NameOfCentre",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "OfficeNumber",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "PF",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "PhoneResidence",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Pincode",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Registration",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Title",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Document",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "NameOfCentre",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "OfficeNumber",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PF",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PhoneResidence",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Pincode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Registration",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Doctors");

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

            migrationBuilder.AddColumn<string>(
                name: "Prescription",
                table: "Appointments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterNumber",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
