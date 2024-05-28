using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;

		public AuthController(UserManager<IdentityUser> userManager)
		{
			this.userManager = userManager;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
		{
			var user = new IdentityUser
			{
				UserName = request.Email?.Trim(),
				Email = request.Email?.Trim(),
			};

			var identityResult = await userManager.CreateAsync(user, request.Password);

			if (identityResult.Succeeded)
			{
				identityResult = await userManager.AddToRoleAsync(user, "Reader");

				if (identityResult.Succeeded)
				{
					return Ok();
				}
				else
				{
					if (identityResult.Errors.Any())
					{
						foreach (var error in identityResult.Errors)
						{
							ModelState.AddModelError("", error.Description);
						}
					}
				}
			}
			else
			{
				if (identityResult.Errors.Any())
				{
					foreach (var error in identityResult.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}

			return ValidationProblem(ModelState);
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
		{
			var identityUser = await userManager.FindByEmailAsync(request.Email);

			if (identityUser != null)
			{
				var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

				if (checkPasswordResult)
				{
					var roles = await userManager.GetRolesAsync(identityUser);

					var response = new LoginResponseDTO
					{
						Email = request.Email,
						Roles = roles.ToList(),
						Token = "TOKEN"
					};

					return Ok(response);
				}
			}
			else
			{
				ModelState.AddModelError("", "Invalid email or password.");
			}

			return ValidationProblem(ModelState);
		}
	}
}
