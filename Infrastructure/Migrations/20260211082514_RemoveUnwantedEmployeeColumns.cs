using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnwantedEmployeeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add the new "Documents" column first
            migrationBuilder.AddColumn<string>(
                name: "Documents",
                table: "Employees",
                type: "text",
                nullable: true);

            // 2. Migrate existing binary/metadata data into the new "Documents" column as JSON
            migrationBuilder.Sql(
                @"UPDATE ""Employees""
                  SET ""Documents"" = json_build_object(
                      'IdProof', CASE WHEN ""IdProof"" IS NOT NULL THEN json_build_object(
                          'FileName', ""IdProofFileName"",
                          'ContentType', ""IdProofContentType"",
                          'FileContent', encode(""IdProof"", 'base64')
                      ) ELSE NULL END,
                      'Resume', CASE WHEN ""Resume"" IS NOT NULL THEN json_build_object(
                          'FileName', ""ResumeFileName"",
                          'ContentType', ""ResumeContentType"",
                          'FileContent', encode(""Resume"", 'base64')
                      ) ELSE NULL END,
                      'OfferLetter', CASE WHEN ""OfferLetter"" IS NOT NULL THEN json_build_object(
                          'FileName', ""OfferLetterFileName"",
                          'ContentType', ""OfferLetterContentType"",
                          'FileContent', encode(""OfferLetter"", 'base64')
                      ) ELSE NULL END
                  )::text
                  WHERE ""IdProof"" IS NOT NULL OR ""Resume"" IS NOT NULL OR ""OfferLetter"" IS NOT NULL;");

            // 3. Drop the old columns
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdProof",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdProofContentType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdProofFileName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OfferLetter",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OfferLetterContentType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OfferLetterFileName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ResumeContentType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ResumeFileName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Recreate the original columns
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "IdProof",
                table: "Employees",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdProofContentType",
                table: "Employees",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdProofFileName",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "OfferLetter",
                table: "Employees",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferLetterContentType",
                table: "Employees",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferLetterFileName",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Resume",
                table: "Employees",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResumeContentType",
                table: "Employees",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResumeFileName",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            // 2. Restore data from "Documents" back to original columns
            migrationBuilder.Sql(
                @"UPDATE ""Employees""
                  SET
                      ""IdProof"" = decode((""Documents""::json -> 'IdProof' ->> 'FileContent'), 'base64'),
                      ""IdProofFileName"" = ""Documents""::json -> 'IdProof' ->> 'FileName',
                      ""IdProofContentType"" = ""Documents""::json -> 'IdProof' ->> 'ContentType',

                      ""Resume"" = decode((""Documents""::json -> 'Resume' ->> 'FileContent'), 'base64'),
                      ""ResumeFileName"" = ""Documents""::json -> 'Resume' ->> 'FileName',
                      ""ResumeContentType"" = ""Documents""::json -> 'Resume' ->> 'ContentType',

                      ""OfferLetter"" = decode((""Documents""::json -> 'OfferLetter' ->> 'FileContent'), 'base64'),
                      ""OfferLetterFileName"" = ""Documents""::json -> 'OfferLetter' ->> 'FileName',
                      ""OfferLetterContentType"" = ""Documents""::json -> 'OfferLetter' ->> 'ContentType'
                  WHERE ""Documents"" IS NOT NULL;");

            // 3. Drop the "Documents" column
            migrationBuilder.DropColumn(
                name: "Documents",
                table: "Employees");
        }
    }
}
