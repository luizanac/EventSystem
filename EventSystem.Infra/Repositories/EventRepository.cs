using System;
using System.Collections.Generic;
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
		
		public async Task<Event> GetByIdWithPointOfSaleEvents(Guid id)
		{
			return await DbSet
				.Include(e => e.PointOfSaleEvents)
				.ThenInclude(pse => pse.PointOfSale)
				.SingleOrDefaultAsync(e => e.Id == id);
		}

		public async Task<IList<Event>> GetAllWithPointOfSaleEvents()
		{
			return await DbSet
				.Include(e => e.PointOfSaleEvents)
				.ThenInclude(pse => pse.PointOfSale)
				.ToListAsync();
		}
	}
}