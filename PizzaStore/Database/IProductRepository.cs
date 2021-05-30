using PizzaStore.Models;
using System.Linq;

namespace PizzaStore.Database
{
	public interface IProductRepository
	{
		IQueryable<Product> Products { get; }

		void SaveProduct(Product product);

		Product DeleteProduct(int productID);
	}
}
