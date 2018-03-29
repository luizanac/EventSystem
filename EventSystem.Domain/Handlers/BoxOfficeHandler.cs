using System.Threading.Tasks;
using EventSystem.Domain.Commands.BoxOfficeCommands.Input;
using EventSystem.Domain.Commands.BoxOfficeCommands.Output;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Shared.Commands;
using Microsoft.Extensions.Logging;

namespace EventSystem.Domain.Handlers
{
	public class BoxOfficeHandler :
		Handler, 
		ICommandHandler<CreateBoxOfficeCommand, ICommandResult>
	{

		private readonly IUserRepository _userRepository;
		private readonly IBoxOfficeRepository _boxOfficeRepository;
		
		public BoxOfficeHandler(
			IUserRepository userRepository, 
			IBoxOfficeRepository boxOfficeRepository
		)
		{
			_userRepository = userRepository;
			_boxOfficeRepository = boxOfficeRepository;
		}
		
		public async Task<ICommandResult> Handle(CreateBoxOfficeCommand command)
		{
			//Verifica se o e-mail já está sendo utilizado
			if(await _userRepository.GetUserByEmail(command.Email) != null)
				AddNotification("email", "Este e-mail já está sendo utilizado");

			var boxOffice = new BoxOffice(command.Name, command.Email, command.Password);
			
			AddNotifications(boxOffice.Notifications);
			
			if (!IsValid())
				return null;
		
			//Faz a persistencia no banco
			await _boxOfficeRepository.Create(boxOffice);

			await _boxOfficeRepository.Commit();
			
			//retorna o resultado para tela
			return new CreateBoxOfficeCommandResult(
				boxOffice.Id,
				boxOffice.Name,
				boxOffice.Email
			);
		}
		

		public bool IsValid()
		{
			return Valid;
		}
	}
}