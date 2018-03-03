using System;
using EventSystem.Shared.Commands;

namespace EventSystem.Domain.Commands.BoxOfficeCommands.Output
{
	public class CreateBoxOfficeCommandResult : ICommandResult
	{
		public CreateBoxOfficeCommandResult(Guid id, string name, string email)
		{
			Id = id;
			Name = name;
			Email = email;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	}
}