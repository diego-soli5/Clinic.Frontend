using Clinic.Domain.Models.Responses;
using System.Threading.Tasks;

namespace Clinic.Data.Abstractions
{
    public interface IRepository
    {
        Task<DefaultGetApiResponse<TData>> Get<TData>(string url, object dataToSend = null, string authToken = null);
        Task<DefaultPostApiResponse> Post(string url,object dataToSend = null, string authToken = null);
        Task<DataPostApiResponse<TData>> Post<TData>(string url,object dataToSend = null, string authToken = null);
        Task<DefaultPutApiResponse> Put(string url, object dataToSend = null, string authToken = null);
        Task<DefaultDeleteApiResponse> Delete(string url, object dataToSend = null, string authToken = null);
        Task<DefaultPatchApiResponse> Patch(string url, object dataToSend = null, string authToken = null);
    }
}
