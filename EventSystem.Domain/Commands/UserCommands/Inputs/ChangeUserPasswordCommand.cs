using EventSystem.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace EventSystem.Domain.Commands.UserCommands.Inputs
{
	public class ChangeUserPasswordCommand : Notifiable, ICommand
	{
		
		public string Password { get; set; }
		public string PasswordValidate { get; set; }
		
		
		public bool IsValid()
		{
			AddNotifications(new Contract()
				.Requires()
				.AreEquals(Password, PasswordValidate, "password", "Ambos campos de senha devem ser iguais")
				.IsNotNullOrEmpty(Password, "password", "A senha não pode ser vazia")
				.IsNotNullOrEmpty(PasswordValidate, "passwordValidate", "A confirmação de senha não pode ser vazia")
				.HasMinLen(Password, 6, "password", "A senha não pode ter menos que 6 caracteres")
			);
			
			return Valid;
		}
	}
}