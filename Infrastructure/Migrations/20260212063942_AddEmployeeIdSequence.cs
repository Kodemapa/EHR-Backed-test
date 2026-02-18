using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeIdSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create a sequence for Employee IDs
            // Starting at 1001 to avoid conflicts with early test data if any
            migrationBuilder.Sql("CREATE SEQUENCE \"EmployeeIdSequence\" START WITH 1001 INCREMENT BY 1;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP SEQUENCE IF EXISTS \"EmployeeIdSequence\";");
        }
    }
}
