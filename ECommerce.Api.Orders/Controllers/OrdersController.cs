using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
	[Route("api/orders")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrdersProvider ordersProvider;

		public OrdersController(IOrdersProvider ordersProvider)
		{
			this.ordersProvider = ordersProvider;
		}

		[HttpGet("{customerId}")]
		public async Task<IActionResult> GetOrdersAsync(int customerId)
		{
			var result = await ordersProvider.GetOrdersAsync(customerId);
			return result.IsSuccess
				? Ok(result.Orders)
				: NotFound();
		}
	}
}