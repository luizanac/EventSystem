
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventSystem.Domain.Commands.EventCommands.Input;
using EventSystem.Domain.Commands.EventCommands.Output;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventHandler = EventSystem.Domain.Handlers.EventHandler;

namespace EventSystem.Api.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, EventAdministrator")]
	public class EventController : Controller
	{

		private readonly IEventRepository _eventRepository;
		private readonly EventHandler _handler;
		
		
		public EventController(IEventRepository eventRepository, EventHandler handler)
		{
			_eventRepository = eventRepository;
			_handler = handler;
		}
		
		[HttpPost]
		[Route("api/event")]
		public async Task<IActionResult> Post(CreateEventCommand command)
		{
			var result = await _handler.Handle(command);
			if (!_handler.IsValid())
				return BadRequest(_handler.GetErrors());

			return Ok(result);
		}
		
		[HttpGet]
		[Route("api/event/{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			return Ok(await _eventRepository.GetByIdWithPointOfSaleEvents(id));
		}
		
		
		[HttpDelete]
		[Route("api/event/{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var mEvent = await _eventRepository.GetById(id);
			_eventRepository.Remove(mEvent);
			await _eventRepository.Commit();
			return Ok();
		}
	
		[HttpGet]
		[Route("api/event")]
		public async Task<IActionResult> Get()
		{
			return Ok(await _eventRepository.GetAllWithPointOfSaleEvents());
		}
		
		[HttpPost]
		[Route("api/event/{eventId}/pointOfSale")]
		public async Task<IActionResult> AddPointOfSaleInEvent(AddPointOfSaleInEventCommand command)
		{
			var result = await _handler.Handle(command);
			if (!_handler.IsValid())
				return BadRequest(_handler.GetErrors());

			return Ok(result);
		}
		
	}
}