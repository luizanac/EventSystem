using System;
using EventSystem.Shared.Entities;

namespace EventSystem.Domain.Entities
{
	public class Payment : Entity
	{
		public Decimal Value { get; set; }
		public DateTime PaymenteDate { get; set; }
		
		public virtual Client Client { get; set; }
		public virtual PointOfSale PointOfSale { get; set; }
		public virtual Event Event { get; set; }
	}
}