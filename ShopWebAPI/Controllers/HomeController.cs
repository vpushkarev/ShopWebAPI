using Microsoft.AspNetCore.Mvc;
using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopWebAPI.Controllers
{
	[Route("api")]
	public class HomeController : ControllerBase
	{
		private readonly ICars _carRep;
		
		public HomeController(ICars carRepository)
		{
			_carRep = carRepository;
		}

		public ActionResult<List<Car>> Index()
		{
			return _carRep.FavCars.ToList();
		}
	}
}
