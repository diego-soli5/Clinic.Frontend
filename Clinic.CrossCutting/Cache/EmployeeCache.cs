using Clinic.Domain.QueryFilters;

namespace Clinic.CrossCutting.Cache
{
    public static class EmployeeCache
    {
        private static EmployeeQueryFilter EmployeeQueryFilterCache { get; set; }

        public static void GetEmployeeQueryFilterCache(EmployeeQueryFilter filters)
        {
            filters.EmployeeRole = filters.EmployeeRole ?? EmployeeQueryFilterCache.EmployeeRole;
            filters.EmployeeStatus = filters.EmployeeStatus ?? EmployeeQueryFilterCache.EmployeeStatus;
            filters.HireDate = filters.HireDate ?? EmployeeQueryFilterCache.HireDate;
            filters.Identification = filters.Identification ?? EmployeeQueryFilterCache.Identification;
        }

        public static void SetEmployeeQueryFilterCache(EmployeeQueryFilter filters)
        {
            EmployeeQueryFilterCache = filters;
        }
    }
}
