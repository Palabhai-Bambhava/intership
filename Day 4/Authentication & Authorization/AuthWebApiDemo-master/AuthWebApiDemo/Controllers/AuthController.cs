//using AuthWebApiDemo.Entities;
//using AuthWebApiDemo.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace AuthWebApiDemo.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        public AuthController(IConfiguration configuration)
//        {
//            this.configuration = configuration;
//        }
//        private static User user = new();
//        private IConfiguration configuration;

//        [HttpPost("register")]

//        public ActionResult<User?> Register(UserDTO request)
//        {
//            user.Username = request.Username;
//            user.PasswordHash = new PasswordHasher<User>()
//                .HashPassword(user, request.Password);
//            return Ok(user);
//        }

//        [HttpPost("login")]
//        //public ActionResult<User?> Login(UserDTO request)
//        public ActionResult<string> Login(UserDTO request)
//        {
//            if (user.Username != request.Username)
//                return BadRequest("User Not Found");
//            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
//                == PasswordVerificationResult.Failed)
//                return BadRequest("Wrong Password");
//            //user.Username = request.Username;
//            //user.PasswordHash = new PasswordHasher<User>()
//            //    .HashPassword(user, request.Password);
//            string token = CreateToken(user);
//            return Ok(user);
//        }

//        private string CreateToken(User user)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Name,user.Username)
//            };
//            var key = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
//            var tokenDescriptor = new JwtSecurityToken(
//                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
//                audience: configuration.GetValue<string>("AppSettings:Audience"),
//                claims: claims,
//                expires: DateTime.UtcNow.AddDays(1),
//                signingCredentials: creds
//                );
//            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
//        }
//    }
//}
using AuthWebApiDemo.Model;
using AuthWebApiDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDTO request)
        {
            var token = _authService.Register(request);
            if (token == null)
                return BadRequest("User already exists or registration failed.");

            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public IActionResult Login(UserDTO request)
        {
            var token = _authService.Login(request);
            if (token == null)
                return BadRequest("Invalid username or password.");

            return Ok(new { Token = token });
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult ProtectedEndpoint()
        {
            var username = User.Identity?.Name;
            return Ok(new { Message = $"Hello {username}, you're authorized!" });
        }
    }
}
