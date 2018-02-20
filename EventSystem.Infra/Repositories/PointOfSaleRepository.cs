using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventSystem.Infra.Repositories
{
	public class PointOfSaleRepository : Repository<PointOfSale>, IPointOfSaleRepository
	{
		
		public PointOfSaleRepository(AppDbContext dbContext) : base(dbContext)
		{}
		
		/*public async Task<PointOfSale> GetById(Guid id)
		{
			return await _dbContext.PointOfSales.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<IList<PointOfSale>> GetAll()
		{
			return await _dbContext.PointOfSales.ToListAsync();
		}

		public async Task Create(PointOfSale pointOfSale)
		{
			await _dbContext.PointOfSales.AddAsync(pointOfSale);
		}

		public async Task Commit()
		{
			await _dbContext.SaveChangesAsync();
		}*/
	}
}