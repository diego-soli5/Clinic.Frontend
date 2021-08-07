using Clinic.Domain.Models.Responses.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Clinic.Domain.Models.Responses
{
    public class DefaultResponseResult : IResponseResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success => StatusCode == StatusCodes.Status200OK ||
                               StatusCode == StatusCodes.Status201Created ||
                               StatusCode == StatusCodes.Status204NoContent;
        public int? NewResourceId { get; set; }
        public string NewResourceName { get; set; }
        public int? NotFoundResourceId { get; set; }
        public List<string> ModelErrors { get; set; }
        public bool HasModelErrors => ModelErrors?.Count > 0;
        public Metadata Metadata { get; set; }
        public bool ShouldFireNotFoundException => NotFoundResourceId.HasValue;
        public bool IsUnauthorizedResponse => StatusCode == StatusCodes.Status401Unauthorized;
    }

    public class DefaultResponseResult<TData> : DefaultResponseResult, IDataResponseResult<TData>
    {
        public TData Data { get; set; }
    }
}
