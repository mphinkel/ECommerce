using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
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

		[HttpPost]
		public async Task<ActionResult<Customer>> SaveCustomerAsync(Customer customer) {
			var result = await customersProvider.SaveCustomerAsync(customer);
			return result.IsSuccess
				? Ok()
				: NotFound();
		}

		[HttpPut]
		public async Task<ActionResult> UpdateCustomerAsync(Customer customer) {
			var result = await customersProvider.UpdateCustomerAsync(customer);
			return result.IsSuccess
				? Ok()
				: NotFound();
		}

		[HttpDelete ("{customerId}")]
		public async Task<ActionResult> DeleteCustomerAsync(int customerId) {
			var result = await customersProvider.DeleteCustomerAsync(customerId);
			return result.IsSuccess
				? Ok()
				: NotFound();
		}
	}
}