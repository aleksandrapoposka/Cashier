namespace Cashier.Models.Orders
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
    }
}
