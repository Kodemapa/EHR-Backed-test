using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Migrations
{
    /// <inheritdoc />
    public partial class FixDoctorIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "Specialty",
            //     table: "Doctors");

            // migrationBuilder.AlterColumn<string>(
            //     name: "LastName",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text");

            // migrationBuilder.AlterColumn<string>(
            //     name: "FirstName",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text");

            // migrationBuilder.AddColumn<string>(
            //     name: "Email",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<bool>(
            //     name: "IsActive",
            //     table: "Doctors",
            //     type: "boolean",
            //     nullable: false,
            //     defaultValue: false);

            // migrationBuilder.AddColumn<string>(
            //     name: "PhoneNumber",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Specialization",
            //     table: "Doctors",
            //     type: "text",
            //     nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Doctors");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Doctors",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Doctors",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Doctors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
