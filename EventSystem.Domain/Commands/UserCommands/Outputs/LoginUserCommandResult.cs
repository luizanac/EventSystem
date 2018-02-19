using System;
using EventSystem.Shared.Commands;

namespace EventSystem.Domain.Commands.UserCommands.Outputs
{
	public class LoginUserCommandResult : ICommandResult
	{

		public LoginUserCommandResult(string type, string accessToken, DateTime expirationDate)
		{
			Type = type;
			AccessToken = accessToken;
			ExpirationDate = expirationDate;
		}
		
		public string Type { get; set; }
		public string AccessToken { get; set; }
		public DateTime ExpirationDate { get; set; }
		
	}
}