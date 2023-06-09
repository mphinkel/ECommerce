using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers
{
	[Route("api/search")]
	[ApiController]
	public class SearchController : ControllerBase
	{
		private readonly ISearchService searchService;

		public SearchController(ISearchService searchService)
		{
			this.searchService = searchService;
		}

		[HttpPost]
		public async Task<IActionResult> SearchAsync(SearchTerm term)
		{
			var result = await searchService.SearchAsync(term.CustomerId);
			return result.IsSuccess
				? Ok(result.SearchResults)
				: NotFound();
		}
	}
}
