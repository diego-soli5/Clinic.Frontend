using Clinic.Domain.QueryFilters;
using System;
using System.Collections.Generic;

namespace Clinic.CrossCutting.Abstractions
{
    public interface IUriGenerator
    {
        Uri CreatePagedListUri<TQueryFilter>(string baseUri, TQueryFilter filter)
             where TQueryFilter : BaseQueryFilter;
        Uri AddQueryStringParams(string baseUri, Dictionary<string, object> queryStringParams);
    }
}
