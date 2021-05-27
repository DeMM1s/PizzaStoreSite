﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PizzaStore.Infrastructure;

namespace PizzaStore.Models
{
    public class SessionCart : Cart
    {
		[JsonIgnore]
	    public ISession Session { get; set; }

	    public static Cart GetCart(IServiceProvider service)
	    {
		    ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
		    SessionCart cart = session.GetJson<SessionCart>("Cart") ?? new SessionCart();
		    cart.Session = session;
		    return cart;
	    }

	    public override void AddItems(Product product, int quantity)
	    {
		    base.AddItems(product, quantity);
			Session.SetJson("Cart", this);
	    }

	    public override void RemoveLine(Product product)
	    {
		    base.RemoveLine(product);
			Session.SetJson("Cart",this);
	    }

	    public override void Clear()
	    {
		    base.Clear();
			Session.Remove("Cart");
	    }
    }
}
