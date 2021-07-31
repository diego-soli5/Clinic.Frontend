using Clinic.CrossCutting.Options;
using Microsoft.Extensions.Options;

namespace Clinic.CrossCutting.Routes
{
    public class ResourceRoutes
    {
        private readonly ApiOptions _apiOptions;

        public ResourceRoutes(IOptions<ApiOptions> options)
        {
            _apiOptions = options.Value;

            Resource = $"{_apiOptions.Domain}/api/{nameof(Resource)}";
        }

        public string Resource { get; private set; }
        public string GetEntityImgById { get { return $"{Resource}/"; } }

    }
}
