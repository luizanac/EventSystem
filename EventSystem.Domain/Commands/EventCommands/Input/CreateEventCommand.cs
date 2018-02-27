
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
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public IFormFile Photo { get; set; }
		
		public bool IsValid()
		{
			AddNotifications(new Contract()
				.Requires()
				.IsNotNullOrEmpty(Name, "name", "Nome é um campo obrigatório" )
				.HasMaxLen(Name, 45,"name", "O máximo de caracteres para nome é 45")
				.IsTrue(DateTime.TryParse(StartDate.ToString(CultureInfo.CurrentCulture), out _startDate), "startDate", "Data inicial é um campo obrigatório" )
				.IsTrue(DateTime.TryParse(EndDate.ToString(CultureInfo.CurrentCulture), out _endDate), "endDate", "Data final é um campo obrigatório" )
			);
			
			return Valid;
		}
	}
}