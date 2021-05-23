using Microsoft.AspNetCore.Mvc;
using PizzaStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.Components
{
	public class CartSummaryViewComponent : ViewComponent
	{
		private Cart cart;

		public CartSummaryViewComponent(Cart cartService)
		{
			cart = cartService;
		}

		public IViewComponentResult Invoke()
		{
			return View(cart);
		}
	}
}
