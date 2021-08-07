using Clinic.Domain.Models.DTOs.Account;
using Clinic.Domain.Models.DTOs.Person;
using Clinic.Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IAccountService
    {
        Task<DefaultResponseResult<LoginResultDTO>> TryAuthenticateAsync(string emailOrIdentification, string password);
        Task<DefaultResponseResult<PersonDTO>> GetCurrentUser(int id, string authToken);
        Task<DefaultResponseResult> ChangeImage(IFormFile image, int id,string authToken);
    }
}
