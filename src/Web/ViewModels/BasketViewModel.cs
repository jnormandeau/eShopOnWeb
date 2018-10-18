using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.Web.ViewModels
{
	public class BasketViewModel
	{
		public int Id { get; set; }
		public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
		public string BuyerId { get; set; }

		public decimal SubTotal()
		{
			return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
		}

		public decimal TaxAmount()
		{
			var tax = 0.15m;
			return Math.Round(SubTotal() * tax, 2);
		}

		public decimal Total()
		{
			return Math.Round(SubTotal() + TaxAmount(), 2);
		}
	}
}