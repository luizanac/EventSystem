using System;
using System.Globalization;
using EventSystem.Shared.Entities;
using Flunt.Validations;

namespace EventSystem.Domain.Entities
{
	public class Event : Entity
	{
		public Event()
		{}
		
		public Event(string name, DateTime startDate, DateTime endDate, string photo)
			:base()
		{
			Name = name;
			StartDate = startDate;
			EndDate = endDate;
			Photo = photo;
			
			this.Validate();
		}
		
		public Event(string name, DateTime startDate, DateTime endDate)
			:base()
		{
			Name = name;
			StartDate = startDate;
			EndDate = endDate;
			
			this.Validate();
		}
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Photo { get; set; }
		

		private void Validate()
		{
			DateTime startDate;
			DateTime endDate;
			
			AddNotifications(new Contract()
				.Requires()
				.IsNotNullOrEmpty(Name, "name", "Nome é um campo obrigatório" )
				.HasMaxLen(Name, 45,"name", "O máximo de caracteres para nome é 45")
				.IsTrue(DateTime.TryParse(StartDate.ToString(CultureInfo.CurrentCulture), out startDate), "startDate", "Data inicial deve ser válido" )
				.IsTrue(DateTime.TryParse(EndDate.ToString(CultureInfo.CurrentCulture), out endDate), "endDate", "Data final deve ser válido" )
			);
		}
	}
}