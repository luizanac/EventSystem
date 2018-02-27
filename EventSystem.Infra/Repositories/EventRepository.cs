using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;

namespace EventSystem.Infra.Repositories
{
	public class EventRepository : Repository<Event>, IEventRepository
	{
		public EventRepository(AppDbContext dbContext) : base(dbContext)
		{}
		
	}
}