using Clinic.Domain.Models.QueryFilters;
using System;

namespace Clinic.CrossCutting.Abstractions
{
    public interface IUriGenerator
    {
        Uri CreatePagedListUri<TQueryFilter>(string baseUri, TQueryFilter filter)
             where TQueryFilter : BaseQueryFilter;
        Uri AddQueryStringParams(string baseUri, object queryStringParams);
    }
}
