using System.Collections.Generic;

namespace Clinic.Domain.Models.Responses
{
    public class DefaultApiResponseResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int? NewResourceId { get; set; }
        public List<string> ModelErrors { get; set; }
        public bool HasModelErrors => ModelErrors?.Count > 0;
        public Metadata Metadata { get; set; }

        public DefaultApiResponseResult()
        {
            Success = false;
        }
    }

    public class DefaultApiResponseResult<TData>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int? NewResourceId { get; set; }
        public List<string> ModelErrors { get; set; }
        public bool HasModelErrors => ModelErrors?.Count > 0;
        public Metadata Metadata { get; set; }
        public TData Data { get; set; }

        public DefaultApiResponseResult()
        {
            Success = false;
        }
    }
}
