using Microsoft.AspNetCore.OData.Query;

namespace EHR_Application.ODataFilters
{
    public static class ODataQueryService
    {
        // Define the OData Query Options for Patients
        public const AllowedQueryOptions PatientQueryOptions = 
            AllowedQueryOptions.Filter |
            AllowedQueryOptions.OrderBy |
            AllowedQueryOptions.Select |
            AllowedQueryOptions.Expand |
            AllowedQueryOptions.Count |
            AllowedQueryOptions.Skip |
            AllowedQueryOptions.Top |
            AllowedQueryOptions.Search;

        // Define the OData Query Options for Doctors
        public const AllowedQueryOptions DoctorQueryOptions = 
            AllowedQueryOptions.Filter |
            AllowedQueryOptions.OrderBy |
            AllowedQueryOptions.Select |
            AllowedQueryOptions.Expand |
            AllowedQueryOptions.Count |
            AllowedQueryOptions.Skip |
            AllowedQueryOptions.Top |
            AllowedQueryOptions.Search;

        // Define the OData Query Options for Appointments
        public const AllowedQueryOptions AppointmentQueryOptions = 
            AllowedQueryOptions.Filter |
            AllowedQueryOptions.OrderBy |
            AllowedQueryOptions.Select |
            AllowedQueryOptions.Expand |
            AllowedQueryOptions.Count |
            AllowedQueryOptions.Skip |
            AllowedQueryOptions.Top |
            AllowedQueryOptions.Search;

        // Define the OData Query Options for Lab Operators
        public const AllowedQueryOptions LabOperatorQueryOptions = 
            AllowedQueryOptions.Filter |
            AllowedQueryOptions.OrderBy |
            AllowedQueryOptions.Select |
            AllowedQueryOptions.Expand |
            AllowedQueryOptions.Count |
            AllowedQueryOptions.Skip |
            AllowedQueryOptions.Top |
            AllowedQueryOptions.Search;

        // Define the OData Query Options for Prescriptions
        public const AllowedQueryOptions PrescriptionQueryOptions = 
            AllowedQueryOptions.Filter |
            AllowedQueryOptions.OrderBy |
            AllowedQueryOptions.Select |
            AllowedQueryOptions.Expand |
            AllowedQueryOptions.Count |
            AllowedQueryOptions.Skip |
            AllowedQueryOptions.Top |
            AllowedQueryOptions.Search;

        // Define the OData Query Options for Insurance Claims
        public const AllowedQueryOptions InsuranceQueryOptions = 
            AllowedQueryOptions.Filter |
            AllowedQueryOptions.OrderBy |
            AllowedQueryOptions.Select |
            AllowedQueryOptions.Expand |
            AllowedQueryOptions.Count |
            AllowedQueryOptions.Skip |
            AllowedQueryOptions.Top |
            AllowedQueryOptions.Search;

        // Define the OData Query Options for Employees
        public const AllowedQueryOptions EmployeeQueryOptions = 
            AllowedQueryOptions.Filter |
            AllowedQueryOptions.OrderBy |
            AllowedQueryOptions.Select |
            AllowedQueryOptions.Expand |
            AllowedQueryOptions.Count |
            AllowedQueryOptions.Skip |
            AllowedQueryOptions.Top |
            AllowedQueryOptions.Search;
    }
}
