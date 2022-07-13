using ShopWebAPI.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopWebAPI.Data.Interfaces
{
	public interface ICars
	{
		public IEnumerable<Car> GetAllCars();
		public IEnumerable<Car> FavCars { get; }
		public Task<Car> GetCarAsync(int carId);
		public Task<IEnumerable<Car>> GetCarsAsync(int[] ids);
		public Task<IEnumerable<Car>> GetCarsAsync(string category);
		public Task AddCarAsync(Car car);
		public Task<Car> UpdateCarAsync(Car car);
		public Task<Car> DeleteCar(int id);
	}
}
