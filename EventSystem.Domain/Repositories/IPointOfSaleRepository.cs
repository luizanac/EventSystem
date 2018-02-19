using System.Threading.Tasks;

namespace EventSystem.Domain.Repositories
{
	public interface IPointOfSaleRepository
	{
		Task Commit();
	}
}