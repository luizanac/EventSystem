
using System;
using System.Security.Cryptography;
using System.Text;
using EventSystem.Shared.Entities;
using Flunt.Notifications;
using Flunt.Validations;
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
			
			AddNotifications(new Contract()
				.Requires()
				.IsNotNullOrEmpty(password, "password", "A senha não pode ser vazia")
				.HasMinLen(password, 6, "password", "A senha não pode ter menos que 6 caracteres")
				.IsEmail(Email, "email", "O E-mail deve ser em um formato válido")
				.HasMinLen(Name, 3,"name", "O nome de usuário deve ter no minimo 3 caracteres")
				.HasMaxLen(Name, 45,"name", "O nome de usuário deve ter no máximo 45 caracteres")
				.IsNotNullOrEmpty(Name, "name", "O nome de usuário não pode ser vazio")
			);
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