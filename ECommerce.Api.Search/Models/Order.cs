namespace ECommerce.Api.Search.Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal Total { get; set; }
		public List<OrderItem> Items { get; set; }
	}
}
