using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;

namespace EventSystem.Infra.Repositories
{
	public class BoxOfficeRepository : Repository<BoxOffice>, IBoxOfficeRepository
	{
		public BoxOfficeRepository(AppDbContext dbContext) : base(dbContext)
		{}
		
	}
}