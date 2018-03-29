using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;

namespace EventSystem.Domain.Repositories
{
	public interface IEventRepository : IRepository<Event>
	{
		Task<Event> GetByIdWithPointOfSaleEvents(Guid id);
		Task<IList<Event>> GetAllWithPointOfSaleEvents();
	}
}