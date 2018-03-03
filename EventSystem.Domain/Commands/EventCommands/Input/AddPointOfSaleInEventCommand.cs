using System;
using EventSystem.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace EventSystem.Domain.Commands.EventCommands.Input
{
	public class AddPointOfSaleInEventCommand : Notifiable ,ICommand
	{
		public Guid PointOfSaleId { get; set; }
		public Guid EventId { get; set; }
		
		public bool IsValid()
		{
			AddNotifications(new Contract()
				.Requires()
				.IsNotNullOrEmpty(PointOfSaleId.ToString(), "pointOfsaleId", "Você deve informar um ponto de venda")
				.IsNotNullOrEmpty(EventId.ToString(), "eventId", "Você deve informar um evento válido")
			);
			
			return Valid;
		}
	}
}