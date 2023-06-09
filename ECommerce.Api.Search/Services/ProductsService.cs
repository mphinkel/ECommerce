using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
	public class ProductsService : IProductsService
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly ILogger logger;

		public ProductsService(IHttpClientFactory httpClientFactory, ILogger<IProductsService> logger)
		{
			this.httpClientFactory = httpClientFactory;
			this.logger = logger;
		}

		public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
		{
			try
			{
				var client = httpClientFactory.CreateClient("ProductsService");

				var response = await client.GetAsync("api/products");
				if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

				var content = await response.Content.ReadAsByteArrayAsync();
				var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
				var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

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
