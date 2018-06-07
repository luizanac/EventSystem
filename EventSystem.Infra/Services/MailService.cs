using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EventSystem.Infra.Services
{
	public class MailService : IMailService
	{

		private readonly IConfiguration _configuration;
		private readonly SmtpClient _client;

		public MailService(IConfiguration configuration)
		{
			_configuration = configuration;
			_client = new SmtpClient(_configuration["Client"], Int16.Parse(_configuration["Port"]))
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(_configuration["User"], _configuration["Password"])
			};
		}
		
		public void Send(IList<User> to, string subject, string content)
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
		}

		public void Send(User to, string subject, string content)
		{
			var mailMessage = new MailMessage
			{
				From = new MailAddress(_configuration["From"])
			};
			mailMessage.To.Add(to.Email);
			mailMessage.Body = content;
			mailMessage.Subject = "EVENTOS: " +  subject;
			_client.Send(mailMessage);		
		}
	}
}