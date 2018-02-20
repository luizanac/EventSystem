using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSystem.Shared.Entities;

namespace EventSystem.Domain.Repositories
{
	public interface IRepository<T> where T : Entity
	{
		Task<T> GetById(Guid id);
		Task<IList<T>> GetAll();
		Task Create(T t);
		void Update(T t);
		void Remove(T t);
		Task Commit();
	}
}