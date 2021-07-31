using Clinic.Domain.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clinic.Web.Controllers
{
    [Route("api/Resource")]
    [ApiController]
    [Authorize]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetImg(string n, int? id)
        {
            (byte[], string) imageResult = (null, null);
           
            if(id.HasValue)
                imageResult = await _resourceService.GetProfileImage(id.Value, GetCurrentToken());
            else
                imageResult = await _resourceService.GetProfileImage(n, GetCurrentToken());

            if (imageResult.Item1 == null)
                return NotFound();

            return File(imageResult.Item1, imageResult.Item2);
        }

        #region UTILITY METHODS
        private string GetCurrentToken()
        {
            string token = User.FindFirstValue("Token");

            return token;
        }
        #endregion
    }
}
