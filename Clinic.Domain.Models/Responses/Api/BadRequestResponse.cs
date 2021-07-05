using System.Collections.Generic;

namespace Clinic.Domain.Models.Responses.Api
{
    public class BadRequestResponse
    {
        public string Message { get; set; }
        public List<string> ModelErrors { get; set; }
        public bool HasModelErrors { get; set; }
    }
}
