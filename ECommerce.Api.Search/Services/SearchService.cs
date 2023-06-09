using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
	public class SearchService : ISearchService
	{
		private readonly IOrdersService ordersService;

		public SearchService(IOrdersService ordersService)
		{
			this.ordersService = ordersService;
		}

		public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
		{
			var ordersResult = await ordersService.GetOrdersAsync(customerId);
			if (ordersResult.IsSuccess)
			{
				return (true, ordersResult.Orders);
			}

			return (false, null);
		}
	}
}
