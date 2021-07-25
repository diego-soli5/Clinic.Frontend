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
    public class AccountService : ServiceMiddleware, IAccountService
    {
        private readonly IRepository _repository;
        private readonly AccountRoutes _routes;

        public AccountService(IRepository repository,
                              AccountRoutes routes)
        {
            _repository = repository;
            _routes = routes;
        }

        public async Task<DefaultApiResponseResult<LoginResultDTO>> TryAuthenticateAsync(string emailOrIdentification, string password)
        {
            string url = _routes.Authenticate;

            var response = await _repository.Post<LoginResultDTO>(url, new { emailOrIdentification, password });

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult<PersonDTO>> GetCurrentUser(int id, string authToken)
        {
            string url = $"{_routes.GetCurrentUser}{id}";

            var response = await _repository.Get<PersonDTO>(url, authToken: authToken);

            ValidateResponse(response);

            return response;
        }

        public async Task<DefaultApiResponseResult> ChangeImage(IFormFile image, int id, string authToken)
        {
            string url = $"{_routes.ChangeImage}{id}";

            var response = await _repository.Post(url, new[] { image }, authToken);

            ValidateResponse(response);

            return response;
        }
    }
}
