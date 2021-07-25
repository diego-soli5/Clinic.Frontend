namespace Clinic.Domain.Models.Responses.Api
{
    public class NotFoundResponse
    {
        public string Message { get; set; }
        public int? NotFoundResourceId { get; set; }
    }
}
