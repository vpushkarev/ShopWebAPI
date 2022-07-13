using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;
using System.Collections.Generic;

namespace ShopWebAPI.Data.Repository
{
	public class CategoryRepository : ICategory
	{
		private readonly AppDbContext _context;

		public CategoryRepository(AppDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Category> AllCategories => _context.Categories;
	}
}
