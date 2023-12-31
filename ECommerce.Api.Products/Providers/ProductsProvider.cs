﻿using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
	public class ProductsProvider : IProductsProvider
	{
		private readonly ProductsDbContext dbContext;
		private readonly ILogger<ProductsProvider> logger;
		private readonly IMapper mapper;

		public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.logger = logger;
			this.mapper = mapper;

			SeedData();
		}

		private void SeedData()
		{
			if (dbContext.Products.Any()) return;

			dbContext.Products.Add(new Product { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
			dbContext.Products.Add(new Product { Id = 2, Name = "Mouse", Price = 5, Inventory = 200 });
			dbContext.Products.Add(new Product { Id = 3, Name = "Monitor", Price = 150, Inventory = 150 });
			dbContext.Products.Add(new Product { Id = 4, Name = "CPU", Price = 200, Inventory = 2000 });
			dbContext.SaveChanges();
		}

		public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
		{
			try
			{
				var products = await dbContext.Products.ToListAsync();
				if (!products.Any()) return (false, null, "Not Found");

				var result = mapper.Map<IEnumerable<Product>, IEnumerable<Models.Product>>(products);
				return (true, result, null);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.ToString());
				return (false, null, ex.Message);
			}
		}

		public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductsAsync(int id)
		{
			try
			{
				var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
				if (product is null) return (false, null, "Not Found");

				var result = mapper.Map<Product, Models.Product>(product);
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
