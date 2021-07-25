using Clinic.CrossCutting.CustomExceptions;
using Clinic.Domain.Models.Responses;

namespace Clinic.Domain.Services
{
    public abstract class ServiceMiddleware
    {
        protected void ValidateResponse(DefaultApiResponseResult response)
        {
            if (response.IsUnauthorizedResponse)
                throw new UnauthorizedException();

            if (response.ShouldFireNotFoundException)
                throw new NotFoundException(response.Message, response.NotFoundResourceId.Value);
        }

        protected void ValidateResponse<TData>(DefaultApiResponseResult<TData> response)
        {
            if (response.IsUnauthorizedResponse)
                throw new UnauthorizedException();

            if (response.ShouldFireNotFoundException)
                throw new NotFoundException(response.Message, response.NotFoundResourceId.Value);
        }
    }
}
