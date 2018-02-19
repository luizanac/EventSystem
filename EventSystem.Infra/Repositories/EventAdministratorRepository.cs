using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventSystem.Infra.Repositories
{
	public class EventAdministratorRepository : IEventAdministratorRepository
	{
		private readonly AppDbContext _dbContext;

		public EventAdministratorRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public Task<EventAdministrator> GetById(Guid id)
		{
			return _dbContext.EventAdministrators.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<IList<EventAdministrator>> GetAll()
		{
			return await _dbContext.EventAdministrators.ToListAsync();
		}

		public async Task Create(EventAdministrator eventAdministrator)
		{
			await _dbContext.EventAdministrators.AddAsync(eventAdministrator);
		}

		public async Task Commit()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}