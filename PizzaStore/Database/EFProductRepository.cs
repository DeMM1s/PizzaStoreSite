using PizzaStore.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

		public void SaveProduct(Product product)
		{
			if (product.ProductID == 0)
			{
				context.Products.Add(product);
			}
			else
			{
				Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
				if (dbEntry != null)
				{
					dbEntry.Name = product.Name;
					dbEntry.Category = product.Category;
					dbEntry.Image = product.Image;
					dbEntry.Price = product.Price;
				}
			}
			context.SaveChanges();
		}

		public Product DeleteProduct(int productID)
		{
			Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == productID);
			if (dbEntry != null)
			{
				//доделать удаление с уже существующей связью
				context.Products.Remove(dbEntry);
				context.SaveChanges();
			}
			return dbEntry;
		}
	}
}
