using System;
using System.Threading.Tasks;
using EventSystem.Domain.Commands.BoxOfficeCommands.Input;
using EventSystem.Domain.Handlers;
using EventSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventSystem.Api.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, EventAdministrator")]
	public class BoxOfficeController : Controller
	{
		private readonly IBoxOfficeRepository _boxOfficeRepository;
		private readonly BoxOfficeHandler _handler;

		public BoxOfficeController(
			IBoxOfficeRepository boxOfficeRepository, 
			BoxOfficeHandler handler
		)
		{
			_boxOfficeRepository = boxOfficeRepository;
			_handler = handler;
		}

		[HttpPost]
		[Route("api/boxOffice")]
		public async Task<IActionResult> Post(CreateBoxOfficeCommand command)
		{
			var result = await _handler.Handle(command);
			if (!_handler.IsValid())
				return BadRequest(_handler.GetErrors());

			return Ok(result);
		}
		
		[HttpGet]
		[Route("api/boxOffice/{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			return Ok(await _boxOfficeRepository.GetById(id));
		}
	
		[HttpDelete]
		[Route("api/boxOffice/{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var boxOffice = await _boxOfficeRepository.GetById(id);
			_boxOfficeRepository.Remove(boxOffice);
			await _boxOfficeRepository.Commit();
			return Ok();
		}
		
		[HttpGet]
		[Route("api/boxOffice")]
		public async Task<IActionResult> Get()
		{
			return Ok(await _boxOfficeRepository.GetAll());
		}
	}
}