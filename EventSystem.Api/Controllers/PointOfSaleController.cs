using System;
using System.Threading.Tasks;
using EventSystem.Domain.Commands.EventAdministratorCommands.Input;
using EventSystem.Domain.Commands.EventCommands.Input;
using EventSystem.Domain.Commands.PointOfSaleCommands.Input;
using EventSystem.Domain.Handlers;
using EventSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventSystem.Api.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class PointOfSaleController : Controller
	{
		private readonly IPointOfSaleRepository _pointOfSaleRepository;
		private readonly PointOfSaleHandler _handler;
		private readonly ILogger<PointOfSaleController> _logger;

		public PointOfSaleController(
			IPointOfSaleRepository pointOfSaleRepository, 
			PointOfSaleHandler handler, 
			ILogger<PointOfSaleController> logger
		)
		{
			_pointOfSaleRepository = pointOfSaleRepository;
			_handler = handler;
			_logger = logger;
		}

		[HttpPost]
		[Route("api/pointOfSale")]
		[Authorize(Roles = "Administrator, EventAdministrator")]
		public async Task<IActionResult> Post(CreatePointOfSaleCommand command)
		{
			var result = await _handler.Handle(command);
			if (!_handler.IsValid())
				return BadRequest(_handler.GetErrors());

			return Ok(result);
		}
		
		[HttpGet]
		[Route("api/pointOfSale/{id}")]
		[Authorize(Roles = "Administrator, EventAdministrator")]
		public async Task<IActionResult> Get(Guid id)
		{
			return Ok(await _pointOfSaleRepository.GetById(id));
		}
	
		[HttpGet]
		[Route("api/pointOfSale")]
		[Authorize(Roles = "Administrator, EventAdministrator")]
		public async Task<IActionResult> Get()
		{
			return Ok(await _pointOfSaleRepository.GetAll());
		}
	}
}