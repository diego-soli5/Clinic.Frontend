using Clinic.CrossCutting.CustomExceptions;
using Clinic.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinic.Web.Controllers
{
    public class ErrorController : BaseController
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

            if (exception is NotFoundException)
            {
                var nFexc = exception as NotFoundException;

                var oVM = new NotFoundViewModel();

                oVM.Message = nFexc.Message;
                oVM.Id = nFexc.Id;

                return View("NotFound", oVM);
            }

            return View("Error", new ErrorViewModel());

        }
    }
}


