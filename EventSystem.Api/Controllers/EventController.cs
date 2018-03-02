
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EventSystem.Domain.Commands.EventCommands.Input;
using EventSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventHandler = EventSystem.Domain.Handlers.EventHandler;

namespace EventSystem.Api.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class EventController : Controller
	{

		private readonly IEventRepository _eventRepository;
		private readonly EventHandler _handler;
		private readonly ILogger<EventController> _logger;
		
		
		public EventController(IEventRepository eventRepository, EventHandler handler, ILogger<EventController> logger)
		{
			_eventRepository = eventRepository;
			_handler = handler;
			_logger = logger;
		}
		
		[HttpPost]
		[Route("api/event")]
		[Authorize(Roles = "EventAdministrator")]
		public async Task<IActionResult> Post(CreateEventCommand command)
		{
			var result = await _handler.Handle(command);
			if (!_handler.IsValid())
				return BadRequest(_handler.GetErrors());

			return Ok(result);
		}
		
		[HttpGet]
		[Route("api/event/{id}")]
		[Authorize(Roles = "EventAdministrator")]
		public async Task<IActionResult> Get(Guid id)
		{
			return Ok(await _eventRepository.GetById(id));
		}
		
		
		[HttpDelete]
		[Route("api/event/{id}")]
		[Authorize(Roles = "EventAdministrator")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var mEvent = await _eventRepository.GetById(id);
			_eventRepository.Remove(mEvent);
			await _eventRepository.Commit();
			return Ok();
		}
	
		[HttpGet]
		[Route("api/event")]
		[Authorize(Roles = "EventAdministrator")]
		public async Task<IActionResult> Get()
		{
			return Ok(await _eventRepository.GetAll());
		}
		
	}
}