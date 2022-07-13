using Microsoft.AspNetCore.Mvc;
using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Interfaces.ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;

namespace ShopWebAPI.Controllers
{
	[Route("api/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IOrders _allOrders;
		private readonly IShopCart _shopCart;

		public OrderController(IOrders allOrders, IShopCart shopCart)
		{
			_allOrders = allOrders;
			_shopCart = shopCart;
		}

		//	public IActionResult Checkout()
		//	{
		//		return View();
		//	}

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			_shopCart.ListShopItems = _shopCart.GetShopCartItems();

			if (_shopCart.ListShopItems.Count == 0)
				ModelState.AddModelError("", "У вас должны быть товары!");

			if (ModelState.IsValid)
			{
				_allOrders.CreateOrder(order);
				return RedirectToAction("Complete");
			}

				return View(order);
		}

			//	public IActionResult Complete()
			//	{
			//		ViewBag.Message = "Заказ обработан";
			//		return View();
			//	}
		}
}
