using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Mocks;
using ShopWebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopWebAPI.Data.Mocks
{
	public class MockCars //: IAllCars
	{
		private readonly ICategory _categoryCars = new MockCategory();

		public IEnumerable<Car> Cars =>
			new List<Car>
			{
				new Car 
				{
					Name = "Tesla", 
					Price = 45000,
					ShortDesc = "Илон Маск мой герой",
					IsFavourite = true, 
					Available = true, 
					Img = "/img/Tesla.jpg", 
					Category = _categoryCars.AllCategories.First()

				},
				new Car
				{
					Name = "Volkswagen", 
					Price = 15000,
					ShortDesc = "Хэнд э хох",
					IsFavourite = true, 
					Available = false, 
					Img = "/img/Volkswagen.jpg", 
					Category = _categoryCars.AllCategories.Last()
				},
				new Car
				{
					Name = "BMW", 
					Price = 30000,
					ShortDesc = "Их бин больной",
					IsFavourite = false, 
					Available = true, 
					Img = "/img/BMW.jpg", 
					Category = _categoryCars.AllCategories.Last()
				}
			};

		public IEnumerable<Car> FavCars => throw new NotImplementedException();

		public Car GetObjectCar(int carId)
		{
			throw new NotImplementedException();
		}
	}
}
