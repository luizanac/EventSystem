using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;

namespace EventSystem.Domain.Repositories
{
	public interface IEventAdministratorRepository
	{
		Task<EventAdministrator> GetById(Guid id);
		Task<IList<EventAdministrator>> GetAll();
		Task Create(EventAdministrator eventAdministrator);
		Task Commit();
	}
}