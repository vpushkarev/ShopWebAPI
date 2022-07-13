using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShopWebAPI.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopWebAPI.Data
{
	public class DbObjects
	{
		public static void Initial(AppDbContext context)
		{
			if (!context.Categories.Any())
				context.Categories.AddRange(Categories.Select(c => c.Value));

			if (!context.Cars.Any())
				context.Cars.AddRange(
					new Car
					{
						Name = "Tesla",
						Price = 45000,
						ShortDesc = "Илон Маск мой герой",
						IsFavourite = true,
						Available = true,
						Img = "/img/Tesla.jpg",
						Category = Categories["Электромобили"]

					},
					new Car
					{
						Name = "Volkswagen",
						Price = 15000,
						ShortDesc = "Хэнд э хох",
						IsFavourite = true,
						Available = false,
						Img = "/img/Volkswagen.jpg",
						Category = Categories["Безиновые автомобили"]
					},
					new Car
					{
						Name = "BMW",
						Price = 30000,
						ShortDesc = "Их бин больной",
						IsFavourite = false,
						Available = true,
						Img = "/img/BMW.jpg",
						Category = Categories["Безиновые автомобили"]
					});

			context.SaveChanges();
		}

		private static Dictionary<string, Category> _categories;
		public static Dictionary<string, Category> Categories
		{
			get
			{
				if (_categories == null)
				{
					Category[] list = {
						new Category{ CategoryName = "Электромобили", Desc = "Современный вид транспорта"},
						new Category{ CategoryName = "Безиновые автомобили", Desc = "Машины с двигателем внутреннего сгорания"}
					};

					_categories = new Dictionary<string, Category>();
					foreach (Category el in list)
					{
						_categories.Add(el.CategoryName, el);
					}
				}

				return _categories;
			}
		}
	}
}
