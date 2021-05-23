﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PizzaStore.Infrastructure
{
    public static class SessionExtensions
    {
	    public static void SetJson(this ISession session, string key, object value)
	    {
			session.SetString(key, JsonConvert.SerializeObject(value));
	    }

	    public static T GetJson<T>(this ISession session, string key)
	    {
		    var sessionData = session.GetString(key);
		    return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
	    }
    }
}
