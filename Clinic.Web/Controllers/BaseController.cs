using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string CurrentToken => User.FindFirstValue("Token");
        protected int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        protected string[] ValidImageTypes
        {
            get
            {
                return new[] {

                    "image/png",
                    "image/jpg",
                    "image/jpeg"
                };
            }
        }
    }
}