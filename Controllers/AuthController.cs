using K1Y25_BE02_HuynhVinhHung_BT2.Data;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using K1Y25_BE02_HuynhVinhHung_BT2.Repositories;
using K1Y25_BE02_HuynhVinhHung_BT2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly UserRepo _userRepository;
    private readonly JwtService _jwtService;

    public AuthController(ApplicationDbContext context,UserRepo userRepository, JwtService jwtService, IConfiguration configuration)
    {
        _context = context;
        _userRepository = userRepository;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest userRequest)
    {
        if (userRequest == null || string.IsNullOrWhiteSpace(userRequest.Email) || string.IsNullOrWhiteSpace(userRequest.Password))
        {
            return BadRequest(new { message = "Email và mật khẩu không được để trống!" });
        }

        var user = _context.Users.FirstOrDefault(u => u.Email == userRequest.Email);
        if (user == null || user.Password != userRequest.Password) // Cần hash mật khẩu trong thực tế
        {
            return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu!" });
        }

        //if (user.RoleId != 2) 
        //{
         //   return Unauthorized(new { message = "Bạn không có quyền đăng nhập vào hệ thống!" });
        //}
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); // Sử dụng IConfiguration để lấy giá trị từ appsettings.json

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.RoleId?.ToString() ?? "0") // RoleId có thể null
                }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new { Token = tokenHandler.WriteToken(token) });
    }

}
