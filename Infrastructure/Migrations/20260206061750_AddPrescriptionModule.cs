using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescriptionModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PrescriptionCode = table.Column<string>(name: "Prescription Code", type: "text", nullable: false),
                    PatientId = table.Column<Guid>(name: "Patient Id", type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(name: "Doctor Id", type: "uuid", nullable: false),
                    Diagnosis = table.Column<string>(type: "text", nullable: false),
                    IssueDate = table.Column<DateTime>(name: "Issue Date", type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    AdditionalInstructions = table.Column<string>(name: "Additional Instructions", type: "text", nullable: true),
                    CancellationReason = table.Column<string>(name: "Cancellation Reason", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_Doctor Id",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_Patient Id",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionMedications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PrescriptionId = table.Column<Guid>(name: "Prescription Id", type: "uuid", nullable: false),
                    MedicineName = table.Column<string>(name: "Medicine Name", type: "text", nullable: false),
                    Dosage = table.Column<string>(type: "text", nullable: false),
                    Frequency = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedications_Prescriptions_Prescription Id",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedications_Prescription Id",
                table: "PrescriptionMedications",
                column: "Prescription Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Doctor Id",
                table: "Prescriptions",
                column: "Doctor Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Patient Id",
                table: "Prescriptions",
                column: "Patient Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionMedications");

            migrationBuilder.DropTable(
                name: "Prescriptions");
        }
    }
}
