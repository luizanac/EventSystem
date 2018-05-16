using System;
using EventSystem.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace EventSystem.Domain.Commands.EventAdministratorCommands.Input
{
	public class UpdateEventAdministratorCommand : Notifiable, ICommand
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public bool IsValid()
		{
			AddNotifications(new Contract()
				.Requires()
				.HasMinLen(Name, 3,"name", "O nome de usuário deve ter no minimo 3 caracteres")
				.HasMaxLen(Name, 45,"name", "O nome de usuário deve ter no máximo 45 caracteres")
				.IsNotNullOrEmpty(Name, "name", "O nome de usuário não pode ser vazio")
			);
			
			return Valid;
		}
	}
}