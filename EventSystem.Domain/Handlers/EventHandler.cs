using System.Threading.Tasks;
using EventSystem.Domain.Commands.EventCommands.Input;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Shared.Commands;

namespace EventSystem.Domain.Handlers
{
	public class EventHandler : 
		Handler,
		ICommandHandler<CreateEventCommand>
	{

		private readonly IEventRepository _eventRepository;
		
		public EventHandler(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}
		
		public async Task<ICommandResult> Handle(CreateEventCommand command)
		{
			// Valida o command
			if (!command.IsValid())
			{
				AddNotifications(command.Notifications);
			}

			var mEvent = new Event(command.Name, command.StartDate, command.EndDate);
			
			// Valida às entidades relacionadas
			AddNotifications(mEvent.Notifications);

			if (!IsValid())
				return null;
			
			// Cadastra o evento
			await _eventRepository.Create(mEvent);
			
			
			
			// Retorna os dados para o ICommandResult
			return null;
		}
		
		public bool IsValid()
		{
			return Valid;
		}

	}
}