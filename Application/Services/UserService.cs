using Application.dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        public string CreateToken(IdentityUser user, IList<string> roles, IConfiguration conf)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var claims = new List<Claim>()
            {

                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var rolName in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rolName));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IdentityUser> CreateUserAsync(UserDTO userDTO)
        {
            var user = new IdentityUser { UserName = userDTO.Email, Email = userDTO.Email };
            return user;
        }

        public async Task EnsureRoleExistsAsync(string roleName, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
