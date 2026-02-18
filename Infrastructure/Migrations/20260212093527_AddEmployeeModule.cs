using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Employees table
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Designation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    JoiningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmploymentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BasicPay = table.Column<decimal>(type: "numeric", nullable: false),
                    Allowances = table.Column<decimal>(type: "numeric", nullable: true),
                    Deductions = table.Column<decimal>(type: "numeric", nullable: true),
                    PfTaxInfo = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OfferLetter = table.Column<string>(type: "text", nullable: true),
                    Resume = table.Column<string>(type: "text", nullable: true),
                    IdProof = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            // Create a sequence for Employee IDs
            // Starting at 1001 to avoid conflicts with early test data if any
            migrationBuilder.Sql("CREATE SEQUENCE \"EmployeeIdSequence\" START WITH 1001 INCREMENT BY 1;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the Employee ID sequence
            migrationBuilder.Sql("DROP SEQUENCE IF EXISTS \"EmployeeIdSequence\";");

            // Drop Employees table
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
