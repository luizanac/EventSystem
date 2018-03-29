using System;
using System.Collections.Generic;
using EventSystem.Shared.Entities;

namespace EventSystem.Domain.Entities
{
	public class PointOfSaleEvent : Entity
	{
		public PointOfSaleEvent()
		{}
		
		public PointOfSaleEvent(PointOfSale pointOfSale, Event @event)
		{
			PointOfSale = pointOfSale;
			Event = @event;
		}
		
		public Guid PointOfSaleId { get; set; }
		public Guid EventId { get; set; }
		
		public virtual PointOfSale PointOfSale { get; set; }
		public virtual Event Event { get; set; }
		public virtual IList<Payment> Payments { get; set; }
	}
}