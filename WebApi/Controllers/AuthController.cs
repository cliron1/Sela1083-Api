using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Data;
using WebApi.Data.Entities;

namespace WebApi.Controllers;

[ApiController]
[Route("auth")]
[AllowAnonymous]
public class AuthController : ControllerBase {
	public const string TOKEN_SECRET = "Th1s 1s a s3cr3t that I don't want anyone to know!";

	private readonly MyContext context;

	public AuthController(MyContext context) {
		this.context = context;
	}

	[HttpPost("login")]
	public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model) {
		var user = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
		if(user == null || user.Pwd != model.Pwd.Hash())
			return Unauthorized();

		var token = generateToken(user);

		return new LoginResponseModel {
			User = user,
			Token = token
		};
	}

	private string generateToken(User user) {
		var claims = new List<Claim> {
			new Claim("id", user.Id.ToString())
		};

		var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TOKEN_SECRET));
		var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: "WebApi",
			audience: "myAngularApp.com",
			claims: claims,
			notBefore: DateTime.UtcNow,
			expires: DateTime.Now.AddMinutes(30),

			signingCredentials: credentials
	   );
		string ret = new JwtSecurityTokenHandler()
			.WriteToken(token);
		return ret;
	}

	public class LoginRequestModel {
		[Required, EmailAddress]
		public string Email { get; set; }

		[Required, MinLength(4)]
		public string Pwd { get; set; }
	}

	public class LoginResponseModel {
		public User User { get; set; }
		public string Token { get; set; }
	}
}
