using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
	[Route("api/products")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductsProvider productsProvider;

		public ProductsController(IProductsProvider productsProvider)
		{
			this.productsProvider = productsProvider;
		}

		[HttpGet]
		public async Task<IActionResult> GetProductsAsync()
		{
			var result = await productsProvider.GetProductsAsync();
			return result.IsSuccess
				? Ok(result.Products)
				: NotFound();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductsAsync(int id)
		{
			var result = await productsProvider.GetProductsAsync(id);
			return result.IsSuccess
				? Ok(result.Product)
				: NotFound();
		}
	}
}
