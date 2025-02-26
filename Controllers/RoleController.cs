using K1Y25_BE02_HuynhVinhHung_BT2.Services;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using K1Y25_BE02_HuynhVinhHung_BT2.IServices;
using Microsoft.AspNetCore.Authorization;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleService.GetRoles();
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _roleService.GetRoleById(id);
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public IActionResult Create(RoleRequest roleRequest)
        {
            var result = _roleService.CreateRole(roleRequest);
            return result.Code == 0 ? CreatedAtAction(nameof(GetById), new { id = result.Data.RoleId }, result) : Conflict(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, RoleRequest roleRequest)
        {
            var result = _roleService.UpdateRole(id, roleRequest);
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _roleService.DeleteRole(id);
            return result.Code == 0 ? NoContent() : NotFound(result);
        }
    }
}
