using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaStore.Models;

namespace PizzaStore.Database
{
    public interface IOrderRepository
    {
	    IQueryable<Order> Orders { get; }
	    void SaveOrder(Order order);
    }
}
