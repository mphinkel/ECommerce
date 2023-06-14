using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
	public class CustomersProvider : ICustomersProvider
	{
		private readonly CustomersDbContext dbContext;
		private readonly ILogger<CustomersProvider> logger;
		private readonly IMapper mapper;

		public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.logger = logger;
			this.mapper = mapper;

			SeedData();
		}

		private void SeedData()
		{
			if (dbContext.Customers.Any()) return;

			dbContext.Customers.Add(new Customer { CustomerId = 1, Name = "Jessica Smith", Address = "20 Elm St." });
			dbContext.Customers.Add(new Customer { CustomerId = 2, Name = "John Smith", Address = "30 Main St." });
			dbContext.Customers.Add(new Customer { CustomerId = 3, Name = "William Johnson", Address = "100 10th St." });
			dbContext.SaveChanges();
		}

		public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
		{
			try
			{
				var customers = await dbContext.Customers.ToListAsync();
				if (!customers.Any()) return (false, null, "Not Found");

				var result = mapper.Map<IEnumerable<Customer>, IEnumerable<Models.Customer>>(customers);
				return (true, result, null);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.ToString());
				return (false, null, ex.Message);
			}
		}

		public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
		{
			try
			{
				var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
				if (customer is null) return (false, null, "Not Found");

				var result = mapper.Map<Customer, Models.Customer>(customer);
				return (true, result, null);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.ToString());
				return (false, null, ex.Message);
			}
		}

		public async Task<(bool IsSuccess, string ErrorMessage)> SaveCustomerAsync(Models.Customer customer)
		{
			try
			{
				var result = mapper.Map<Models.Customer, Customer>(customer);

				await dbContext.Customers.AddAsync(result);
				await dbContext.SaveChangesAsync();

				return (true, null);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.ToString());
				return (false, ex.Message);
			}
		}

		public async Task<(bool IsSuccess, string ErrorMessage)> UpdateCustomerAsync(Models.Customer customer)
		{
			try
			{
				var result = mapper.Map<Models.Customer, Customer>(customer);

				dbContext.Customers.Update(result);
				await dbContext.SaveChangesAsync();

				return (true, null);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.ToString());
				return (false, ex.Message);
			}
		}

		public async Task<(bool IsSuccess, string ErrorMessage)> DeleteCustomerAsync(int customerId)
		{
			try
			{
				var customer = await dbContext.Customers.FindAsync(customerId);
				if (customer == null) return (false, "Not Found");

				dbContext.Remove(customer);
				await dbContext.SaveChangesAsync();

				return (true, null);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.ToString());
				return (false, ex.Message);
			}
		}
	}
}