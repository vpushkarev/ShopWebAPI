using Microsoft.EntityFrameworkCore;
using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebAPI.Data.Repository
{
	public class CarRepository : ICars
	{
		private readonly AppDbContext _context;

		public CarRepository(AppDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Car> GetAllCars()
		{
			List<Car> result = _context.Cars.Include(car => car.Category).ToList();
			return result;
		}

		public IEnumerable<Car> FavCars => _context.Cars.Where(car => car.IsFavourite).Include(car => car.Category);
		public async Task<Car> GetCarAsync(int carId) => await _context.Cars.FindAsync(carId);

		public async Task<IEnumerable<Car>> GetCarsAsync(int[] ids)
		{
			List<Car> result = new List<Car>();

			foreach (int id in ids)
			{
				Car car = await _context.Cars.FindAsync(id);
				
				if(car == null)
					continue;

				result.Add(car);
			}

			return result;
		}

		public async Task<IEnumerable<Car>> GetCarsAsync(string category)
		{
			string currCategory;
			if (string.IsNullOrEmpty(category))
			{
				currCategory = "Все автомобили";
			}
			else
			{
				if (string.Equals("electro", category, StringComparison.OrdinalIgnoreCase))
				{
					currCategory = "Электромобили";
				}
				else if (string.Equals("fuel", category, StringComparison.OrdinalIgnoreCase))
				{
					currCategory = "Безиновые автомобили";
				}
				else
				{
					return null;
				}
			}

			IEnumerable<Car> cars = GetAllCars();

			IEnumerable<Car> enumerable = cars.ToList();
			if (!enumerable.Any())
				return enumerable.ToList();

			IEnumerable<Car> result = enumerable.Where(i => i.Category.CategoryName.Equals(currCategory)).OrderBy(i => i.Id);
			
			return result;
		}

		public async Task AddCarAsync(Car car)
		{
			_context.Cars.Add(car);
			await _context.SaveChangesAsync();
		}

		public async Task<Car> UpdateCarAsync(Car car)
		{
			Car result = await GetCarAsync(car.Id);

			if (result == null)
				return null;

			if (result.CategoryId != car.CategoryId)
				result.Category = await _context.Categories.FindAsync(car.CategoryId);

			result.CategoryId = car.CategoryId;
			result.Available = car.Available;
			result.Img = car.Img;
			result.IsFavourite = car.IsFavourite;
			result.LongDesc = car.LongDesc;
			result.ShortDesc = car.ShortDesc;
			result.Name = car.Name;
			result.Price = car.Price;
			
			await _context.SaveChangesAsync();

			return result;
		}

		public async Task<Car> DeleteCar(int id)
		{
			Car car = await _context.Cars.FindAsync(id);

			if (car != null)
			{
				_context.Cars.Remove(car);
				await _context.SaveChangesAsync();

				return car;
			}

			return null;
		}
	}
}