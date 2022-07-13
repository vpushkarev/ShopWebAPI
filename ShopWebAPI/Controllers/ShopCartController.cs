using Microsoft.AspNetCore.Mvc;
using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Interfaces.ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebAPI.Controllers
{
	[Route("api/[controller]")]
	public class ShopCartController : ControllerBase
	{
		private readonly IShopCart _shopCart;

		public ShopCartController(IShopCart shopCart)
		{
			_shopCart = shopCart;
		}

		public List<ShopCartItem> Index()
		{
			return _shopCart.GetShopCartItems();
		}

		[HttpPost]
		public async Task<IActionResult> AddToCartAsync([FromBody] int[] ids)
		{
			int addedCount = await _shopCart.AddToCartAsync(ids);

			return Ok(addedCount);
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveFromCartAsync([FromBody] int[] ids)
		{
			int removeCount = await _shopCart.RemoveFromCartAsync(ids);

			return Ok(removeCount);
		}
	}
}
