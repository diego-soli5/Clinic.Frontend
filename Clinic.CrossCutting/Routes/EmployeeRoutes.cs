using Clinic.CrossCutting.Options;
using Microsoft.Extensions.Options;

namespace Clinic.CrossCutting.Routes
{
    public class EmployeeRoutes
    {
        private readonly ApiOptions _apiOptions;

        public EmployeeRoutes(IOptions<ApiOptions> options)
        {
            _apiOptions = options.Value;

            Employee = $"{_apiOptions.Domain}/api/Employee";
        }

        public string Employee { get; private set; }

        public string Fire {
            get
            {
                return Employee + "/Fire/";
            }
        }

        public string Activate
        {
            get
            {
                return Employee + "/Activate/";
            }
        }
    }
}
