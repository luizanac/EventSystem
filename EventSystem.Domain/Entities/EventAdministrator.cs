using System;
using System.Security.Cryptography;
using System.Text;
using Flunt.Validations;

namespace EventSystem.Domain.Entities
{
	public class EventAdministrator : User
	{
		public EventAdministrator()
		{
		}

		public EventAdministrator(string name, string email, string password) : base(name, email, password)
		{}
	}
}