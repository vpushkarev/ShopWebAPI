using System;
using System.Collections.Generic;

namespace ShopWebAPI.Data.Models
{
	public class ShopCart
	{
		public string ShopCartId { get; set; }
		public List<ShopCartItem> ListShopItems { get; set; }

		//public static ShopCart GetCart(IServiceProvider services)
		//{
		//	//объект с помощью которого можно работать с сессией
		//	ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

		//	string guid = Guid.NewGuid().ToString();

		//	if (!string.IsNullOrEmpty(session.GetString("CartId"))) return new ShopCart {ShopCartId = guid};
		//	session.SetString("CartId", guid);

		//	return new ShopCart { ShopCartId = guid};
		//}
	}
}
