using EventSystem.Domain.Entities;
using EventSystem.Shared.Commands;
using Microsoft.AspNetCore.Http;

namespace EventSystem.Domain.Commands.EventCommands.Output
{
	public class AddPointOfSaleInEventCommandResult : ICommandResult
	{
		public string Name { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public string Photo { get; set; }
		
		public PointOfSale PointOfSale { get; set; }
	}
}