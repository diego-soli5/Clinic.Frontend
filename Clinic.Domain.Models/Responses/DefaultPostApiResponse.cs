namespace Clinic.Domain.Models.Responses
{
    public class DefaultPostApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public DefaultPostApiResponse()
        {
            Success = false;
        }
    }
}
