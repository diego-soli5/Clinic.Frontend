using System;

namespace Clinic.CrossCutting.CustomExceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message)
            : base(message)
        {
            
        }

        public UnauthorizedException()
        {

        }
    }
}
