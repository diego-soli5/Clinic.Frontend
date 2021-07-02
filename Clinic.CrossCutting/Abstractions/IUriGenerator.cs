using Clinic.Domain.QueryFilters;
using System;

namespace Clinic.CrossCutting.Abstractions
{
    public interface IUriGenerator
    {
        Uri CreateUri<TQueryFilter>(string baseUri, TQueryFilter filter)
             where TQueryFilter : BaseQueryFilter;
    }
}
