using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShowErrorsInFormik.Data.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShowErrorsInFormik.Services
{
    public interface IJwtBearerTokenService 
    {
        public string CreateToken(AppUser user);
    }
    public class JwtBearerTokenService : IJwtBearerTokenService
    {
        private UserManager<AppUser> _userManager { get; set; }
        private IConfiguration _configuration { get; set; }
        public JwtBearerTokenService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public string CreateToken(AppUser user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;

            List<Claim> claims = new List<Claim> { 
                new Claim("name", user.Firstname),
                new Claim("email", user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim("roles", role));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration
                .GetValue<String>("PRV_KEY")));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: credentials, claims: claims,
                expires: DateTime.Now.AddDays(100));


            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
