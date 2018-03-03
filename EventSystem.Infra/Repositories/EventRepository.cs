using System;
using System.Linq;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventSystem.Infra.Repositories
{
	public class EventRepository : Repository<Event>, IEventRepository
	{
		public EventRepository(AppDbContext dbContext) : base(dbContext)
		{}
		
		public async Task<Event> GetByIdWithEvents(Guid id)
		{
			return await DbSet
				.Include(e => e.PointOfSaleEvents)
				.SingleOrDefaultAsync(e => e.Id == id);
		}
	}
}