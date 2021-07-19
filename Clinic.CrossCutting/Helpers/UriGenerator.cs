using Clinic.CrossCutting.Abstractions;
using Clinic.CrossCutting.Options;
using Clinic.Domain.Models.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;

namespace Clinic.CrossCutting.Helpers
{
    public class UriGenerator : IUriGenerator
    {
        private readonly PaginationOptions _options;
        public UriGenerator(IOptions<PaginationOptions> options)
        {
            _options = options.Value;
        }

        public Uri CreatePagedListUri<TQueryFilter>(string baseUri, TQueryFilter filter)
            where TQueryFilter : BaseQueryFilter
        {
            filter.PageSize = _options.DefaultPageSize;

            string uri = $"{baseUri}?";

            PropertyInfo[] properties = filter.GetType().GetProperties();

            properties.ToList().ForEach(prop =>
            {
                var value = prop.GetValue(filter);

                if (value != null)
                    uri += $"{prop.Name}={value}&";
            });

            uri = uri.Remove(uri.Length - 1);

            return new Uri(uri);
        }

        public Uri AddQueryStringParams(string baseUri, object queryStringParams)
        {
            string uri = $"{baseUri}?";

            PropertyInfo[] properties = queryStringParams.GetType().GetProperties();

            properties.ToList().ForEach(prop =>
            {
                var value = prop.GetValue(queryStringParams);

                if (value != null)
                    uri += $"{prop.Name}={value}&";
            });

            uri = uri.Remove(uri.Length - 1);

            return new Uri(uri);
        }
    }
}
