using Clinic.Domain.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        public async Task<IActionResult> GetProfileImage(int id)
        {
            var imageResult = await _resourceService.GetProfileImage(id, GetCurrentToken());

            if (imageResult.Item1 == null)
                return NotFound();

            return File(imageResult.Item1, imageResult.Item2);
        }

        #region UTILITY METHODS
        private string GetCurrentToken()
        {
            string token = User.Claims.FirstOrDefault(x => x.Type == "Token").Value;

            return token;
        }
        #endregion
    }
}
