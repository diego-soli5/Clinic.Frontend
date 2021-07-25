using Clinic.Domain.Models.QueryFilters;
using System;

namespace Clinic.CrossCutting.Abstractions
{
    public interface IUriGenerator
    {
        /// <summary>
        /// Añade parametros queryString necesarios para la paginación y los pasados por parametro a la URL base.
        /// </summary>
        /// <typeparam name="TQueryFilter">El tipo de parametros de filtros y paginación</typeparam>
        /// <param name="baseUri">URL base para añadir los queryString.</param>
        /// <param name="filter">Los filtros de paginación y consulta.</param>
        /// <returns></returns>
        Uri CreatePagedListUri<TQueryFilter>(string baseUri, TQueryFilter filter)
             where TQueryFilter : BaseQueryFilter;
        /// <summary>
        /// Añade parametros queryString pasados por parametro a la URL base.
        /// </summary>
        /// <typeparam name="TQueryFilter">El tipo de parametros de filtros y paginación</typeparam>
        /// <param name="baseUri">URL base para añadir los queryString.</param>
        /// <param name="filter">Los filtros de paginación y consulta.</param>
        /// <returns></returns>
        Uri AddQueryStringParams(string baseUri, object queryStringParams);
    }
}
