using Clinic.CrossCutting.CustomExceptions;
using Clinic.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinic.Web.Controllers
{
    public class ErrorController : Controller
    {
        public async Task<IActionResult> Handle()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error;

            if (exception is UnauthorizedException)
            {
                await HttpContext.SignOutAsync();

                TempData["Sesion"] = "La sesión ha expirado, por favor vuelve a iniciar sesión.";

                return RedirectToAction("Login", "Account");
            }

            return View("Error", new ErrorViewModel());
        }
    }
}

