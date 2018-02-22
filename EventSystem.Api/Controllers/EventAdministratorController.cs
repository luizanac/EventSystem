using System;
using System.Threading.Tasks;
using EventSystem.Domain.Commands.EventAdministratorCommands.Input;
using EventSystem.Domain.Handlers;
using EventSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventSystem.Api.Controllers
{
	public class EventAdministratorController : Controller
	{
		private readonly IEventAdministratorRepository _eventAdministratorRepository;
		private readonly EventAdministratorHandler _handler;
		private readonly ILogger<EventAdministratorController> _logger;
		
		public EventAdministratorController(IEventAdministratorRepository eventAdministratorRepository, EventAdministratorHandler handler, ILogger<EventAdministratorController> logger)
		{
			_eventAdministratorRepository = eventAdministratorRepository;
			_handler = handler;
			_logger = logger;
		}		
		
		[HttpPost]
		[Route("api/eventAdministrator")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
		public async Task<IActionResult> Post([FromBody] CreateEventAdministratorCommand command)
		{
			
			var result = await _handler.Handle(command);
			if (!_handler.IsValid())
				return BadRequest(_handler.GetErrors());

			return Ok(result);
		}
		
		[HttpGet]
		[Route("api/eventAdministrator/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
		public async Task<IActionResult> Get(Guid id)
		{
			return Ok(await _eventAdministratorRepository.GetById(id));
		}
	
		[HttpGet]
		[Route("api/eventAdministrator")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
		public async Task<IActionResult> Get()
		{
			return Ok(await _eventAdministratorRepository.GetAll());
		}
	
	}
}