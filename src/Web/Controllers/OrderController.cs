﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class OrderController : Controller
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IUriComposer _uriComposer;

		public OrderController(IOrderRepository orderRepository, IUriComposer uriComposer)
		{
			_orderRepository = orderRepository;
			_uriComposer = uriComposer;
		}

		public async Task<IActionResult> Index()
		{
			var orders = await _orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification(User.Identity.Name));

			var viewModel = orders
					.Select(o => new OrderViewModel()
					{
						OrderDate = o.OrderDate,
						OrderItems = o.OrderItems?.Select(oi => new OrderItemViewModel()
						{
							Discount = 0,
							PictureUrl = _uriComposer.ComposePicUri(oi.ItemOrdered.PictureUri),
							ProductId = oi.ItemOrdered.CatalogItemId,
							ProductName = oi.ItemOrdered.ProductName,
							UnitPrice = oi.UnitPrice,
							Units = oi.Units
						}).ToList(),
						OrderNumber = o.Id,
						ShippingAddress = o.ShipToAddress,
						Status = "Pending",
						Total = o.Total(),
						SubTotal = o.SubTotal(),
						TaxAmount = o.TaxAmount()
					});
			return View(viewModel);
		}

		[HttpGet("{orderId}")]
		public async Task<IActionResult> Detail(int orderId)
		{
			var customerOrders = await _orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification(User.Identity.Name));
			var order = customerOrders.FirstOrDefault(o => o.Id == orderId);
			if (order == null)
			{
				return BadRequest("No such order found for this user.");
			}
			var viewModel = new OrderViewModel()
			{
				OrderDate = order.OrderDate,
				OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel()
				{
					Discount = 0,
					PictureUrl = _uriComposer.ComposePicUri(oi.ItemOrdered.PictureUri),
					ProductId = oi.ItemOrdered.CatalogItemId,
					ProductName = oi.ItemOrdered.ProductName,
					UnitPrice = oi.UnitPrice,
					Units = oi.Units
				}).ToList(),
				OrderNumber = order.Id,
				ShippingAddress = order.ShipToAddress,
				Status = "Pending",
				Total = order.Total(),
				SubTotal = order.SubTotal(),
				TaxAmount = order.TaxAmount()
			};
			return View(viewModel);
		}
	}
}