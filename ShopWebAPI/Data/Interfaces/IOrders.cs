using ShopWebAPI.Data.Models;

namespace ShopWebAPI.Data.Interfaces
{
	public interface IOrders
	{
		void CreateOrder(Order order);
	}
}
