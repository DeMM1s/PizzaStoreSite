using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PizzaStore.Models
{
    public class Order
    {
	    [BindNever] 
	    public int OrderID { get; set; }
        [BindNever] 
        public ICollection<CartLine> Lines { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите город")]
        public string Sity { get; set; }
        [Required(ErrorMessage = "Введите адрес улицы")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Введите номер дома")]
        public string House { get; set; }
        [Required(ErrorMessage = "Введите номер квартиры")]
        public string Flat { get; set; }
        public bool Callback { get; set; } = false;

    }
}
