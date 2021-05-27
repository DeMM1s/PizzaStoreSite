using Microsoft.AspNetCore.Mvc;
using PizzaStore.Database;
using PizzaStore.Models.ViewModels;
using System.Linq;

namespace PizzaStore.Controllers
{
	public class ProductController : Controller
	{
		private IProductRepository repository;
		public int pageSize = 2;

		public ProductController(IProductRepository repo)
		{
			repository = repo;
		}

		public ViewResult List(string category, int productPage = 1)
		{
			return View(new ProductsListViewModel
			{
				Products = repository.Products
					.Where(p => category == null || p.Category == category)
					.OrderBy(p => p.ProductID)
					.Skip((productPage - 1) * pageSize)
					.Take(pageSize),
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPages = pageSize,
					TotalItems = category == null ?
						repository.Products.Count() :
						repository.Products.Where(p => p.Category == category).Count()
				},
				CurrentCategory = category
			});
		}
	}
}