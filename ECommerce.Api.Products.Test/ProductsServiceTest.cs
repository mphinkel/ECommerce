using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Products.Test
{
	public class ProductsServiceTest
	{
		[Fact]
		public async Task GetProductsReturnsAllProducts()
		{
			var productProvider = CreateProductsProviderForTest(nameof(GetProductsReturnsAllProducts));
			var result = await productProvider.GetProductsAsync();

			Assert.True(result.IsSuccess);
			Assert.True(result.Products.Any());
			Assert.Null(result.ErrorMessage);
		}

		[Fact]
		public async Task GetProductsReturnsProductUsingValidId()
		{
			var productProvider = CreateProductsProviderForTest(nameof(GetProductsReturnsProductUsingValidId));
			var result = await productProvider.GetProductsAsync(1);

			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Product);
			Assert.Equal(1, result.Product.Id);
			Assert.Null(result.ErrorMessage);
		}

		[Fact]
		public async Task GetProductsReturnsProductUsingInvalidId()
		{
			var productProvider = CreateProductsProviderForTest(nameof(GetProductsReturnsProductUsingInvalidId));
			var result = await productProvider.GetProductsAsync(-1);

			Assert.False(result.IsSuccess);
			Assert.Null(result.Product);
			Assert.NotNull(result.ErrorMessage);
		}

		private static ProductsProvider CreateProductsProviderForTest(string dbName)
		{
			var productProfile = new ProductProfile();
			var config = new MapperConfiguration(c => c.AddProfile(productProfile));
			var mapper = new Mapper(config);
			var logger = new Logger<ProductsProvider>(new LoggerFactory());
			var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(dbName).Options;
			var dbContext = new ProductsDbContext(options);

			CreateProducts(dbContext);

			return new ProductsProvider(dbContext, logger, mapper);
		}

		private static void CreateProducts(ProductsDbContext dbContext)
		{
			for (var i = 1; i <= 10; i++)
			{
				dbContext.Products.Add(new Product
				{
					Id = i,
					Name = Guid.NewGuid().ToString(),
					Inventory = i + 20,
					Price = i * 4
				});
			}

			dbContext.SaveChanges();
		}
	}
}