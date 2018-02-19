using System;
using EventSystem.Shared.Commands;

namespace EventSystem.Domain.Commands.UserCommands.Outputs
{
	public class InfoUserCommandResult : ICommandResult
	{
		public InfoUserCommandResult(Guid id, string name, string email, bool isActive, bool isDisabled)
		{
			Id = id;
			Name = name;
			Email = email;
			IsActive = isActive;
			IsDisabled = isDisabled;
		}
		
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
		public bool IsDisabled { get; set; }
	}
}