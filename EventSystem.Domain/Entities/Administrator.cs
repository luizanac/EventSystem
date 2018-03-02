using System.Collections.Generic;

namespace EventSystem.Domain.Entities
{
	public class Administrator : User
	{
		public virtual IEnumerable<Event> Events { get; set; }
	}
}