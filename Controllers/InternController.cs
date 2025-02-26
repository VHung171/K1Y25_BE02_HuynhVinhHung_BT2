using K1Y25_BE02_HuynhVinhHung_BT2.IServices;
using K1Y25_BE02_HuynhVinhHung_BT2.Repositories;
using K1Y25_BE02_HuynhVinhHung_BT2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Controllers
{
    [Route("api/interns")]
    [ApiController]
    public class InternController : ControllerBase
    {
        private readonly InternService _internService;

        public InternController(InternService internService)
        {
            _internService = internService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInterns()
        {
            var roleClaim =
            User.Claims.FirstOrDefault(c => c.Type
            ==
            "Role")?.Value;
            if (string.IsNullOrEmpty(roleClaim))
                return Unauthorized("Không tìm thấy thông tin Role trong token");
            var result = await _internService.GetInternsByAccessAsync(roleClaim, "Intern"); return Ok(result);
        }
    }
}
