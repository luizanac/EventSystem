using EventSystem.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace EventSystem.Domain.Commands.EventAdministratorCommands.Input
{
	public class CreateEventAdministratorCommand : Notifiable, ICommand
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public bool IsValid()
		{
			AddNotifications(new Contract()
				.Requires()
				.IsNotNullOrEmpty(Password, "password", "A senha não pode ser vazia")
				.HasMinLen(Password, 6, "password", "A senha não pode ter menos que 6 caracteres")
				.IsEmail(Email, "email", "O E-mail deve ser em um formato válido")
				.HasMinLen(Name, 3,"name", "O nome de usuário deve ter no minimo 3 caracteres")
				.HasMaxLen(Name, 45,"name", "O nome de usuário deve ter no máximo 45 caracteres")
				.IsNotNullOrEmpty(Name, "name", "O nome de usuário não pode ser vazio")
			);
			
			return Valid;
		}
	}
}