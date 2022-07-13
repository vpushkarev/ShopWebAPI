using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebAPI.Data;
using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebAPI.Controllers
{
	[Route("api/[controller]")]
	public class CarsController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly ICars _allCars;
		private readonly ICategory _allCarsCategory;

		public CarsController(AppDbContext context, ICars allCars, ICategory allCarsCategory)
		{
			_context = context;
			_allCars = allCars;
			_allCarsCategory = allCarsCategory;
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Car>> GetProductAsync(int id)
		{
			try
			{
				Car car = await _allCars.GetCarAsync(id);
				return car;
			}
			catch
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet("{category:alpha}")]
		public async Task<ActionResult<IEnumerable<Car>>> GetProductsAsync(string category)
		{
			try
			{
				IEnumerable<Car> result = await _allCars.GetCarsAsync(category);
				
				if(result == null)
					return new BadRequestResult();
		
				return result.ToList();
			}
			catch
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		public async Task<ActionResult<object>> AddProduct(Car car)
		{
			try
			{
				await _allCars.AddCarAsync(car);

				return CreatedAtAction(nameof(GetProductAsync), new {car.Id}, car);
				//return new CreatedResult(nameof(GetProductAsync), new {id});
			}
			catch
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPut]
		public async Task<ActionResult<Car>> UpdateProduct(Car car)
		{
			try
			{
				Car result = await _allCars.UpdateCarAsync(car);

				return result;
			}
			catch
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			try
			{
				Car car = await _allCars.DeleteCar(id);

				if (car == null)
					return NotFound($"Car with Id = {id} not found");

				return NoContent();
			}
			catch
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
