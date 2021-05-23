using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Models;
using PizzaStore.Models.ViewModels;

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

		public ViewResult List(string category,int productPage = 1)
		{
			return View(new ProductsListViewModel
			{
				Products = repository.Products
					.Where(p => category == null || p.Category == category)
					.OrderBy(p =>p.ProductID)
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