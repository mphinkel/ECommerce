using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
	[Route("api/customers")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private readonly ICustomersProvider customersProvider;

		public CustomersController(ICustomersProvider customersProvider)
		{
			this.customersProvider = customersProvider;
		}

		[HttpGet]
		public async Task<IActionResult> GetCustomersAsync()
		{
			var result = await customersProvider.GetCustomersAsync();
			return result.IsSuccess
				? Ok(result.Customers)
				: NotFound();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomerAsync(int id)
		{
			var result = await customersProvider.GetCustomerAsync(id);
			return result.IsSuccess
				? Ok(result.Customer)
				: NotFound();
		}
	}
}