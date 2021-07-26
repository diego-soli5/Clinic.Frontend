using Clinic.CrossCutting.Abstractions;
using Clinic.CrossCutting.Routes;
using Clinic.Data.Abstractions;
using Clinic.Domain.Abstractions;
using System.Threading.Tasks;

namespace Clinic.Domain.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IRepository _repository;
        private readonly ApiRoutes _routes;
        private readonly IUriGenerator _uriGenerator;

        public ResourceService(IRepository repository,
                               ApiRoutes routes,
                               IUriGenerator uriGenerator)
        {
            _repository = repository;
            _routes = routes;
            _uriGenerator = uriGenerator;
        }

        public async Task<(byte[], string)> GetProfileImage(string n, string authToken)
        {
            string url = _uriGenerator.AddQueryStringParams(_routes.ResourceRoutes.Resource, new { n, type = "img" }).ToString();

            var image = await _repository.Get(url, authToken);

            return image;
        }
    }
}
