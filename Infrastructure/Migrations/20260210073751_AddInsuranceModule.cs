using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInsuranceModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsuranceClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimId = table.Column<string>(type: "text", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderName = table.Column<string>(type: "text", nullable: false),
                    PolicyNumber = table.Column<string>(type: "text", nullable: false),
                    TreatmentType = table.Column<string>(type: "text", nullable: false),
                    NetworkHospital = table.Column<string>(type: "text", nullable: false),
                    ClaimedAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CoveragePercentage = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_PatientId",
                table: "InsuranceClaims",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceClaims");
        }
    }
}
