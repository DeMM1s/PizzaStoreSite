using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using PizzaStore.Components;
using PizzaStore.Database;
using PizzaStore.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PizzaStore.Tests
{
	public class NavigationMenuViewComponentTests
	{
		[Fact]
		public void Can_Selected_Category()
		{
			//Организация
			Mock<IProductRepository> mock = new Mock<IProductRepository>();
			mock.Setup(m => m.Products).Returns(new Product[]
			{
				new Product {ProductID = 1, Name = "P1", Category = "Apples"},
				new Product {ProductID = 2, Name = "P2", Category = "Apples"},
				new Product {ProductID = 3, Name = "P3", Category = "Plums"},
				new Product {ProductID = 4, Name = "P4", Category = "Oranges"}
			}.AsQueryable<Product>);

			NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

			//Действие
			string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

			//Утверждение
			Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
		}

		[Fact]
		public void Indicated_Selected_Category()
		{
			//Организация
			string categoryToSelect = "Apples";
			Mock<IProductRepository> mock = new Mock<IProductRepository>();
			mock.Setup(m => m.Products).Returns(new Product[]
			{
				new Product {ProductID = 1, Name = "P1", Category = "Apples"},
				new Product {ProductID = 4, Name = "P2", Category = "Oranges"}
			}.AsQueryable<Product>);

			NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
			target.ViewComponentContext = new ViewComponentContext
			{
				ViewContext = new ViewContext
				{
					RouteData = new RouteData()
				}
			};
			target.RouteData.Values["category"] = categoryToSelect;

			//Действие
			string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

			//Утверждение
			Assert.Equal(categoryToSelect, result);
		}
	}
}
