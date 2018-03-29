using System.Collections.Generic;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;

namespace EventSystem.Domain.Services
{
	public interface IMailService
	{
		Task Send(IList<User> to, string subject, string content);
		Task Send(User to, string subject, string content);

	}
}