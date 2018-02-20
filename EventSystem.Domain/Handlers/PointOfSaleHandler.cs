using System.Threading.Tasks;
using EventSystem.Domain.Commands.PointOfSaleCommands.Input;
using EventSystem.Domain.Commands.PointOfSaleCommands.Output;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Shared.Commands;
using Microsoft.Extensions.Logging;

namespace EventSystem.Domain.Handlers
{
	public class PointOfSaleHandler :
		Handler,
		ICommandHandler<CreatePointOfSaleCommand>
	{

		private readonly IUserRepository _userRepository;
		private readonly IPointOfSaleRepository _pointOfSaleRepository;
		private readonly ILogger<PointOfSaleHandler> _logger;
		
		public PointOfSaleHandler(IUserRepository userRepository, IPointOfSaleRepository pointOfSaleRepository, ILogger<PointOfSaleHandler> logger)
		{
			_userRepository = userRepository;
			_pointOfSaleRepository = pointOfSaleRepository;
			_logger = logger;
		}
		
		public async Task<ICommandResult> Handle(CreatePointOfSaleCommand command)
		{
			//Verifica se o e-mail já está sendo utilizado
			if(await _userRepository.GetUserByEmail(command.Email) != null)
				AddNotification("email", "Este e-mail já está sendo utilizado");

			var pointOfSale = new PointOfSale(command.Name, command.Email, command.Password, command.Cnpj, command.Phone);
			
			//Valida o dominio
			AddNotifications(pointOfSale.Notifications);
			
			//Valida o handler
			if (!IsValid())
				return null;
		
			//Faz a persistencia no banco
			await _pointOfSaleRepository.Create(pointOfSale);

			await _pointOfSaleRepository.Commit();
			
			//retorna o resultado para tela
			return new CreatePointOfSaleCommandResult(
				pointOfSale.Id,
				pointOfSale.Name,
				pointOfSale.Email,
				pointOfSale.Cnpj,
				pointOfSale.Phone
			);
		}
		
		public bool IsValid()
		{
			return Valid;
		}

	}
}