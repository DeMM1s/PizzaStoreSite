using PizzaStore.Models;
using System.Linq;
using Xunit;

namespace PizzaStore.Tests
{
	public class CartTests
	{
		[Fact]
		public void Can_Add_New_Lines()
		{
			//Организация
			Product p1 = new Product { ProductID = 1, Name = "P1" };
			Product p2 = new Product { ProductID = 2, Name = "P2" };

			Cart target = new Cart();

			//Действие
			target.AddItems(p1, 1);
			target.AddItems(p2, 1);
			CartLine[] results = target.Lines.ToArray();

			//Утверждение
			Assert.Equal(2, results.Length);
			Assert.Equal(p1, results[0].Product);
			Assert.Equal(p2, results[1].Product);
		}

		[Fact]
		public void Can_Add_Quantity_For_Existing_Lines()
		{
			//Организация
			Product p1 = new Product { ProductID = 1, Name = "P1" };
			Product p2 = new Product { ProductID = 2, Name = "P2" };

			Cart target = new Cart();

			//Действие
			target.AddItems(p1, 1);
			target.AddItems(p2, 1);
			target.AddItems(p1, 10);
			CartLine[] results = target.Lines.ToArray();

			//Утверждение
			Assert.Equal(2, results.Length);
			Assert.Equal(11, results[0].Quantity);
			Assert.Equal(1, results[1].Quantity);
		}

		[Fact]
		public void Can_Remove_Lines()
		{
			//Организация
			Product p1 = new Product { ProductID = 1, Name = "P1" };
			Product p2 = new Product { ProductID = 2, Name = "P2" };
			Product p3 = new Product { ProductID = 3, Name = "P3" };

			Cart target = new Cart();

			target.AddItems(p1, 1);
			target.AddItems(p2, 2);
			target.AddItems(p3, 3);
			target.AddItems(p2, 3);

			//Действие
			target.RemoveLine(p2);

			//Утверждение
			Assert.Empty(target.Lines.Where(l => l.Product == p2));
			Assert.Equal(2, target.Lines.Count());
		}

		[Fact]
		public void Can_Calculate_Cart_Total()
		{
			//Организация
			Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
			Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

			Cart target = new Cart();

			//Действие
			target.AddItems(p1, 1);
			target.AddItems(p2, 3);
			target.AddItems(p1, 5);
			decimal result = target.ComputeTotalValue();

			//Утверждение
			Assert.Equal(750M, result);
		}

		[Fact]
		public void Can_Clear_Cart()
		{
			//Организация
			Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
			Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

			Cart target = new Cart();

			//Действие
			target.AddItems(p1, 1);
			target.AddItems(p2, 3);
			target.Clear();

			//Утверждение
			Assert.Empty(target.Lines);
		}
	}
}
