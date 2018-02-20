using System.Threading.Tasks;
using EventSystem.Domain.Commands.EventAdministratorCommands.Input;
using EventSystem.Domain.Commands.EventAdministratorCommands.Output;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Shared.Commands;

namespace EventSystem.Domain.Handlers
{
	public class EventAdministratorHandler :
		Handler, 
		ICommandHandler<CreateEventAdministratorCommand>
	{

		private readonly IUserRepository _userRepository;
		private readonly IEventAdministratorRepository _eventAdministratorRepository;
		
		public EventAdministratorHandler(IUserRepository userRepository, IEventAdministratorRepository eventAdministratorRepository)
		{
			_userRepository = userRepository;
			_eventAdministratorRepository = eventAdministratorRepository;
		}
		
		public async Task<ICommandResult> Handle(CreateEventAdministratorCommand command)
		{
			//Verifica se o e-mail já está sendo utilizado
			if(await _userRepository.GetUserByEmail(command.Email) != null)
				AddNotification("email", "Este e-mail já está sendo utilizado");

			var eventAdministrator = new EventAdministrator(command.Name, command.Email, command.Password);
			
			AddNotifications(eventAdministrator.Notifications);
			
			if (!IsValid())
				return null;
		
			//Faz a persistencia no banco
			await _eventAdministratorRepository.Create(eventAdministrator);

			await _eventAdministratorRepository.Commit();
			
			//retorna o resultado para tela
			return new CreateEventAdministratorCommandResult(
				eventAdministrator.Id,
				eventAdministrator.Name,
				eventAdministrator.Email
			);
		}

		public bool IsValid()
		{
			return Valid;
		}
	}
}