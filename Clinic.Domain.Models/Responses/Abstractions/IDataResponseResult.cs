namespace Clinic.Domain.Models.Responses.Abstractions
{
    public interface IDataResponseResult<TData> : IResponseResult
    {
        public TData Data { get; set; }
    }
}
