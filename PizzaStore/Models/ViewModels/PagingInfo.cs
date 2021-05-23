using System;

namespace PizzaStore.Models.ViewModels
{
	public class PagingInfo
	{
		public int TotalItems { get; set; }
		public int ItemsPerPages { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPages);
	}
}
