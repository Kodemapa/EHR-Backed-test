using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorIdBackToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AddColumn<Guid>(
            //     name: "DoctorId",
            //     table: "Appointments",
            //     type: "uuid",
            //     nullable: true);

            // migrationBuilder.CreateIndex(
            //     name: "IX_Appointments_DoctorId",
            //     table: "Appointments",
            //     column: "DoctorId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Appointments_Doctors_DoctorId",
            //     table: "Appointments",
            //     column: "DoctorId",
            //     principalTable: "Doctors",
            //     principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Appointments");
        }
    }
}
