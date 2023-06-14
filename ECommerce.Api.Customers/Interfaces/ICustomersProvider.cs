using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces
{
	public interface ICustomersProvider
	{
		Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
		Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
		Task<(bool IsSuccess, string ErrorMessage)> SaveCustomerAsync(Customer customer);
		Task<(bool IsSuccess, string ErrorMessage)> UpdateCustomerAsync(Customer customer);
		Task<(bool IsSuccess, string ErrorMessage)> DeleteCustomerAsync(int customerId);
	}
}
