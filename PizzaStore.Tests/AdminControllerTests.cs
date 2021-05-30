using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using PizzaStore.Controllers;
using PizzaStore.Database;
using PizzaStore.Models;
using Xunit;

namespace PizzaStore.Tests
{
    public class AdminControllerTests
    {
	    [Fact]
	    public void Index_Contains_All_Products()
	    {
			//Организация
		    Mock<IProductRepository> mock = new Mock<IProductRepository>();
		    mock.Setup(m => m.Products).Returns(new Product[]
		    {
			    new Product {ProductID = 1, Name = "P1"},
			    new Product {ProductID = 2, Name = "P2"},
			    new Product {ProductID = 3, Name = "P3"}
		    }.AsQueryable<Product>);
		    AdminController target = new AdminController(mock.Object);

		    //Действие
		    Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

		    //Утверждение
			Assert.Equal(3,result.Length);
			Assert.Equal("P1", result[0].Name);
			Assert.Equal("P2", result[1].Name);
			Assert.Equal("P3", result[2].Name);
	    }

	    [Fact]
	    public void Can_Edit_Product()
	    {
		    //Организация
		    Mock<IProductRepository> mock = new Mock<IProductRepository>();
		    mock.Setup(m => m.Products).Returns(new Product[]
		    {
			    new Product {ProductID = 1, Name = "P1"},
			    new Product {ProductID = 2, Name = "P2"},
			    new Product {ProductID = 3, Name = "P3"}
		    }.AsQueryable<Product>);
		    AdminController target = new AdminController(mock.Object);

		    //Действие
		    Product p1 = GetViewModel<Product>(target.Edit(1));
		    Product p2 = GetViewModel<Product>(target.Edit(2));
		    Product p3 = GetViewModel<Product>(target.Edit(3));

			//Утверждение
			Assert.Equal(1, p1.ProductID);
		    Assert.Equal(2, p2.ProductID);
		    Assert.Equal(3, p3.ProductID);
	    }

		[Fact]
	    public void Cannot_Edit_Nonexisted_Product()
	    {
		    //Организация
		    Mock<IProductRepository> mock = new Mock<IProductRepository>();
		    mock.Setup(m => m.Products).Returns(new Product[]
		    {
			    new Product {ProductID = 1, Name = "P1"},
			    new Product {ProductID = 2, Name = "P2"},
			    new Product {ProductID = 3, Name = "P3"}
		    }.AsQueryable<Product>);
		    AdminController target = new AdminController(mock.Object);

		    //Действие
		    Product p4 = GetViewModel<Product>(target.Edit(4));

		    //Утверждение
			Assert.Null(p4);
	    }

	    [Fact]
	    public void Can_Saves_Valid_Changes()
	    {
			//Организация
		    Mock<IProductRepository> mock = new Mock<IProductRepository>();
		    Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
		    AdminController target = new AdminController(mock.Object)
		    {
				TempData = tempData.Object
		    };
		    Product product = new Product {Name = "Test"};

		    //Действие
		    IActionResult result = target.Edit(product);

		    //Утверждение
			mock.Verify(m =>m.SaveProduct(product));
			Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
	    }

	    [Fact]
	    public void Cannot_Saves_Invalid_Changes()
	    {
			//Организация
			Mock<IProductRepository> mock = new Mock<IProductRepository>();
			AdminController target = new AdminController(mock.Object);
			Product product = new Product { Name = "Test" };
			target.ModelState.AddModelError("error", "error");

			//Действие
			IActionResult result = target.Edit(product);

			//Утверждение
			mock.Verify(m => m.SaveProduct(It.IsAny<Product>()),Times.Never);
			Assert.IsType<ViewResult>(result);
	    }

	    [Fact]
	    public void Can_Delete_Valid_Product()
	    {
			//Организация
			Product product = new Product { ProductID = 2, Name = "P2" };
			Mock<IProductRepository> mock = new Mock<IProductRepository>();
			mock.Setup(m => m.Products).Returns(new Product[]
			{
				new Product {ProductID = 1, Name = "P1"},
				product,
				new Product {ProductID = 3, Name = "p3"}
			}.AsQueryable<Product>);
		    AdminController target = new AdminController(mock.Object);

		    //Действие
		    target.Delete(product.ProductID);

		    //Утверждение
		    mock.Verify(m => m.DeleteProduct(product.ProductID));
	    }

		private T GetViewModel<T>(IActionResult result) where T : class
	    {
		    return (result as ViewResult)?.ViewData.Model as T;
	    }
	}
}
