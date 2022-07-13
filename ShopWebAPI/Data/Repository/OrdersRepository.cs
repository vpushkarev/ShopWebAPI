using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Interfaces.ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;

namespace ShopWebAPI.Data.Repository
{
	public class OrdersRepository : IOrders
	{
		private readonly AppDbContext _context;
		private readonly IShopCart _shopCart;

		public OrdersRepository(AppDbContext context, IShopCart shopCart)
		{
			_context = context;
			_shopCart = shopCart;
		}

		public void CreateOrder(Order order)
		{
			order.OrderTime = DateTime.Now;

			_context.Orders.Add(order);

			List<ShopCartItem> items = _shopCart.GetShopCartItems();

			foreach (ShopCartItem item in items)
			{
				OrderDetail orderDetail = new OrderDetail
				{
					CarId = item.Car.Id,
					OrderId = order.Id,
					Price = item.Car.Price
				};

				_context.OrderDetails.Add(orderDetail);
			}

			_context.SaveChanges();
		}
	}
}
