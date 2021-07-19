using System;

namespace Clinic.CrossCutting.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {

        }

        public NotFoundException(string message)
            :base(message)
        {

        }

        public NotFoundException(string message, int id)
            : base(message)
        {
            Id = id;
        }

        public int? Id { get; set; }
    }
}
