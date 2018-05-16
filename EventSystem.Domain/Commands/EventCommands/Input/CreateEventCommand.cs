
using System;
using System.Globalization;
using EventSystem.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;

namespace EventSystem.Domain.Commands.EventCommands.Input
{
	public class CreateEventCommand : Notifiable ,ICommand
	{

		private DateTime _startDate;
		private DateTime _endDate;

		public string Name { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public Guid EventAdministratorId { get; set; }
		public IFormFile Photo { get; set; }
		
		public bool IsValid()
		{
			AddNotifications(new Contract()
				.Requires()
				.IsNotNullOrEmpty(Name, "name", "Nome é um campo obrigatório" )
				.HasMaxLen(Name, 45,"name", "O máximo de caracteres para nome é 45")
				.IsTrue(DateTime.TryParse(StartDate , out _startDate), "startDate", "Data inicial deve ser válido" )
				.IsTrue(DateTime.TryParse(EndDate , out _endDate), "endDate", "Data final deve ser válido" )
			);
			
			return Valid;
		}
	}
}