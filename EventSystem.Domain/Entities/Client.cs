using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem.Shared.Entities;

namespace EventSystem.Domain.Entities
{
	public class Client : Entity
	{
		public string Rg { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public Decimal Balance { get; set; }
		
		public virtual IList<Payment> Payments { get; set; } 
	}
}