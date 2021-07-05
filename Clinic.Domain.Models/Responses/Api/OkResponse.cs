using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Models.Responses.Api
{
    public class OkResponse
    {
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
