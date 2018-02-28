using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using EventSystem.Domain.Commands.UserCommands.Inputs;
using EventSystem.Domain.Handlers;
using EventSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace EventSystem.Api.Controllers
{
	public class AuthController : Controller
	{
		private readonly IUserRepository _repository;
		private readonly UserHandler _handler;

		public AuthController(IUserRepository repository, UserHandler handler)
		{
			_repository = repository;
			_handler = handler;
		}
		
		[HttpPost]
		[Route("api/auth")]
		[AllowAnonymous]
		public async Task<IActionResult> Authenticate(LoginUserCommand command)
		{
			var result = await _handler.Handle(command);
			if (!_handler.IsValid())
				return BadRequest(_handler.GetErrors());
			
			return Ok(result);
		}
		
		[HttpPut]
		[Route("api/auth/changePassword")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> ChangePassword(ChangeUserPasswordCommand command)
		{
			var id = Guid.Parse(HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sid));
			
			var result = await _handler.Handle(command, id);
			if (!_handler.IsValid())
				return BadRequest(_handler.GetErrors());
			
			return Ok(result);
		}
		
		// GET api/Auth/Info
		[HttpGet]
		[Route("api/auth/info")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> Info()
		{
			var id = Guid.Parse(HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sid));
			var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
			var result = await _handler.Handle(new InfoUserCommand(id, role));
			
			return Ok(result);
		}	
		
		[HttpDelete]
		[Route("api/auth/disable/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
		public async Task<IActionResult> Disable(Guid id)
		{
			await _repository.Disable(id);
			await _repository.Commit();
			return Ok();
		}
		
		[HttpGet]
		[Route("api/auth/enable/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
		public async Task<IActionResult> Enable(Guid id)
		{
			await _repository.Disable(id);
			await _repository.Commit();
			return Ok();
		}

	}
}