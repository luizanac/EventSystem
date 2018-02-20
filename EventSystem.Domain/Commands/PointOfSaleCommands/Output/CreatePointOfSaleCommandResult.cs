using System;
using EventSystem.Shared.Commands;

namespace EventSystem.Domain.Commands.PointOfSaleCommands.Output
{
	public class CreatePointOfSaleCommandResult : ICommandResult
	{
		public CreatePointOfSaleCommandResult(Guid id, string name, string email, string cnpj, string phone)
		{
			Id = id;
			Name = name;
			Email = email;
			Cnpj = cnpj;
			Phone = phone;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Cnpj { get; set; }
		public string Phone { get; set; }
	}
}