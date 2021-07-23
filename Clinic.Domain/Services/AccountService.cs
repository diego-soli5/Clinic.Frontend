using Clinic.CrossCutting.CustomExceptions;
using Clinic.CrossCutting.Routes;
using Clinic.Data.Abstractions;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Account;
using Clinic.Domain.Models.DTOs.Person;
using Clinic.Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Clinic.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository _repository;
        private readonly AccountRoutes _routes;

        public AccountService(IRepository repository,
                              AccountRoutes routes)
        {
            _repository = repository;
            _routes = routes;
        }

        public async Task<DataPostApiResponse<LoginResultDTO>> TryAuthenticateAsync(string emailOrIdentification, string password)
        {
            string url = _routes.Authenticate;

            return await _repository.Post<LoginResultDTO>(url, new { emailOrIdentification, password });
        }

        public async Task<PersonDTO> GetCurrentUser(int id, string authToken)
        {
            string url = $"{_routes.GetCurrentUser}{id}";

            var apiResponse = await _repository.Get<PersonDTO>(url, authToken: authToken);

            if (apiResponse.StatusCode == StatusCodes.Status404NotFound)
            {
                throw new NotFoundException(apiResponse.Message, id);
            }

            return apiResponse.Data;
        }

        public async Task<DefaultPostApiResponse> ChangeImage(IFormFile image, int id, string authToken)
        {
            string url = $"{_routes.ChangeImage}{id}";

            return await _repository.PostFile(url, image, authToken);
        }
    }
}
