using System.Linq;

namespace PizzaStore.Models
{
	public interface IProductRepository
	{
		IQueryable<Product> Products { get; }
	}
}
