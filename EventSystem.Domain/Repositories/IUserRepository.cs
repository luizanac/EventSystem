using System;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;

namespace EventSystem.Domain.Repositories
{
	public interface IUserRepository
	{
		Task<User> GetUserByEmailAndPassword(string email, string password);
		Task<User> GetUserByEmail(string email);
		Task<User> GetById(Guid id);
		Task Disable(Guid id);
		Task Commit();

	}
}