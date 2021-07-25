using Clinic.Domain.Abstractions;
using Clinic.Domain.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clinic.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            int idCurrentUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var userInfo = await _service.GetCurrentUser(idCurrentUser, GetCurrentToken());

            return View(userInfo);
        }

        [HttpGet]
        [IsAlreadyAuthenticatedFilter]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl != null)
                ViewData["returnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string credential, string password, string returnUrl)
        {
            var result = await _service.TryAuthenticateAsync(credential, password);

            if (result.Success)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name, result.Data.FullName),
                    new Claim(ClaimTypes.Email,result.Data.Email),
                    new Claim(ClaimTypes.MobilePhone, result.Data.PhoneNumber.ToString()),
                    new Claim("ImageName", result.Data.ImageName ?? ""),
                    new Claim(ClaimTypes.Role, result.Data.AppUserRole.ToString()),
                    new Claim(ClaimTypes.Role, result.Data.EmployeeRole.ToString()),
                    new Claim("Token", result.Data.Token)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ViewData["LoginMessage"] = result.Message;

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Unauthorizedv()
        {
            return View();
        }

        #region API ACTIONS
        [HttpPost]
        [Route("api/Account/Profile/ChangeImage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeImage(IFormFile image)
        {
            if (image == null)
                return BadRequest(new
                {
                    Success = false,
                    Message = "Selecciona una imagen válida."
                });

            if (!ValidImageTypes().Any(x => x == image.ContentType))
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Sólo se aceptan imagenes de tipo png, jpg o jpeg."
                });
            }

            var response = await _service.ChangeImage(image, GetCurrentUserId(), GetCurrentToken());

            if (response.Success)
            {
                return Ok(new { Success = true, response.Message });
            }

            return StatusCode(response.StatusCode, new { response.Success, response.Message });
        }
        #endregion

        
    }
}
