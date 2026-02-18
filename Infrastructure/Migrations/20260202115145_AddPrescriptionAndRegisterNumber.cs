using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescriptionAndRegisterNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AddColumn<string>(
            //     name: "Prescription",
            //     table: "Appointments",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "RegisterNumber",
            //     table: "Appointments",
            //     type: "text",
            //     nullable: false,
            //     defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "Prescription",
            //     table: "Appointments");

            // migrationBuilder.DropColumn(
            //     name: "RegisterNumber",
            //     table: "Appointments");
        }
    }
}
