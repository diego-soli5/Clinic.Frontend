using Clinic.Domain.Models.Responses;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Clinic.Data.Abstractions;
using Clinic.Domain.Models.Responses.Api;

namespace Clinic.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<DefaultGetApiResponse<TData>> Get<TData>(string url, object dataToSend = null, string authToken = null)
        {
            DefaultGetApiResponse<TData> defaultGetApiResponse = new DefaultGetApiResponse<TData>();

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

                defaultGetApiResponse.StatusCode = (int)httpResponseMessage.StatusCode;

                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    defaultGetApiResponse.Success = true;

                    if (httpResponseMessage.Content != null)
                    {
                        string jsonOk = await httpResponseMessage.Content.ReadAsStringAsync();

                        var okResponse = JsonConvert.DeserializeObject<OkResponse>(jsonOk);

                        defaultGetApiResponse.Data = JsonConvert.DeserializeObject<TData>(okResponse.Data.ToString());
                    }

                    if (httpResponseMessage.Headers.Contains("X-Pagination"))
                    {
                        string jsonHeader = httpResponseMessage.Headers.GetValues("X-Pagination").FirstOrDefault();

                        defaultGetApiResponse.Metadata = JsonConvert.DeserializeObject<Metadata>(jsonHeader);
                    }
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    if (httpResponseMessage.Content != null)
                    {
                        string jsonNotFound = await httpResponseMessage.Content.ReadAsStringAsync();

                        var notFoundResponse = JsonConvert.DeserializeObject<NotFoundResponse>(jsonNotFound);

                        defaultGetApiResponse.Message = notFoundResponse.Message;
                    }
                }

                return defaultGetApiResponse;
            }
        }

        public async Task<DefaultPostApiResponse> Post(string url, object dataToSend = null, string authToken = null)
        {
            DefaultPostApiResponse defaultPostApiResponse = new DefaultPostApiResponse();

            var request = new HttpRequestMessage(HttpMethod.Post, url);

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

                defaultPostApiResponse.StatusCode = (int)httpResponseMessage.StatusCode;

                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    defaultPostApiResponse.Success = true;

                    if (httpResponseMessage.Content != null)
                    {
                        string jsonOkResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                        var okResponse = JsonConvert.DeserializeObject<OkResponse>(jsonOkResponse);

                        defaultPostApiResponse.Message = okResponse.Message;
                    }
                }


                return defaultPostApiResponse;
            }
        }

        public async Task<DefaultPutApiResponse> Put(string url, object dataToSend = null, string authToken = null)
        {
            DefaultPutApiResponse defaultPutApiResponse = new DefaultPutApiResponse();

            var request = new HttpRequestMessage(HttpMethod.Put, url);

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

                defaultPutApiResponse.StatusCode = (int)httpResponseMessage.StatusCode;

                if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    defaultPutApiResponse.Success = true;
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    string jsonBadRequest = await httpResponseMessage.Content.ReadAsStringAsync();

                    var badRequestResponse = JsonConvert.DeserializeObject<BadRequestResponse>(jsonBadRequest);

                    defaultPutApiResponse.Message = badRequestResponse.Message;
                }

                return defaultPutApiResponse;
            }
        }

        public async Task<DefaultPatchApiResponse> Patch(string url, object dataToSend = null, string authToken = null)
        {
            DefaultPatchApiResponse defaultPatchApiResponse = new DefaultPatchApiResponse();

            var request = new HttpRequestMessage(HttpMethod.Patch, url);

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

                defaultPatchApiResponse.StatusCode = (int)httpResponseMessage.StatusCode;

                if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    defaultPatchApiResponse.Success = true;
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    string jsonBadRequest = await httpResponseMessage.Content.ReadAsStringAsync();

                    var badRequestResponse = JsonConvert.DeserializeObject<BadRequestResponse>(jsonBadRequest);

                    defaultPatchApiResponse.Message = badRequestResponse.Message;
                }

                return defaultPatchApiResponse;
            }
        }
    }
}
