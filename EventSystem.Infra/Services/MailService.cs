using System.Collections.Generic;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Services;
using Newtonsoft.Json;

namespace EventSystem.Infra.Services
{
	public class MailService 
	{
	
		/*public async Task Send(User to, string subject, string content)
		{
			var message = new
			{
				from = new
				{
					email = "endrigo@ubistart.com",
					name = "SIMPLO"
				},
				to = new List<object>
				{
					new
					{
						email = to.Email,
						name = to.Name	
					}
				},
				subject = subject,
				body = content
			};

		}
		
		public v Send(IList<User> to, string subject, string content)
		{
			var message = new
			{
				from = new
				{
					email = "endrigo@ubistart.com",
					name = "SIMPLO"
				},
				to = new List<object>(),
				subject = subject,
				body = content
			};

			foreach (var user in to)
			{
				message.to.Add(new
				{
					email = user.Email,
					name = user.Name
				});
			}
			
		}*/
	}
}