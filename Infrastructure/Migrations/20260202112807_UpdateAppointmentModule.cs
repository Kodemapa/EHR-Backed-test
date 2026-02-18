using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Appointments_Doctors_DoctorId",
            //     table: "Appointments");

            // migrationBuilder.DropIndex(
            //     name: "IX_Appointments_DoctorId",
            //     table: "Appointments");

            // migrationBuilder.DropColumn(
            //     name: "DoctorId",
            //     table: "Appointments");

            // migrationBuilder.RenameColumn(
            //     name: "Notes",
            //     table: "Appointments",
            //     newName: "AppointmentType");

            // migrationBuilder.AddColumn<string>(
            //     name: "AppointmentStatus",
            //     table: "Appointments",
            //     type: "text",
            //     nullable: false,
            //     defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentStatus",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentType",
                table: "Appointments",
                newName: "Notes");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Appointments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
