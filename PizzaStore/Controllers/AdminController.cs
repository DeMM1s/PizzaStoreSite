using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Database;
using PizzaStore.Models;
using System.Linq;

namespace PizzaStore.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private IProductRepository repository;

		public AdminController(IProductRepository repo)
		{
			repository = repo;
		}

		public ViewResult Index()
		{
			return View(repository.Products);
		}

		public ViewResult Edit(int productId)
		{
			return View(repository.Products.FirstOrDefault(p => p.ProductID == productId));
		}

		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				repository.SaveProduct(product);
				TempData["message"] = $"Товар {product.Name} был сохранен";
				return RedirectToAction("Index");
			}
			else
			{
				return View(product);
			}
		}

		public ViewResult Create()
		{
			return View("Edit", new Product());
		}

		[HttpPost]
		public IActionResult Delete(int productId)
		{
			Product deletedProduct = new Product();
			deletedProduct = repository.DeleteProduct(productId);
			if (deletedProduct != null)
			{
				TempData["message"] = $"Товар {deletedProduct.Name} был удален из базы";
			}

			return RedirectToAction("Index");
		}
	}
}
