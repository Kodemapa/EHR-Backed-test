using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddLabOperators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "LabOperators",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uuid", nullable: false),
            //         FirstName = table.Column<string>(type: "text", nullable: false),
            //         LastName = table.Column<string>(type: "text", nullable: false),
            //         Contact = table.Column<string>(type: "text", nullable: false),
            //         OperatorId = table.Column<string>(type: "text", nullable: false),
            //         Role = table.Column<string>(type: "text", nullable: false),
            //         AssignedLab = table.Column<string>(type: "text", nullable: false),
            //         WorkSchedule = table.Column<string>(type: "text", nullable: false),
            //         LicenseNumber = table.Column<string>(type: "text", nullable: false),
            //         Qualification = table.Column<string>(type: "text", nullable: false),
            //         Address = table.Column<string>(type: "text", nullable: false),
            //         Status = table.Column<string>(type: "text", nullable: false),
            //         OperatingHours = table.Column<string>(type: "text", nullable: false),
            //         Experience = table.Column<string>(type: "text", nullable: false),
            //         HealthStatus = table.Column<string>(type: "text", nullable: false),
            //         AvailabilityStatus = table.Column<string>(type: "text", nullable: false),
            //         Supervisor = table.Column<string>(type: "text", nullable: false),
            //         Training = table.Column<string>(type: "text", nullable: false),
            //         Certification = table.Column<string>(type: "text", nullable: false),
            //         Tasks = table.Column<string>(type: "text", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_LabOperators", x => x.Id);
            //     });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabOperators");
        }
    }
}
