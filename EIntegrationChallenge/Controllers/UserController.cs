using Application.dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EIntegrationChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IUserService userService
        ) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var user = await userService.CreateUserAsync(userDTO);
            if (user == null) return StatusCode(400, "Error al crear el usuario.");

            var result = await userManager.CreateAsync(user, userDTO.Password!);
            if (!result.Succeeded) return HandleErrors(result.Errors);

            await userService.EnsureRoleExistsAsync("Admin", roleManager);
            await userService.EnsureRoleExistsAsync("Customer", roleManager);

            await userManager.AddToRoleAsync(user, "Admin");

            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserDTO userDTO)
        {
            var result = await signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(userDTO.Email);
                var roles = await userManager.GetRolesAsync(user);
                var token = userService.CreateToken(user, roles, configuration);

                return new UserTokenDTO
                {
                    Token = token,
                    UserName = user.UserName,
                    Roles = roles
                };
            }
            ModelState.AddModelError("Response", "Invalid user");

            return StatusCode(400, ModelState);
        }

        private IActionResult HandleErrors(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("Response", error.Description);
            }
            return StatusCode(400, ModelState);
        }
    }
}
