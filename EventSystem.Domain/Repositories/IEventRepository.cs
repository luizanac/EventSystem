using System;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;

namespace EventSystem.Domain.Repositories
{
	public interface IEventRepository : IRepository<Event>
	{
		Task<Event> GetByIdWithEvents(Guid id);
	}
}