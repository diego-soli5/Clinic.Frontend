using System;

namespace Clinic.Domain.Models.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
            RequestId = Guid.NewGuid().ToString();
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}