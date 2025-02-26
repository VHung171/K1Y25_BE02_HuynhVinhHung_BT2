using Microsoft.AspNetCore.Mvc;
using K1Y25_BE02_HuynhVinhHung_BT2.IServices;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using Microsoft.AspNetCore.Authorization;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Controllers
{
    [Route("api/allow-access")]
    [ApiController]
    [Authorize]
    public class AllowAccessController : ControllerBase
    {
        private readonly IAllowAccessService _allowAccessService;

        public AllowAccessController(IAllowAccessService allowAccessService)
        {
            _allowAccessService = allowAccessService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _allowAccessService.GetAllowAccessList();
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _allowAccessService.GetAllowAccessById(id);
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public IActionResult Create(AllowAccessRequest request)
        {
            var result = _allowAccessService.CreateAllowAccess(request);
            return result.Code == 0 ? CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result) : Conflict(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, AllowAccessRequest request)
        {
            var result = _allowAccessService.UpdateAllowAccess(id, request);
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _allowAccessService.DeleteAllowAccess(id);
            return result.Code == 0 ? NoContent() : NotFound(result);
        }
    }


}
