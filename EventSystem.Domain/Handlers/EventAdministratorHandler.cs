using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using EventSystem.Domain.Commands.EventAdministratorCommands.Input;
using EventSystem.Domain.Commands.EventAdministratorCommands.Output;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Shared.Commands;
using Microsoft.AspNetCore.Http;

namespace EventSystem.Domain.Handlers
{
	public class EventAdministratorHandler :
		Handler, 
		ICommandHandler<CreateEventAdministratorCommand, ICommandResult>,
		ICommandHandler<UpdateEventAdministratorCommand, ICommandResult>
	{

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUserRepository _userRepository;
		private readonly IEventAdministratorRepository _eventAdministratorRepository;
		
		public EventAdministratorHandler(
			IUserRepository userRepository, 
			IEventAdministratorRepository eventAdministratorRepository, 
			IHttpContextAccessor httpContextAccessor
		)
		{
			_userRepository = userRepository;
			_eventAdministratorRepository = eventAdministratorRepository;
			_httpContextAccessor = httpContextAccessor;
		}
		
		public async Task<ICommandResult> Handle(CreateEventAdministratorCommand command)
		{			
			
			var administratorId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);

			//Verifica se o e-mail já está sendo utilizado
			if(await _userRepository.GetUserByEmail(command.Email) != null)
				AddNotification("email", "Este e-mail já está sendo utilizado");

			var eventAdministrator = new EventAdministrator(command.Name, command.Email, command.Password)
			{
				AdministratorId = administratorId,
			};
			
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
		
		public async Task<ICommandResult> Handle(UpdateEventAdministratorCommand command)
		{
			command.IsValid();
			
			var administratorId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);

			var eventAdministrator = await _eventAdministratorRepository.GetById(command.Id);
			if (eventAdministrator == null)
			{
				AddNotification("id", "Administrador de eventos não encontrado");
				return null;
			}

			if (!eventAdministrator.AdministratorId.Equals(administratorId))
			{
				AddNotification("message", "Você não tem permissão para excluir esse administrador de eventos");
				return null;
			}
			
			eventAdministrator.Name = command.Name;
			
			AddNotifications(command.Notifications);
			AddNotifications(eventAdministrator.Notifications);
			
			if (!IsValid())
				return null;
		
			_eventAdministratorRepository.Update(eventAdministrator);

			await _eventAdministratorRepository.Commit();
			
			return new UpdateEventAdministratorCommandResult{
				Id = eventAdministrator.Id,
				Name = eventAdministrator.Name,
				Email = eventAdministrator.Email,
				AdminidstratorId = eventAdministrator.AdministratorId
			};
		}

		public bool IsValid()
		{
			return Valid;
		}
	}
}