using System;
using EventSystem.Shared.Commands;
using Flunt.Notifications;

namespace EventSystem.Domain.Commands.UserCommands.Inputs
{
	public class InfoUserCommand : Notifiable, ICommand
	{
		public Guid Id { get; set; }
		public string Role { get; set; }

		public InfoUserCommand(Guid id, string role)
		{
			Id = id;
			Role = role;
		}

		public void Validate()
		{}

		public bool IsValid()
		{
			return Valid;
		}
	}
}