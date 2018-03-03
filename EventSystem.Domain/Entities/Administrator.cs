using System.Collections.Generic;

namespace EventSystem.Domain.Entities
{
	public class Administrator : User
	{
		public Administrator(string name, string email, string password) : 
			base(name, email, password)
		{}

		public Administrator()
		{
			
		}
		public virtual IList<Event> Events { get; set; }
	}
}