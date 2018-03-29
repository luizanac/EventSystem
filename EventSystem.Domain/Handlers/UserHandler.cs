using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EventSystem.Domain.Commands.UserCommands.Inputs;
using EventSystem.Domain.Commands.UserCommands.Outputs;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Domain.Services;
using EventSystem.Shared.Commands;
using Flunt.Notifications;
using Microsoft.Extensions.Logging;

namespace EventSystem.Domain.Handlers
{
	public class UserHandler :
		Handler,
		ICommandHandler<LoginUserCommand, ICommandResult>,
		ICommandHandler<InfoUserCommand, ICommandResult>
	{
		private readonly IUserRepository _userRepository;
		private readonly IJwtService _jwtService;
		private readonly ILogger<UserHandler> _logger;

		public UserHandler(IUserRepository userRepository, IJwtService jwtService, ILogger<UserHandler> logger)
		{
			_userRepository = userRepository;
			_jwtService = jwtService;
			_logger = logger;
		}

		public async Task<ICommandResult> Handle(LoginUserCommand command)
		{

			if (!command.IsValid())
			{
				AddNotifications(command.Notifications);
				return null;
			}
			
			var user = await _userRepository.GetUserByEmailAndPassword(command.Email, command.Password);
			if (user == null)
			{
				AddNotification("message", "E-mail ou senha inválidos");
				return null;
			}

			if (user.IsDisabled != null && user.IsDisabled.Value)
			{
				AddNotification("message", "Sua conta está desativada, entre em contato com o administrador");
				return null;
			}

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Discriminator), 
			};

			return new LoginUserCommandResult("Bearer", _jwtService.GenerateToken(claims), DateTime.Now.AddDays(14));
		}
		
		public async Task<ICommandResult> Handle(InfoUserCommand command)
		{
			var user =  await _userRepository.GetById(command.Id);
			return new InfoUserCommandResult(
				user.Id,
				user.Name,
				user.Email,
				user.IsActive,
				user.IsDisabled
			);
		}

		public async Task<ICommandResult> Handle(ChangeUserPasswordCommand command, Guid id)
		{
			if (!command.IsValid())
			{
				AddNotifications(command.Notifications);
				return null;
			}

			var user = await _userRepository.GetById(id);
			var sha256 = SHA256.Create();
			var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(command.Password));
			var passwordHashed = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			user.Password = passwordHashed;
			await _userRepository.Commit();
			return new ChangeUserPasswordCommandResult();
		}
		
		public bool IsValid()
		{
			return Valid;
		}
	}
}