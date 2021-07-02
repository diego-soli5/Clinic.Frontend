namespace Clinic.Domain.Models.DTOs.Api
{
    public class ApiReponseResultDTO<TData>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public TData Data { get; set; }
        public Metadata Metadata { get; set; }

        public ApiReponseResultDTO()
        {
            Success = false;
        }
    }
}
