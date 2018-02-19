using EventSystem.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace EventSystem.Domain.Commands.UserCommands.Inputs
{
	public class LoginUserCommand : Notifiable, ICommand
	{
		public string Email { get; set; }
		public string Password { get; set; }

		public bool IsValid()
		{			
			AddNotifications(new Contract()
				.Requires()
				.IsEmail(Email, "email", "O E-mail informado é inválido"));
			
			return Valid;
		}
	}
}