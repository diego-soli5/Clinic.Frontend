using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string GetCurrentToken()
        {
            string token = User.FindFirstValue("Token");

            return token;
        }

        protected int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        protected string[] ValidImageTypes()
        {
            return new[]
            {
                "image/png",
                "image/jpg",
                "image/jpeg"
            };
        }
    }
}
