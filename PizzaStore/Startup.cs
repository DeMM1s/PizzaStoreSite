using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaStore.Models;

namespace PizzaStore
{
	public class Startup
	{
		private IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration["Data:PizzaStoreProducts:ConnectionString"]));
			services.AddTransient<IProductRepository, EFProductRepository>();
			services.AddMvc();

		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseStatusCodePages();
			app.UseStaticFiles();
			app.UseMvc(route =>
			{
				route.MapRoute(
					name: null,
					template: "{category}/Page{productPage:int}",
					defaults: new { Controller = "Product", action = "List" }
				);
				route.MapRoute(
					name: null,
					template: "Page{productPage:int}",
					defaults: new { Controller = "Product", action = "List", productPage = 1 }
				);
				route.MapRoute(
					name: null,
					template: "{category}",
					defaults: new { Controller = "Product", action = "List", productPage = 1 }
				);
				route.MapRoute(
					name: null,
					template: "",
					defaults: new { Controller = "Product", action = "List", productPage = 1 }
				);
				route.MapRoute(
					name: null,
					template: "{controller=Product}/{action=List}/{id?}");
			});
			SeedData.EnsurePopulated(app);
		}
	}
}
