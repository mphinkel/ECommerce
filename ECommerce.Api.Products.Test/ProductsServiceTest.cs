using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Test
{
	public class ProductsServiceTest
	{
		[Fact]
		public async Task GetProductsReturnsAllProducts()
		{
			var productProfile = new ProductProfile();
			var config = new MapperConfiguration(c => c.AddProfile(productProfile));
			var mapper = new Mapper(config);
			var options = new DbContextOptionsBuilder<ProductsDbContext>()
				.UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
			var dbContext = new ProductsDbContext(options);

			CreateProducts(dbContext);

			var productProvider = new ProductsProvider(dbContext, null, mapper);
			var result = await productProvider.GetProductsAsync();

			Assert.True(result.IsSuccess);
			Assert.True(result.Products.Any());
			Assert.Null(result.ErrorMessage);
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