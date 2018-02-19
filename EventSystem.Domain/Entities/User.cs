
using System;
using System.Security.Cryptography;
using System.Text;
using EventSystem.Shared.Entities;
using Flunt.Notifications;
using Newtonsoft.Json;

namespace EventSystem.Domain.Entities
{
	public class User : Entity
	{
		public User()
		{}
		
		public User(string name, string email, string password) : base()
		{
			Name = name;
			Email = email;
			
			var sha256 = SHA256.Create();
			var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
			var passwordHashed = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			Password = passwordHashed;
		}
		
		public string Name { get; set; }
		public string Email { get; set; }
		[JsonIgnore]
		public string Password { get; set; }
		public bool IsActive { get; set; }
		public bool IsDisabled { get; set; }
		public string Discriminator { get; set; }
	}
}	