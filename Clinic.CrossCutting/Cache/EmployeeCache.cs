using Clinic.Domain.QueryFilters;

namespace Clinic.CrossCutting.Cache
{
    public static class EmployeeCache
    {
        private static EmployeeQueryFilter EmployeeQueryFilterCache { get; set; }

        public static void GetEmployeeQueryFilterCache(EmployeeQueryFilter filters)
        {
            if (filters.IsPagination)
            {
                filters.EmployeeRole ??= EmployeeQueryFilterCache?.EmployeeRole;
                filters.EmployeeStatus ??= EmployeeQueryFilterCache?.EmployeeStatus;
                filters.HireDate ??= EmployeeQueryFilterCache?.HireDate;
                filters.Identification ??= EmployeeQueryFilterCache?.Identification;
            }
        }

        public static void SetEmployeeQueryFilterCache(EmployeeQueryFilter filters)
        {
            EmployeeQueryFilterCache = filters;
        }
    }
}
