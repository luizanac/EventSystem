using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Flunt.Validations;

namespace EventSystem.Domain.Entities
{
	public class EventAdministrator : User
	{
		public EventAdministrator() : base()
		{
		}

		public EventAdministrator(string name, string email, string password) : base(name, email, password)
		{
		}
		
		public Guid AdministratorId { get; set; }
		public virtual Administrator Administrator { get; set; }
		
		public virtual IList<Event> Events { get; set; }
	}
}