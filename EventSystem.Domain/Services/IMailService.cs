using System.Collections.Generic;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;

namespace EventSystem.Domain.Services
{
	public interface IMailService
	{
		void Send(IList<User> to, string subject, string content);
		void Send(User to, string subject, string content);

	}
}