using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
	public class SearchService : ISearchService
	{
		private readonly IOrdersService ordersService;
		private readonly IProductsService productsService;

		public SearchService(IOrdersService ordersService, IProductsService productsService)
		{
			this.ordersService = ordersService;
			this.productsService = productsService;
		}

		public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
		{
			var ordersResult = await ordersService.GetOrdersAsync(customerId);
			if (!ordersResult.IsSuccess) return (false, null);

			var productsResult = await productsService.GetProductsAsync();
			foreach (var orders in ordersResult.Orders)
			{
				foreach (var item in orders.Items)
				{
					item.ProductName = productsResult.IsSuccess
						? productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name ?? string.Empty
						: "Product information is not available";
				}
			}

			return (true, ordersResult.Orders);
		}
	}
}
