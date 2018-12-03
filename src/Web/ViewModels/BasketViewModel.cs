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

		public decimal Subtotal()
		{
			return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
		}

		public decimal TaxAmount()
		{
			return Math.Round(Subtotal() * 0.15m, 2);
		}

		public decimal Total()
		{
			return Subtotal() + TaxAmount();
		}
	}
}