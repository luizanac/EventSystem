using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using EventSystem.Domain.Commands.EventCommands.Input;
using EventSystem.Domain.Commands.PointOfSaleCommands.Input;
using EventSystem.Domain.Commands.PointOfSaleCommands.Output;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Shared.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EventSystem.Domain.Handlers
{
	public class PointOfSaleHandler :
		Handler,
		ICommandHandler<CreatePointOfSaleCommand, ICommandResult>
	{

		private readonly IUserRepository _userRepository;
		private readonly IPointOfSaleRepository _pointOfSaleRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ILogger<PointOfSaleHandler> _logger;
		
		public PointOfSaleHandler(IUserRepository userRepository, IPointOfSaleRepository pointOfSaleRepository, ILogger<PointOfSaleHandler> logger, IHttpContextAccessor httpContextAccessor)
		{
			_userRepository = userRepository;
			_pointOfSaleRepository = pointOfSaleRepository;
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}
		
		public async Task<ICommandResult> Handle(CreatePointOfSaleCommand command)
		{

			var id = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
			
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