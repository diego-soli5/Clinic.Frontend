using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Models.Responses
{
    public class DefaultPatchApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public DefaultPatchApiResponse()
        {
            Success = false;
        }
    }
}
