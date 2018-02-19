using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;

namespace EventSystem.Domain.Handlers
{
	public class Handler : Notifiable
	{
		public Dictionary<string, List<string>> GetErrors()
		{
			var errors = new Dictionary<string, List<string>>();
			foreach (var notification in Notifications)
			{
				if (errors.ContainsKey(notification.Property))
				{
					var list = errors[notification.Property];
					list.Add(notification.Message);
				}
				else
				{
					errors.Add(notification.Property, new List<string>()
					{
						notification.Message
					});
				}
			}

			return errors;
		}
	}
}