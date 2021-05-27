using Microsoft.AspNetCore.Mvc;
using PizzaStore.Database;
using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.Components
{
	public class NavigationMenuViewComponent : ViewComponent
	{
		private IProductRepository repository;

		public NavigationMenuViewComponent(IProductRepository repo)
		{
			repository = repo;
		}

		public IViewComponentResult Invoke()
		{
			ViewBag.SelectedCategory = RouteData?.Values["category"];
			return View(repository.Products
				.Select(x => x.Category)
				.Distinct()
				.OrderBy(x => x));
		}
	}
}
