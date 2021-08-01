using Clinic.CrossCutting.Options;
using Microsoft.Extensions.Options;

namespace Clinic.CrossCutting.Routes
{
    public class MedicRoutes
    {
        private readonly ApiOptions _apiOptions;

        public MedicRoutes(IOptions<ApiOptions> options)
        {
            _apiOptions = options.Value;

            Medic = $"{_apiOptions.Domain}/api/{nameof(Medic)}";
        }

        public string Medic { get; private set; }

        public string Edit
        {
            get
            {
                return $"{Medic}/{nameof(Edit)}/";
            }
        }

        public string Pending
        {
            get
            {
                return $"{Medic}/{nameof(Pending)}/";
            }
        }
    }
}

