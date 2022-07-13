using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Interfaces.ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebAPI.Data.Repository
{
	public class ShopCartRepository: IShopCart
	{
		private readonly AppDbContext _context;
		private readonly ICars _cars;
		private readonly IHttpContextAccessor _httpContextAccessor;
		//private readonly ShopCart _shopCart;

		public ShopCartRepository(IHttpContextAccessor httpContextAccessor, AppDbContext context, ICars cars/*, ShopCart shopCart*/)
		{
			_context = context;
			_cars = cars;
			_httpContextAccessor = httpContextAccessor;
			//_shopCart = shopCart;

			ISession session = httpContextAccessor.HttpContext.Session;
			//if (string.IsNullOrEmpty(session.GetString("CartId")))
			//	session.SetString("CartId", Guid.NewGuid().ToString());
		}

		public async Task<int> AddToCartAsync(int[] ids)
		{
			IEnumerable<Car> cars = (await _cars.GetCarsAsync(ids)).ToList();
			if (!cars.Any())
				return 0;

			string shopCartId = _httpContextAccessor.HttpContext.Session.GetString("CartId");

			List<ShopCartItem> newShopCartItems = new List<ShopCartItem>();
			List<ShopCartItem> shopCartItems = GetShopCartItems();
			foreach (Car car in cars)
			{
				if(shopCartItems.Exists(item => item.Car.Id == car.Id))
					continue;

				ShopCartItem newItem = new ShopCartItem { UserId = shopCartId, Car = car, Price = car.Price };
				newShopCartItems.Add(newItem);
			}

			await _context.ShopCartItems.AddRangeAsync(newShopCartItems);
			await _context.SaveChangesAsync();

			return newShopCartItems.Count;
		}

		public async Task<int> RemoveFromCartAsync(int[] ids)
		{
			List<ShopCartItem> shopCartItems = GetShopCartItems().Where(item => ids.Contains(item.Car.Id)).ToList();
			if (!shopCartItems.Any())
				return 0;

			_context.ShopCartItems.RemoveRange(shopCartItems);
			await _context.SaveChangesAsync();

			return shopCartItems.Count; 
		}

		public List<ShopCartItem> GetShopCartItems()
		{
			string shopCartId = _httpContextAccessor.HttpContext.Session.GetString("CartId");
			List<ShopCartItem> items = _context.ShopCartItems.Where(item => item.ShopCartId == shopCartId).Include(item => item.Car).ToList();
			return items;
		}
	}
}