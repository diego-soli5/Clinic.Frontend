using Clinic.CrossCutting.Routes;
using Clinic.Data.Abstractions;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Models.DTOs.Account;
using Clinic.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
