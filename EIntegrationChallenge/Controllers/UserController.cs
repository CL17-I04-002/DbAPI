using EIntegrationChallenge.Controllers.dto;
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
            IConfiguration configuration
        ) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var user = await CreateUserAsync(userDTO);
            if (user == null)
            {
                return StatusCode(400, "Error al crear el usuario.");
            }

            var result = await userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded)
            {
                return HandleErrors(result.Errors);
            }

            await EnsureRoleExistsAsync("Admin");
            await EnsureRoleExistsAsync("Customer");

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
                var token = CreateToken(user, roles);

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

        private string CreateToken(IdentityUser user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
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

        private async Task<IdentityUser> CreateUserAsync(UserDTO userDTO)
        {
            var user = new IdentityUser { UserName = userDTO.Email, Email = userDTO.Email };
            return user;
        }

        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
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
