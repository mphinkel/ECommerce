﻿using ECommerce.Api.Products.Interfaces;
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
			if (result.IsSuccess)
			{
				return Ok(result.Products);
			}

			return NotFound();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductsAsync(int id)
		{
			var result = await productsProvider.GetProductsAsync(id);
			if (result.IsSuccess)
			{
				return Ok(result.Product);
			}

			return NotFound();
		}
	}
}
