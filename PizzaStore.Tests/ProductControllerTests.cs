using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzaStore.Controllers;
using PizzaStore.Database;
using PizzaStore.Models;
using PizzaStore.Models.ViewModels;
using System;
using System.Linq;
using Xunit;

namespace PizzaStore.Tests
{
	public class ProductControllerTests
	{
		[Fact]
		public void Can_Paginate()
		{
			//Организация
			Mock<IProductRepository> mock = new Mock<IProductRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID = 1, Name = "P1"},
				new Product {ProductID = 2, Name = "P2"},
				new Product {ProductID = 3, Name = "P3"},
				new Product {ProductID = 4, Name = "P4"},
				new Product {ProductID = 5, Name = "P5"}
			}).AsQueryable<Product>());
			ProductController controller = new ProductController(mock.Object) { pageSize = 3 };

			//Действие
			ProductsListViewModel page1Result = controller.List(null, 1).ViewData.Model as ProductsListViewModel;
			ProductsListViewModel page2Result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

			//Утверждение
			Product[] prodPage1 = page1Result.Products.ToArray();
			Product[] prodPage2 = page2Result.Products.ToArray();
			Assert.True(prodPage1.Length == 3);
			Assert.True(prodPage2.Length == 2);
			Assert.Equal("P1", prodPage1[0].Name);
			Assert.Equal("P2", prodPage1[1].Name);
			Assert.Equal("P3", prodPage1[2].Name);
			Assert.Equal("P4", prodPage2[0].Name);
			Assert.Equal("P5", prodPage2[1].Name);
		}

		[Fact]
		public void Can_Send_Pagination_View_Model()
		{
			//Организация
			Mock<IProductRepository> mock = new Mock<IProductRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID = 1, Name = "P1"},
				new Product {ProductID = 2, Name = "P2"},
				new Product {ProductID = 3, Name = "P3"},
				new Product {ProductID = 4, Name = "P4"},
				new Product {ProductID = 5, Name = "P5"}
			}).AsQueryable<Product>());
			ProductController controller = new ProductController(mock.Object) { pageSize = 3 };

			//Действие
			ProductsListViewModel page1Result = controller.List(null, 1).ViewData.Model as ProductsListViewModel;
			ProductsListViewModel page2Result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

			//Утверждение
			PagingInfo page1Info = page1Result.PagingInfo;
			PagingInfo page2Info = page2Result.PagingInfo;
			Assert.Equal(1, page1Info.CurrentPage);
			Assert.Equal(3, page1Info.ItemsPerPages);
			Assert.Equal(5, page1Info.TotalItems);
			Assert.Equal(2, page1Info.TotalPages);

			Assert.Equal(2, page2Info.CurrentPage);
			Assert.Equal(3, page2Info.ItemsPerPages);
			Assert.Equal(5, page2Info.TotalItems);
			Assert.Equal(2, page2Info.TotalPages);
		}

		[Fact]
		public void Generate_Category_Specific_Product_Count()
		{
			//Организация
			Mock<IProductRepository> mock = new Mock<IProductRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
				new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
				new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
				new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
				new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
			}).AsQueryable<Product>());
			ProductController controller = new ProductController(mock.Object) { pageSize = 3 };

			Func<ViewResult, ProductsListViewModel> getModel = result => result?.ViewData?.Model as ProductsListViewModel;

			//Действие
			int? res1 = getModel(controller.List("Cat1"))?.PagingInfo.TotalItems;
			int? res2 = getModel(controller.List("Cat2"))?.PagingInfo.TotalItems;
			int? res3 = getModel(controller.List("Cat3"))?.PagingInfo.TotalItems;
			int? resAll = getModel(controller.List(null))?.PagingInfo.TotalItems;

			//Утверждение
			Assert.Equal(2, res1);
			Assert.Equal(2, res2);
			Assert.Equal(1, res3);
			Assert.Equal(5, resAll);
		}
	}
}
