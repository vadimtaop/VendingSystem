using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingSystemAPI.Models;

namespace VendingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly VendingSystemDbContext _context;

        public AuthController(VendingSystemDbContext context)
        {
            _context = context;
        }

        public class LoginRequest
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Не введен логин или пароль!");
            }

            var user = _context.Users.FirstOrDefault(u => u.Login == request.Login && u.PasswordHash == request.Password);

            if (user == null)
            {
                return Unauthorized("Неверный логин или пароль!");
            }

            return Ok(new
            {
                user.Id,
                user.Surname,
                user.Name,
                user.Patronymic,
                user.Role
            });
        }
    }
}
