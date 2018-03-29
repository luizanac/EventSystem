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
		
		public IList<EventAdministrator> EventAdministrators { get; set; }
	}
}