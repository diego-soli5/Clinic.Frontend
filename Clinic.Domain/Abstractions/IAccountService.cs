using Clinic.Domain.Models.DTOs.Account;
using Clinic.Domain.Models.Responses;
using System.Threading.Tasks;

namespace Clinic.Domain.Abstractions
{
    public interface IAccountService
    {
        Task<DataPostApiResponse<LoginResultDTO>> TryAuthenticateAsync(string emailOrIdentification, string password);
    }
}
