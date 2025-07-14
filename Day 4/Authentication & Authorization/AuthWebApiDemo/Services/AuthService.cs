using AuthWebApiDemo.Data;
using AuthWebApiDemo.Entities;
using AuthWebApiDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthWebApiDemo.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly MyDbContext _context;

        public AuthService(IConfiguration configuration, MyDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string? Register(UserDTO request)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (existingUser != null)
                return null;

            var user = new User
            {
                Username = request.Username,
                PasswordHash = new PasswordHasher<User>()
                    .HashPassword(null!, request.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreateToken(user);
        }

        public string? Login(UserDTO request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null) return null;

            var result = new PasswordHasher<User>()
                .VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
