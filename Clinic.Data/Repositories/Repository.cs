using Clinic.CrossCutting.CustomExceptions;
using Clinic.Data.Abstractions;
using Clinic.Domain.Models.Responses;
using Clinic.Domain.Models.Responses.Api;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Data.Repositories
{
    public class Repository : IRepository
    {
        #region DEPENDENCIES
        private readonly IHttpClientFactory _clientFactory;
        #endregion

        #region CONSTRUCTOR
        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        #endregion

        #region GET
        public async Task<DefaultApiResponseResult<TData>> Get<TData>(string url, object dataToSend = null, string authToken = null)
        {
            DefaultApiResponseResult<TData> result = new();

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                if (dataToSend != null)
                    AddDataToRequest(request, dataToSend);

                using (var client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrEmpty(authToken))
                        AddBearerAuthorizationHeader(client, authToken);

                    using (var httpResponseMessage = await client.SendAsync(request))
                    {
                        ManageApiResponseMessage(result, httpResponseMessage);
                        GetApiResponseHeaders(result, httpResponseMessage);

                        return result;
                    }
                }
            }
        }

        public async Task<(byte[], string)> Get(string url, string authToken = null)
        {
            byte[] fileBytes = null;
            string contentType = null;

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (var client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrEmpty(authToken))
                        AddBearerAuthorizationHeader(client, authToken);

                    using (var httpResponseMessage = await client.SendAsync(request))
                    {
                        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                        {
                            if (httpResponseMessage.Content != null)
                            {
                                fileBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync();

                                if (httpResponseMessage.Content.Headers.Contains("Content-Type"))
                                {
                                    contentType = httpResponseMessage.Content.Headers.GetValues("Content-Type").FirstOrDefault();
                                }
                            }
                        }
                    }
                }
            }

            return (fileBytes, contentType);
        }
        #endregion

        #region POST
        public async Task<DefaultApiResponseResult> Post(string url, object dataToSend = null, string authToken = null)
        {
            DefaultApiResponseResult result = new();

            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                if (dataToSend != null)
                    AddDataToRequest(request, dataToSend);

                using (var client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrEmpty(authToken))
                        AddBearerAuthorizationHeader(client, authToken);

                    using (var httpResponseMessage = await client.SendAsync(request))
                    {
                        ManageApiResponseMessage(result, httpResponseMessage);
                        GetApiResponseHeaders(result, httpResponseMessage);

                        return result;
                    }
                }
            }
        }

        public async Task<DefaultApiResponseResult<TData>> Post<TData>(string url, object dataToSend = null, string authToken = null)
        {
            DefaultApiResponseResult<TData> result = new();

            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                if (dataToSend != null)
                    AddDataToRequest(request, dataToSend);

                using (var client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrEmpty(authToken))
                        AddBearerAuthorizationHeader(client, authToken);

                    using (var httpResponseMessage = await client.SendAsync(request))
                    {
                        ManageApiResponseMessage(result, httpResponseMessage);
                        GetApiResponseHeaders(result, httpResponseMessage);

                        return result;
                    }
                }
            }
        }

        public async Task<DefaultApiResponseResult> Post(string url, IFormFile[] filesToSend, string authToken = null)
        {
            DefaultApiResponseResult result = new();

            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                if (filesToSend != null)
                    AddDataToRequest(request, filesToSend);

                using (var client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrEmpty(authToken))
                        AddBearerAuthorizationHeader(client, authToken);

                    using (var httpResponseMessage = await client.SendAsync(request))
                    {
                        ManageApiResponseMessage(result, httpResponseMessage);
                        GetApiResponseHeaders(result, httpResponseMessage);

                        return result;
                    }
                }
            }
        }
        #endregion

        #region PUT
        public async Task<DefaultApiResponseResult> Put(string url, object dataToSend = null, string authToken = null)
        {
            DefaultApiResponseResult result = new();

            using (var request = new HttpRequestMessage(HttpMethod.Put, url))
            {
                if (dataToSend != null)
                    AddDataToRequest(request, dataToSend);

                using (var client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrEmpty(authToken))
                        AddBearerAuthorizationHeader(client, authToken);

                    using (var httpResponseMessage = await client.SendAsync(request))
                    {
                        ManageApiResponseMessage(result, httpResponseMessage);
                        GetApiResponseHeaders(result, httpResponseMessage);

                        return result;
                    }
                }
            }
        }
        #endregion

        #region PATCH
        public async Task<DefaultApiResponseResult> Patch(string url, object dataToSend = null, string authToken = null)
        {
            DefaultApiResponseResult result = new();

            using (var request = new HttpRequestMessage(HttpMethod.Patch, url))
            {
                if (dataToSend != null)
                    AddDataToRequest(request, dataToSend);

                using (var client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrEmpty(authToken))
                        AddBearerAuthorizationHeader(client, authToken);

                    using (var httpResponseMessage = await client.SendAsync(request))
                    {
                        ManageApiResponseMessage(result, httpResponseMessage);
                        GetApiResponseHeaders(result, httpResponseMessage);

                        return result;
                    }
                }
            }
        }
        #endregion

        #region DELETE
        public async Task<DefaultApiResponseResult> Delete(string url, object dataToSend = null, string authToken = null)
        {
            DefaultApiResponseResult result = new();

            using (var request = new HttpRequestMessage(HttpMethod.Delete, url))
            {
                if (dataToSend != null)
                    AddDataToRequest(request, dataToSend);

                using (var client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrEmpty(authToken))
                        AddBearerAuthorizationHeader(client, authToken);

                    using (var httpResponseMessage = await client.SendAsync(request))
                    {
                        ManageApiResponseMessage(result, httpResponseMessage);
                        GetApiResponseHeaders(result, httpResponseMessage);

                        return result;
                    }
                }
            }
        }
        #endregion

        #region UTILITY METHODS
        private void AddDataToRequest(HttpRequestMessage request, object dataToSend)
        {
            if (dataToSend == null)
                throw new ArgumentNullException(nameof(dataToSend), $"Unexpected null value at {nameof(AddDataToRequest)}.");

            if (dataToSend is IFormFile[])
            {
                var filesToSend = dataToSend as IFormFile[];

                var form = new MultipartFormDataContent();

                filesToSend.ToList().ForEach(file =>
                {
                    var streamContent = new StreamContent(file.OpenReadStream());

                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = file.Name, FileName = file.FileName };

                    form.Add(streamContent, "file", file.FileName);

                    request.Content = form;
                });
            }
            else
            {
                string json = JsonConvert.SerializeObject(dataToSend);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
        }

        private void AddBearerAuthorizationHeader(HttpClient client, string authToken)
        {
            if (string.IsNullOrEmpty(authToken))
                throw new ArgumentNullException(nameof(authToken), $"Unexpected null value at {nameof(AddBearerAuthorizationHeader)}.");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        #region ManageApiResponseMessage
        private async void ManageApiResponseMessage(DefaultApiResponseResult result, HttpResponseMessage httpResponseMessage)
        {
            result.StatusCode = (int)httpResponseMessage.StatusCode;

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                result.Success = true;

                if (httpResponseMessage.Content != null)
                {
                    string jsonOkResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    var okResponse = JsonConvert.DeserializeObject<OkResponse>(jsonOkResponse);

                    result.Message = okResponse.Message;
                }
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.Created)
            {
                result.Success = true;
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                result.Success = true;
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                if (httpResponseMessage.Content != null)
                {
                    string jsonNotFound = await httpResponseMessage.Content.ReadAsStringAsync();

                    var notFoundResponse = JsonConvert.DeserializeObject<NotFoundResponse>(jsonNotFound);

                    result.Message = notFoundResponse.Message;
                    result.NotFoundResourceId = notFoundResponse.NotFoundResourceId;
                }
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                string jsonBadRequest = await httpResponseMessage.Content.ReadAsStringAsync();

                var badRequestResponse = JsonConvert.DeserializeObject<BadRequestResponse>(jsonBadRequest);

                result.Message = badRequestResponse.Message;

                if (badRequestResponse.HasModelErrors)
                {
                    result.ModelErrors = badRequestResponse.ModelErrors;
                }
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {

            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                result.Message = "Ocurrió un error interno en el servidor.";
            }
            else
            {
                result.Message = $"El servidor respondió con un código de estado {result.StatusCode}.";
            }
        }

        private async void ManageApiResponseMessage<TData>(DefaultApiResponseResult<TData> result, HttpResponseMessage httpResponseMessage)
        {
            result.StatusCode = (int)httpResponseMessage.StatusCode;

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                result.Success = true;

                if (httpResponseMessage.Content != null)
                {
                    string jsonOkResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    var okResponse = JsonConvert.DeserializeObject<OkResponse>(jsonOkResponse);

                    result.Message = okResponse.Message;

                    if (okResponse.Data != null)
                    {
                        result.Data = JsonConvert.DeserializeObject<TData>(okResponse.Data.ToString());
                    }
                }
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.Created)
            {
                result.Success = true;
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                result.Success = true;
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                if (httpResponseMessage.Content != null)
                {
                    string jsonNotFound = await httpResponseMessage.Content.ReadAsStringAsync();

                    var notFoundResponse = JsonConvert.DeserializeObject<NotFoundResponse>(jsonNotFound);

                    result.Message = notFoundResponse.Message;
                    result.NotFoundResourceId = notFoundResponse.NotFoundResourceId;
                }
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                string jsonBadRequest = await httpResponseMessage.Content.ReadAsStringAsync();

                var badRequestResponse = JsonConvert.DeserializeObject<BadRequestResponse>(jsonBadRequest);

                result.Message = badRequestResponse.Message;

                if (badRequestResponse.HasModelErrors)
                {
                    result.ModelErrors = badRequestResponse.ModelErrors;
                }
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {

            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                result.Message = "Ocurrió un error interno en el servidor.";
            }
            else
            {
                result.Message = $"El servidor respondió con un código de estado {result.StatusCode}.";
            }
        }
        #endregion

        #region GetApiResponseHeaders
        private void GetApiResponseHeaders(DefaultApiResponseResult result, HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.Headers.Contains("X-Resource-Id"))
            {
                result.NewResourceId = int.Parse(httpResponseMessage.Headers.GetValues("X-Resource-Id").FirstOrDefault());
            }

            if (httpResponseMessage.Headers.Contains("X-Resource-Name"))
            {
                result.NewResourceName = httpResponseMessage.Headers.GetValues("X-Resource-Name").FirstOrDefault();
            }

            if (httpResponseMessage.Headers.Contains("X-Pagination"))
            {
                string jsonHeader = httpResponseMessage.Headers.GetValues("X-Pagination").FirstOrDefault();

                result.Metadata = JsonConvert.DeserializeObject<Metadata>(jsonHeader);
            }
        }

        private void GetApiResponseHeaders<TData>(DefaultApiResponseResult<TData> result, HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.Headers.Contains("X-Resource-Id"))
            {
                result.NewResourceId = int.Parse(httpResponseMessage.Headers.GetValues("X-Resource-Id").FirstOrDefault());
            }

            if (httpResponseMessage.Headers.Contains("X-Resource-Name"))
            {
                result.NewResourceName = httpResponseMessage.Headers.GetValues("X-Resource-Name").FirstOrDefault();
            }

            if (httpResponseMessage.Headers.Contains("X-Pagination"))
            {
                string jsonHeader = httpResponseMessage.Headers.GetValues("X-Pagination").FirstOrDefault();

                result.Metadata = JsonConvert.DeserializeObject<Metadata>(jsonHeader);
            }
        }
        #endregion

        #endregion
    }
}



