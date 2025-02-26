using K1Y25_BE02_HuynhVinhHung_BT2.Services;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using K1Y25_BE02_HuynhVinhHung_BT2.IServices;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace K1Y25_BE02_HuynhVinhHung_BT2.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery]
        string search = "", [FromQuery] string sortColumn = "Id", [FromQuery] string sortOrder = "asc")
        {
            var result = _userService.GetUsers(page, pageSize, search, sortColumn, sortOrder);
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _userService.GetUserById(id);
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public IActionResult Create(UserRequest userRequest)
        {
            var result = _userService.CreateUser(userRequest);
            return result.Code == 0 ? CreatedAtAction(nameof(GetById), new { id = result.Data.UserId }, result) : Conflict(result);
             
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UserRequest userRequest)
        {
            var result = _userService.UpdateUser(id, userRequest);
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _userService.DeleteUser(id);
            return result.Code == 0 ? Ok(result) : NotFound(result);
        }


    }
}
