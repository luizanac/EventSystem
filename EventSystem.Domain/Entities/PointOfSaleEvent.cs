using EventSystem.Shared.Entities;

namespace EventSystem.Domain.Entities
{
	public class PointOfSaleEvent : Entity
	{
		public virtual PointOfSale PointOfSale { get; set; }
		public virtual Event Event { get; set; }
	}
}