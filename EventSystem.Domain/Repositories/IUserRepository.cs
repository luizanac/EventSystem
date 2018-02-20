using System;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;

namespace EventSystem.Domain.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> GetUserByEmailAndPassword(string email, string password);
		Task<User> GetUserByEmail(string email);
		Task Disable(Guid id);
		Task Enable(Guid id);
	}
}