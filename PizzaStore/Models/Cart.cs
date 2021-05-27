using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.Models
{
	public class Cart
	{
		private List<CartLine> lineCollection = new List<CartLine>();

		public virtual IEnumerable<CartLine> Lines => lineCollection;

		public virtual void AddItems(Product product, int quantity)
		{
			CartLine line = lineCollection
				.Where(p => p.Product.ProductID == product.ProductID)
				.FirstOrDefault();
			if (line == null)
			{
				lineCollection.Add(new CartLine
				{
					Product = product,
					Quantity = quantity
				});
			}
			else
			{
				line.Quantity += quantity;
			}
		}

		public virtual decimal ComputeTotalValue()
		{
			return lineCollection.Sum(e => e.Product.Price * e.Quantity);
		}

		public virtual void RemoveLine(Product product)
		{
			lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
		}

		public virtual void Clear()
		{
			lineCollection.Clear();
		}
	}

	public class CartLine
	{
		public int Id { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}
}
