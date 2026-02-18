using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropExtraAppointmentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Patients\" DROP COLUMN IF EXISTS \"Appointment\";");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
