using PizzaStore.Models;
using System.Linq;

namespace PizzaStore.Database
{
	public interface IOrderRepository
	{
		IQueryable<Order> Orders { get; }

		void SaveOrder(Order order);
	}
}
