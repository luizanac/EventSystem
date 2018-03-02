using System;
using EventSystem.Shared.Commands;

namespace EventSystem.Domain.Commands.EventCommands.Output
{
	public class CreateEventCommandResult : ICommandResult
	{
		public CreateEventCommandResult(string name, DateTime startDate, DateTime endDate, string photo, DateTime createDate)
		{
			Name = name;
			StartDate = startDate;
			EndDate = endDate;
			Photo = photo;
			CreateDate = createDate;
		}

		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Photo { get; set; }
		public DateTime CreateDate { get; set; }
	}
}