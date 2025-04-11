using Application.dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        string CreateToken(IdentityUser user, IList<string> roles, IConfiguration conf);
        Task<IdentityUser> CreateUserAsync(UserDTO userDTO);
        Task EnsureRoleExistsAsync(string roleName, RoleManager<IdentityRole> roleManager);
    }
}
