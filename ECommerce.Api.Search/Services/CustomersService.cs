using System.Text.Json;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services
{
	public class CustomersService : ICustomersService
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly ILogger<CustomersService> logger;

		public CustomersService(IHttpClientFactory httpClientFactory, ILogger<CustomersService> logger)
		{
			this.httpClientFactory = httpClientFactory;
			this.logger = logger;
		}

		public async Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int customerId)
		{
			try
			{
				var client = httpClientFactory.CreateClient("CustomersService");

				var response = await client.GetAsync($"api/customers/{customerId}");
				if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

				var content = await response.Content.ReadAsByteArrayAsync();
				var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
				var result = JsonSerializer.Deserialize<dynamic>(content, options);

				return (true, result, null);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.ToString());
				return (false, null, ex.Message); 
			}
		}
	}
}
