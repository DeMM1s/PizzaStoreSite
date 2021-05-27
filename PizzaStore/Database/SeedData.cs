using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaStore.Models;
using System.Linq;

namespace PizzaStore.Database
{
	public static class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
			context.Database.Migrate();

			if (!context.Products.Any())
			{
				context.Products.AddRange(
					new Product
					{
						Category = "Пицца",
						Name = "Пепперони",
						Image = "https://cdn7.arora.pro/c/upload/2b70575c-d540-4719-8b64-21b5b42ddca5/original//c1dd3858-5eb0-4710-96ef-ab5700c72f72.jpg",
						Price = 289M
					},
					new Product
					{
						Category = "Пицца",
						Name = "4 сыра",
						Image = "https://cdn7.arora.pro/c/upload/2b70575c-d540-4719-8b64-21b5b42ddca5/original//bcc41443-f6b4-4626-a105-ac8600d1bba2.jpg",
						Price = 289M
					},
					new Product
					{
						Category = "Пицца",
						Name = "Мясная",
						Image = "https://cdn10.arora.pro/c/upload/2b70575c-d540-4719-8b64-21b5b42ddca5/original//ae4be268-4367-4fae-b41a-ac1e00b95e9a.jpg",
						Price = 289M
					},
				new Product
				{
					Category = "Напитки",
					Name = "Пепси",
					Image = "https://cdn10.arora.pro/c/upload/2b70575c-d540-4719-8b64-21b5b42ddca5/original//50e2ad05-b710-4287-be3f-ad2d00c1f8ef.png",
					Price = 89M
				},
				new Product
				{
					Category = "Напитки",
					Name = "Миринда",
					Image = "https://cdn4.arora.pro/c/upload/2b70575c-d540-4719-8b64-21b5b42ddca5/original//62f28f97-129b-447d-96cb-ad2d00c21782.png",
					Price = 89M
				},
				new Product
				{
					Category = "Напитки",
					Name = "7up",
					Image = "https://cdn8.arora.pro/c/upload/2b70575c-d540-4719-8b64-21b5b42ddca5/original//f590f6f7-6ca5-4e05-adad-ad2d00c22ecf.png",
					Price = 89M
				},
				new Product
				{
					Category = "Соусы",
					Name = "Сырный",
					Image = "https://cdn10.arora.pro/c/upload/2b70575c-d540-4719-8b64-21b5b42ddca5/original//770fe53f-22f0-4675-8e82-ab20004a9bac.png",
					Price = 89M
				},
				new Product
				{
					Category = "Соусы",
					Name = "Кисло-сладкий",
					Image = "https://cdn9.arora.pro/c/upload/2b70575c-d540-4719-8b64-21b5b42ddca5/original//a4d7c978-896c-4310-9a6c-ab20004a63e4.png",
					Price = 89M
				},
				new Product
				{
					Category = "Соусы",
					Name = "Карри",
					Image = "https://cdn10.arora.pro/c/upload/2b70575c-d540-4719-8b64-21b5b42ddca5/original//314ffb59-7c3c-4940-9560-ad2d00c2bf74.png",
					Price = 89M
				});
				context.SaveChanges();
			}
		}
	}
}
