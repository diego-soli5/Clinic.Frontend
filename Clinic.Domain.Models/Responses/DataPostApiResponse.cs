namespace Clinic.Domain.Models.Responses
{
    public class DataPostApiResponse<TData>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int? NewResourceId { get; set; }
        public TData Data { get; set; }

        public DataPostApiResponse()
        {
            Success = false;
        }
    }
}
