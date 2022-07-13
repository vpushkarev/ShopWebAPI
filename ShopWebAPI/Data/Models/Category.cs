using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopWebAPI.Data.Models
{
	public class Category
	{
		public int Id { set; get; }
		public string CategoryName { set; get; }
		public string Desc { set; get; }
		[JsonIgnore]
		public List<Car> Cars { set; get; }
	}
}
