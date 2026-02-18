using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStatusToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Employees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            // Convert numeric string values ("0", "1") to semantic names
            migrationBuilder.Sql(
                @"UPDATE ""Employees""
                  SET ""Status"" = CASE ""Status""
                      WHEN '0' THEN 'Inactive'
                      WHEN '1' THEN 'Active'
                      ELSE 'Active' -- Default to Active for unexpected values
                  END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert semantic names back to numeric string values ("0", "1") before converting type
            migrationBuilder.Sql(
                @"UPDATE ""Employees""
                  SET ""Status"" = CASE ""Status""
                      WHEN 'Inactive' THEN '0'
                      WHEN 'Active' THEN '1'
                      ELSE '1' -- Default back to Active (1) if unknown
                  END");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Employees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
