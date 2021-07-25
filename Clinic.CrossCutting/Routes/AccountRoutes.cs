using Clinic.CrossCutting.Options;
using Microsoft.Extensions.Options;

namespace Clinic.CrossCutting.Routes
{
    public class AccountRoutes
    {
        private readonly ApiOptions _apiOptions;

        public AccountRoutes(IOptions<ApiOptions> options)
        {
            _apiOptions = options.Value;

            Account = $"{_apiOptions.Domain}/api/{nameof(Account)}";
        }

        public string Account { get; private set; }

        public string Authenticate
        {
            get
            {
                return $"{Account}/{nameof(Authenticate)}";
            }
        }

        public string GetCurrentUser
        {
            get
            {
                return $"{Account}/{nameof(GetCurrentUser)}/";
            }
        }

        public string ChangeImage
        {
            get
            {
                return $"{Account}/{nameof(ChangeImage)}/";
            }
        }

        public string Logout
        {
            get
            {
                return $"{Account}/{nameof(Logout)}";
            }
        }

        public string PasswordChangeRequest
        {
            get
            {
                return $"{Account}/{nameof(PasswordChangeRequest)}";
            }
        }

        public string ChangePassword
        {
            get
            {
                return $"{Account}/{nameof(ChangePassword)}";
            }
        }
    }
}
