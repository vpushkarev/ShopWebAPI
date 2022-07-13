using System.Collections.Generic;
using System.Threading.Tasks;
using ShopWebAPI.Data.Models;

namespace ShopWebAPI.Data.Interfaces
{
	namespace ShopWebAPI.Data.Interfaces
	{
		public interface IShopCart
		{
			public Task<int> AddToCartAsync(int[] ids);
			public Task<int> RemoveFromCartAsync(int[] ids);
			public List<ShopCartItem> GetShopCartItems();
		}
	}
}