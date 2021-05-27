using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzaStore.Controllers;
using PizzaStore.Database;
using PizzaStore.Models;
using Xunit;

namespace PizzaStore.Tests
{
	public class OrderControllerTests
	{
		[Fact]
		public void Cannot_Checkout_Empty_Cart()
		{
			//Организация
			Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
			Cart cart = new Cart();
			Order order = new Order();
			OrderController target = new OrderController(mock.Object, cart);

			//Действие
			ViewResult result = target.Checkout(order) as ViewResult;

			//Утверждение
			mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
			Assert.True(string.IsNullOrEmpty(result.ViewName));
			Assert.False(result.ViewData.ModelState.IsValid);
		}

		[Fact]
		public void Cannot_Checkout_Invalid_DeliveryDetails()
		{
			//Организация
			Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
			Cart cart = new Cart();
			cart.AddItems(new Product(), 1);
			Order order = new Order();
			OrderController target = new OrderController(mock.Object, cart);
			target.ModelState.AddModelError("error", "error");

			//Действие
			ViewResult result = target.Checkout(order) as ViewResult;

			//Утверждение
			mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
			Assert.True(string.IsNullOrEmpty(result.ViewName));
			Assert.False(result.ViewData.ModelState.IsValid);
		}

		[Fact]
		public void Can_Checkout_And_Submit_Order()
		{
			//Организация
			Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
			Cart cart = new Cart();
			cart.AddItems(new Product(), 1);
			Order order = new Order();
			OrderController target = new OrderController(mock.Object, cart);

			//Действие
			RedirectToActionResult result = target.Checkout(order) as RedirectToActionResult;

			//Утверждение
			mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
			Assert.Equal("Completed", result.ActionName);
		}
	}
}
