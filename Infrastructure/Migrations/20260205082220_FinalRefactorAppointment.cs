using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR_Application.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalRefactorAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop problematic columns from LabOperators if they exist (using raw SQL to avoid error if they don't)
            migrationBuilder.Sql("ALTER TABLE \"LabOperators\" DROP COLUMN IF EXISTS \"HealthStatus\";");
            migrationBuilder.Sql("ALTER TABLE \"LabOperators\" DROP COLUMN IF EXISTS \"Supervisor\";");

            // Recreate Appointments table
            migrationBuilder.Sql("DROP TABLE IF EXISTS \"Appointments\" CASCADE;");

            migrationBuilder.Sql(@"
                CREATE TABLE ""Appointments"" (
                    ""Id"" uuid NOT NULL,
                    ""Patient Id"" uuid NOT NULL,
                    ""Doctor Id"" uuid NULL,
                    ""Date & Time"" timestamp with time zone NOT NULL,
                    ""Appointment type"" text NOT NULL,
                    ""Status"" text NOT NULL,
                    ""Token no:"" text NOT NULL,
                    ""Prescription"" text NULL,
                    ""Reschedule"" text NULL,
                    ""Concellation"" text NULL,
                    CONSTRAINT ""PK_Appointments"" PRIMARY KEY (""Id""),
                    CONSTRAINT ""FK_Appointments_Doctors_Doctor Id"" FOREIGN KEY (""Doctor Id"") REFERENCES ""Doctors"" (""Id""),
                    CONSTRAINT ""FK_Appointments_Patients_Patient Id"" FOREIGN KEY (""Patient Id"") REFERENCES ""Patients"" (""Id"") ON DELETE CASCADE
                );
            ");

            migrationBuilder.Sql("CREATE INDEX \"IX_Appointments_Doctor Id\" ON \"Appointments\" (\"Doctor Id\");");
            migrationBuilder.Sql("CREATE INDEX \"IX_Appointments_Patient Id\" ON \"Appointments\" (\"Patient Id\");");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS \"Appointments\" CASCADE;");
        }
    }
}
