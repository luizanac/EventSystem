using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
		
		public virtual IList<PointOfSaleEvent> PointOfSaleEvents { get; set; }

		//Adiciona um ponto de venda ao evento
		public void AddPointOfSaleInEvent(PointOfSale pointOfSale)
		{
			PointOfSaleEvents.Add(new PointOfSaleEvent(pointOfSale, this));
		}
		
		//Verifica se o pdv já está no evento
		public bool PointOfSaleInEvent(Guid pointOfsale)
		{
			var pointOfSaleEvent = PointOfSaleEvents.SingleOrDefault(ps => ps.PointOfSaleId == pointOfsale);
			if (pointOfSaleEvent == null)
			{
				return false;
			}

			return true;
			
		}
		
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