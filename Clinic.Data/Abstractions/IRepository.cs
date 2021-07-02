using Clinic.Domain.Models.DTOs.Api;
using System.Threading.Tasks;

namespace Clinic.Data.Abstractions
{
    public interface IRepository
    {
        Task<ApiReponseResultDTO<TData>> Get<TData>(string url, object dataToSend = null, string authToken = null);
    }
}
