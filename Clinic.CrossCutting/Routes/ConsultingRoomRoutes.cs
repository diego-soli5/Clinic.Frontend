using Clinic.CrossCutting.Options;
using Microsoft.Extensions.Options;

namespace Clinic.CrossCutting.Routes
{
    public class ConsultingRoomRoutes
    {
        private readonly ApiOptions _apiOptions;

        public ConsultingRoomRoutes(IOptions<ApiOptions> options)
        {
            _apiOptions = options.Value;

            ConsultingRoom = $"{_apiOptions.Domain}/api/{nameof(ConsultingRoom)}";
        }

        public string ConsultingRoom { get; private set; }
    }
}
