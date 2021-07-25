using Clinic.Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Clinic.Data.Abstractions
{
    public interface IRepository
    {
        /// <summary>
        /// Hace una peticion GET que espera recibir data en JSON.
        /// </summary>
        /// <typeparam name="TData">El tipo de data que se espera recibir.</typeparam>
        /// <param name="url">Url para realizar la petición.</param>
        /// <param name="dataToSend">Contenido para agregar al body de la petición.</param>
        /// <param name="authToken">Token de autorización para realizar la petición.</param>
        /// <returns></returns>
        Task<DefaultApiResponseResult<TData>> Get<TData>(string url, object dataToSend = null, string authToken = null);

        /// <summary>
        /// Hace una peticion GET que recibe UNICAMENTE archivos.
        /// </summary>
        /// <param name="url">Url para realizar la petición.</param>
        /// <param name="authToken">Token de autorización para realizar la petición.</param>
        /// <returns></returns>
        Task<(byte[], string)> Get(string url, string authToken = null);

        /// <summary>
        /// Hace una peticion POST.
        /// </summary>
        /// <param name="url">Url para realizar la petición.</param>
        /// <param name="dataToSend">Contenido para agregar al body de la petición.</param>
        /// <param name="authToken">Token de autorización para realizar la petición.</param>
        /// <returns></returns>
        Task<DefaultApiResponseResult> Post(string url, object dataToSend = null, string authToken = null);

        /// <summary>
        /// Hace una peticion POST que envía UNICAMENTE archivos.
        /// </summary>
        /// <param name="url">Url para realizar la petición.</param>
        /// <param name="filesToSend">Arreglo de archivos para enviar en el Form-Data de la petición.</param>
        /// <param name="authToken">Token de autorización para realizar la petición.</param>
        /// <returns></returns>
        Task<DefaultApiResponseResult> Post(string url, IFormFile[] filesToSend, string authToken = null);

        /// <summary>
        /// Hace una peticion POST que espera recibir data en JSON.
        /// </summary>
        /// <typeparam name="TData">El tipo de dato que se espera recibir.</typeparam>
        /// <param name="url">Url para realizar la petición.</param>
        /// <param name="dataToSend">Contenido para agregar al body de la petición.</param>
        /// <param name="authToken">Token de autorización para realizar la petición.</param>
        /// <returns></returns>
        Task<DefaultApiResponseResult<TData>> Post<TData>(string url, object dataToSend = null, string authToken = null);

        /// <summary>
        /// Hace una peticion PUT.
        /// </summary>
        /// <param name="url">Url para realizar la petición.</param>
        /// <param name="dataToSend">Contenido para agregar al body de la petición.</param>
        /// <param name="authToken">Token de autorización para realizar la petición.</param>
        /// <returns></returns>
        Task<DefaultApiResponseResult> Put(string url, object dataToSend = null, string authToken = null);

        /// <summary>
        /// Hace una peticion DELETE.
        /// </summary>
        /// <param name="url">Url para realizar la petición.</param>
        /// <param name="dataToSend">Contenido para agregar al body de la petición.</param>
        /// <param name="authToken">Token de autorización para realizar la petición.</param>
        /// <returns></returns>
        Task<DefaultApiResponseResult> Delete(string url, object dataToSend = null, string authToken = null);

        /// <summary>
        /// Hace una peticion PATCH.
        /// </summary>
        /// <param name="url">Url para realizar la petición.</param>
        /// <param name="dataToSend">Contenido para agregar al body de la petición.</param>
        /// <param name="authToken">Token de autorización para realizar la petición.</param>
        /// <returns></returns>
        Task<DefaultApiResponseResult> Patch(string url, object dataToSend = null, string authToken = null);
    }
}
