using Clinic.CrossCutting.Options;
using Microsoft.Extensions.Options;

namespace Clinic.CrossCutting.Routes
{
    public class ApiRoutes
    {
        private readonly ApiOptions _apiOptions;

        public ApiRoutes(IOptions<ApiOptions> options)
        {
            _apiOptions = options.Value;

            Employee = $"{_apiOptions.Domain}/api/Employee";
        }

        public string Employee { get; private set; }
    }
}
