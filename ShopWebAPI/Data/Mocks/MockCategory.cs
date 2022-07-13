using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;
using System.Collections.Generic;

namespace ShopWebAPI.Data.Mocks
{
	public class MockCategory : ICategory
	{
		public IEnumerable<Category> AllCategories =>
			new List<Category>
			{
				new Category{ CategoryName = "Электромобили", Desc = "Современный вид транспорта"},
				new Category{ CategoryName = "Безиновые автомобили", Desc = "Машины с двигателем внутреннего сгорания"}
			};
	}
}
