namespace Clinic.Domain.Models.Responses
{
    public class DefaultPutApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public DefaultPutApiResponse()
        {
            Success = false;
        }
    }
}
