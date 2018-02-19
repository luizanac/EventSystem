using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventSystem.Infra.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _dbContext;

		public UserRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public async Task<User> GetUserByEmailAndPassword(string email, string password)
		{
			var sha256 = SHA256.Create();
			var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
			var passwordHashed = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			
			return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHashed);
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task<User> GetById(Guid id)
		{
			return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task Disable(Guid id)
		{
			var user = await GetById(id);
			user.IsDisabled = true;
		}

		public async Task Commit()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}