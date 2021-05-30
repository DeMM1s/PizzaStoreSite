using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
	public class Product
	{
		public int ProductID { get; set; }
		[Required(ErrorMessage = "Введите название товара")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Добавьте изображение товара")]
		public string Image { get; set; }
		[Required]
		[Range(0.01, double.MaxValue,ErrorMessage = "Введите положительное число")]
		public decimal Price { get; set; }
		[Required(ErrorMessage = "Укажите категорию товара")]
		public string Category { get; set; }
	}
}
