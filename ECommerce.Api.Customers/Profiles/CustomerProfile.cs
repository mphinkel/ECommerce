namespace ECommerce.Api.Customers.Profiles
{
	public class CustomerProfile : AutoMapper.Profile
	{
		public CustomerProfile()
		{
			CreateMap<Db.Customer, Models.Customer>();
			CreateMap<Models.Customer, Db.Customer>();
		}
	}
}