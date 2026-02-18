using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelsToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Manual SQL to handle type conversion specifically for PostgreSQL
            migrationBuilder.Sql("ALTER TABLE \"Prescriptions\" ALTER COLUMN \"Issue Date\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"Prescriptions\" ALTER COLUMN \"Issue Date\" TYPE integer USING extract(epoch from \"Issue Date\")::integer;");
            
            migrationBuilder.Sql("ALTER TABLE \"Patients\" ALTER COLUMN \"Pincode\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"Patients\" ALTER COLUMN \"Pincode\" TYPE integer USING (CASE WHEN \"Pincode\" ~ '^[0-9]+$' THEN \"Pincode\"::integer ELSE 0 END);");
            
            migrationBuilder.Sql("ALTER TABLE \"Patients\" ALTER COLUMN \"PhoneNumber\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"Patients\" ALTER COLUMN \"PhoneNumber\" TYPE bigint USING (CASE WHEN \"PhoneNumber\" ~ '^[0-9]+$' THEN \"PhoneNumber\"::bigint ELSE 0 END);");
            
            migrationBuilder.Sql("ALTER TABLE \"InsuranceClaims\" ALTER COLUMN \"PolicyNumber\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"InsuranceClaims\" ALTER COLUMN \"PolicyNumber\" TYPE integer USING (CASE WHEN \"PolicyNumber\" ~ '^[0-9]+$' THEN \"PolicyNumber\"::integer ELSE 0 END);");
            
            migrationBuilder.Sql("ALTER TABLE \"Doctors\" ALTER COLUMN \"Pincode\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"Doctors\" ALTER COLUMN \"Pincode\" TYPE integer USING (CASE WHEN \"Pincode\" ~ '^[0-9]+$' THEN \"Pincode\"::integer ELSE NULL END);");
            
            migrationBuilder.Sql("ALTER TABLE \"Doctors\" ALTER COLUMN \"PhoneNumber\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"Doctors\" ALTER COLUMN \"PhoneNumber\" TYPE bigint USING (CASE WHEN \"PhoneNumber\" ~ '^[0-9]+$' THEN \"PhoneNumber\"::bigint ELSE NULL END);");
            
            migrationBuilder.Sql("ALTER TABLE \"Doctors\" ALTER COLUMN \"PF\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"Doctors\" ALTER COLUMN \"PF\" TYPE integer USING (CASE WHEN \"PF\" ~ '^[0-9]+$' THEN \"PF\"::integer ELSE NULL END);");
            
            migrationBuilder.Sql("ALTER TABLE \"Doctors\" ALTER COLUMN \"OfficeNumber\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"Doctors\" ALTER COLUMN \"OfficeNumber\" TYPE bigint USING (CASE WHEN \"OfficeNumber\" ~ '^[0-9]+$' THEN \"OfficeNumber\"::bigint ELSE NULL END);");
            
            migrationBuilder.Sql("ALTER TABLE \"Appointments\" ALTER COLUMN \"Token no:\" DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE \"Appointments\" ALTER COLUMN \"Token no:\" TYPE integer USING (CASE WHEN \"Token no:\" ~ '^[0-9]+$' THEN \"Token no:\"::integer ELSE 0 END);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Issue Date",
                table: "Prescriptions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Pincode",
                table: "Patients",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Patients",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "PolicyNumber",
                table: "InsuranceClaims",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Pincode",
                table: "Doctors",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Doctors",
                type: "text",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PF",
                table: "Doctors",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OfficeNumber",
                table: "Doctors",
                type: "text",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Token no:",
                table: "Appointments",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
