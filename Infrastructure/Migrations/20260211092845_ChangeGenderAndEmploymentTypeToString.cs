using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGenderAndEmploymentTypeToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Transform integer Gender values to strings by simply casting existing value
            migrationBuilder.Sql(
                @"UPDATE ""Employees""
                  SET ""Gender"" = ""Gender""::text");

            // Transform integer EmploymentType values to strings by simply casting existing value
            migrationBuilder.Sql(
                @"UPDATE ""Employees""
                  SET ""EmploymentType"" = ""EmploymentType""::text");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Employees",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "EmploymentType",
                table: "Employees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Employees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "EmploymentType",
                table: "Employees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
