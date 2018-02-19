using System;

namespace EventSystem.Domain.Entities
{
	public class UserEvent
	{
		public Guid UserId { get; set; }
		public Guid EventId { get; set; }
	}
}