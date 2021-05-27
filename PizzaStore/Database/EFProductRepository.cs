using PizzaStore.Models;
using System.Linq;

namespace PizzaStore.Database
{
	public class EFProductRepository : IProductRepository
	{
		private ApplicationDbContext context;

		public EFProductRepository(ApplicationDbContext ctx)
		{
			context = ctx;
		}

		public IQueryable<Product> Products => context.Products;
	}
}
