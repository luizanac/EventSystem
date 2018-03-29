using System;
using System.Threading.Tasks;
using EventSystem.Domain.Commands.EventCommands.Input;
using EventSystem.Domain.Commands.EventCommands.Output;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Shared.Commands;
using Microsoft.Extensions.Logging;

namespace EventSystem.Domain.Handlers
{
	public class EventHandler : 
		Handler,
		ICommandHandler<CreateEventCommand, ICommandResult>,
		ICommandHandler<AddPointOfSaleInEventCommand, ICommandResult>
	{

		private readonly IEventRepository _eventRepository;
		private readonly IPointOfSaleRepository _pointOfSaleRepository;
		private readonly ILogger<EventHandler> _logger;
		
		public EventHandler(
			IEventRepository eventRepository, 
			IPointOfSaleRepository pointOfSaleRepository, ILogger<EventHandler> logger)
		{
			_eventRepository = eventRepository;
			_pointOfSaleRepository = pointOfSaleRepository;
			_logger = logger;
		}
		
		public async Task<ICommandResult> Handle(CreateEventCommand command)
		{
			// Valida o command
			if (!command.IsValid())
			{
				AddNotifications(command.Notifications);
				return null;
			}

			var mEvent = new Event(command.Name, DateTime.Parse(command.StartDate), DateTime.Parse(command.EndDate));
			
			// Valida às entidades relacionadas
			AddNotifications(mEvent.Notifications);

			if (!IsValid())
				return null;
			
			// Cadastra o evento
			await _eventRepository.Create(mEvent);
			await _eventRepository.Commit();
			
			
			// Retorna os dados para o ICommandResult
			return new CreateEventCommandResult(
				mEvent.Name, 
				mEvent.StartDate, 
				mEvent.EndDate, 
				mEvent.Photo, 
				mEvent.CreateDate
			);
		}
		
		
		public async Task<ICommandResult> Handle(AddPointOfSaleInEventCommand command)
		{
			if (!command.IsValid())
			{
				AddNotifications(command.Notifications);
				return null;
			}

			var @event = await _eventRepository.GetByIdWithPointOfSaleEvents(command.EventId);
			if (@event.PointOfSaleInEvent(command.PointOfSaleId))
			{
				AddNotification("event", "Ponto de venda já está cadastrado neste evento");
				return null;
			}
			
			var pointOfSale = await _pointOfSaleRepository.GetById(command.PointOfSaleId);
			@event.AddPointOfSaleInEvent(pointOfSale);
			await _eventRepository.Commit();
			
			return new AddPointOfSaleInEventCommandResult()
			{
				Name = @event.Name,
				StartDate = @event.StartDate.ToShortDateString(),
				EndDate = @event.EndDate.ToShortDateString(),
				Photo = @event.Photo,
				PointOfSale = pointOfSale
			};
		}
		
		public bool IsValid()
		{
			return Valid;
		}

	}
}