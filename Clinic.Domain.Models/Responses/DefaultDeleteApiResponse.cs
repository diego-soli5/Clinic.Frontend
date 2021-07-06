namespace Clinic.Domain.Models.Responses
{
    public class DefaultDeleteApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public DefaultDeleteApiResponse()
        {
            Success = false;
        }
    }
}
