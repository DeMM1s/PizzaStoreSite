using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Database;
using PizzaStore.Models;
using System.Linq;

namespace PizzaStore.Controllers
{
	public class OrderController : Controller
	{
		private IOrderRepository repository;
		private Cart cart;

		public OrderController(IOrderRepository rep, Cart cartService)
		{
			repository = rep;
			cart = cartService;
		}

		[Authorize]
		public ViewResult List()
		{
			return View(repository.Orders.Where(o => !o.Delivered));
		}

		[HttpPost]
		[Authorize]
		public IActionResult MarkDelivered(int orderID)
		{
			Order order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
			if (order != null)
			{
				order.Delivered = true;
				repository.SaveOrder(order);
			}

			return View("List", repository.Orders);
		}

		public ViewResult Checkout()
		{
			return View(new Order());
		}

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			if (cart.Lines.Count() == 0)
			{
				{
					ModelState.AddModelError("", "Корзина пуста!");
				}
			}
			if (ModelState.IsValid)
			{
				order.Lines = cart.Lines.ToArray();
				repository.SaveOrder(order);
				return RedirectToAction(nameof(Completed));
			}
			else
			{
				return View(order);
			}
		}

		public ViewResult Completed()
		{
			cart.Clear();
			return View();
		}
	}
}
