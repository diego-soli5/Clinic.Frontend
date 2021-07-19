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
using Clinic.CrossCutting.CustomExceptions;

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
                else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    string jsonBadRequest = await httpResponseMessage.Content.ReadAsStringAsync();

                    var badRequestResponse = JsonConvert.DeserializeObject<BadRequestResponse>(jsonBadRequest);

                    defaultGetApiResponse.Message = badRequestResponse.Message;
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    defaultGetApiResponse.Message = "Ocurrió un error interno en el servidor.";
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException();
                }
                else
                {
                    defaultGetApiResponse.Message = $"El servidor respondió con un código de estado {defaultGetApiResponse.StatusCode}.";
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
                else if(httpResponseMessage.StatusCode == HttpStatusCode.Created)
                {
                    defaultPostApiResponse.Success = true;
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    string jsonBadRequest = await httpResponseMessage.Content.ReadAsStringAsync();

                    var badRequestResponse = JsonConvert.DeserializeObject<BadRequestResponse>(jsonBadRequest);

                    defaultPostApiResponse.Message = badRequestResponse.Message;
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //string jsonUnauthorized = await httpResponseMessage.Content.ReadAsStringAsync();
                    //var unauthorizedResponse = JsonConvert.DeserializeObject<UnauthorizedResponse>(jsonUnauthorized);
                    //defaultPostApiResponse.Message = unauthorizedResponse.Message;

                    throw new UnauthorizedException();
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    defaultPostApiResponse.Message = "Ocurrió un error interno en el servidor.";
                }
                else
                {
                    defaultPostApiResponse.Message = $"El servidor respondió con un código de estado {defaultPostApiResponse.StatusCode}.";
                }

                return defaultPostApiResponse;
            }
        }

        public async Task<DataPostApiResponse<TData>> Post<TData>(string url, object dataToSend = null, string authToken = null)
        {
            DataPostApiResponse<TData> dataPostApiResponse = new DataPostApiResponse<TData>();

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

                dataPostApiResponse.StatusCode = (int)httpResponseMessage.StatusCode;

                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    dataPostApiResponse.Success = true;

                    if (httpResponseMessage.Content != null)
                    {
                        string jsonOkResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                        var okResponse = JsonConvert.DeserializeObject<OkResponse>(jsonOkResponse);

                        if(okResponse.Data != null)
                            dataPostApiResponse.Data = JsonConvert.DeserializeObject<TData>(okResponse.Data.ToString());

                        dataPostApiResponse.Message = okResponse.Message;
                    }
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Created)
                {
                    dataPostApiResponse.Success = true;
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    string jsonBadRequest = await httpResponseMessage.Content.ReadAsStringAsync();

                    var badRequestResponse = JsonConvert.DeserializeObject<BadRequestResponse>(jsonBadRequest);

                    dataPostApiResponse.Message = badRequestResponse.Message;
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //string jsonUnauthorized = await httpResponseMessage.Content.ReadAsStringAsync();
                    //var unauthorizedResponse = JsonConvert.DeserializeObject<UnauthorizedResponse>(jsonUnauthorized);
                    //dataPostApiResponse.Message = unauthorizedResponse.Message;

                    throw new UnauthorizedException();
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    dataPostApiResponse.Message = "Ocurrió un error interno en el servidor.";
                }
                else
                {
                    dataPostApiResponse.Message = $"El servidor respondió con un código de estado {dataPostApiResponse.StatusCode}.";
                }

                return dataPostApiResponse;
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
                else if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    defaultPutApiResponse.Message = "Ocurrió un error interno en el servidor.";
                }
                else if(httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException();
                }
                else
                {
                    defaultPutApiResponse.Message = $"El servidor respondió con un código de estado {defaultPutApiResponse.StatusCode}.";
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
                else if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    defaultPatchApiResponse.Message = "Ocurrió un error interno en el servidor.";
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException();
                }
                else
                {
                    defaultPatchApiResponse.Message = $"El servidor respondió con un código de estado {defaultPatchApiResponse.StatusCode}.";
                }

                return defaultPatchApiResponse;
            }
        }

        public async Task<DefaultDeleteApiResponse> Delete(string url, object dataToSend = null, string authToken = null)
        {
            DefaultDeleteApiResponse defaultDeleteApiResponse = new DefaultDeleteApiResponse();

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

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

                defaultDeleteApiResponse.StatusCode = (int)httpResponseMessage.StatusCode;

                if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                {
                    defaultDeleteApiResponse.Success = true;
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    string jsonBadRequest = await httpResponseMessage.Content.ReadAsStringAsync();

                    var badRequestResponse = JsonConvert.DeserializeObject<BadRequestResponse>(jsonBadRequest);

                    defaultDeleteApiResponse.Message = badRequestResponse.Message;
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    defaultDeleteApiResponse.Message = "Ocurrió un error interno en el servidor.";
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException();
                }
                else
                {
                    defaultDeleteApiResponse.Message = $"El servidor respondió con un código de estado {defaultDeleteApiResponse.StatusCode}.";
                }

                return defaultDeleteApiResponse;
            }
        }
    }
}
