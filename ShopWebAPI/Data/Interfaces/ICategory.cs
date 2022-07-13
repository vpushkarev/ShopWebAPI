using ShopWebAPI.Data.Models;
using System.Collections.Generic;

namespace ShopWebAPI.Data.Interfaces
{
	public interface ICategory
	{
		public IEnumerable<Category> AllCategories { get; }
	}
}
