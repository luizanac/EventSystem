using System;
using EventSystem.Shared.Entities;

namespace EventSystem.Domain.Entities
{
	public class Payment : Entity
	{
		public Decimal Value { get; set; }
		public DateTime PaymenteDate { get; set; }
		
		public Guid ClientId { get; set; }
		public Guid PointOfSaleEventId { get; set; }
		
		public virtual PointOfSaleEvent PointOfSaleEvent { get; set; }
		public virtual Client Client { get; set; }
	}
}