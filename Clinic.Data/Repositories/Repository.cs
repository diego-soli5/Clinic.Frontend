using Clinic.Domain.Models.DTOs.Api;
using Clinic.Domain.Models.Responses;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Clinic.Data.Abstractions;

namespace Clinic.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ApiReponseResultDTO<TData>> Get<TData>(string url, object dataToSend = null, string authToken = null)
        {
            ApiReponseResultDTO<TData> apiResponseResultDTO = new ApiReponseResultDTO<TData>();

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (dataToSend != null)
            {
                string json = JsonConvert.SerializeObject(dataToSend);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            using (var client = _clientFactory.CreateClient())
            {
                if (!string.IsNullOrEmpty(authToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                }

                var httpResponseMessage = await client.SendAsync(request);

                apiResponseResultDTO.StatusCode = (int)httpResponseMessage.StatusCode;

                if(httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    apiResponseResultDTO.Success = true;

                    if(httpResponseMessage.Content != null)
                    {
                        string jsonOkResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                        var okResponse = JsonConvert.DeserializeObject<OkResponse>(jsonOkResponse);

                        apiResponseResultDTO.Data = JsonConvert.DeserializeObject<TData>(okResponse.Data.ToString());
                    }

                    if (httpResponseMessage.Headers.Contains("X-Pagination"))
                    {
                        string jsonHeader = httpResponseMessage.Headers.GetValues("X-Pagination").FirstOrDefault();

                        apiResponseResultDTO.Metadata = JsonConvert.DeserializeObject<Metadata>(jsonHeader);
                    }
                }

                return apiResponseResultDTO;
            }
        }
    }
}
