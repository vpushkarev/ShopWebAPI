using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace ShopWebAPI.Data.Models
{
	public class Order
	{
		[BindNever]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Adress { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }

		[BindNever]
		public DateTime OrderTime { get; set; }

		public List<OrderDetail> OrderDetails { get; set; }
		
	}
}
