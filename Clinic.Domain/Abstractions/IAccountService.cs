using Clinic.Domain.Models.DTOs.Account;
using Clinic.Domain.Models.DTOs.Person;
using Clinic.Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IAccountService
    {
        Task<DefaultApiResponseResult<LoginResultDTO>> TryAuthenticateAsync(string emailOrIdentification, string password);
        Task<PersonDTO> GetCurrentUser(int id, string authToken);
        Task<DefaultApiResponseResult> ChangeImage(IFormFile image, int id,string authToken);
    }
}
