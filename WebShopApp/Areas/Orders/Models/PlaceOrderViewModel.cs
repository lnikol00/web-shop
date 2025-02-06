namespace WebShopApp.Areas.Orders.Models
{
    public class PlaceOrderViewModel
    {
       public string UserId { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public ShippingAddressViewModel ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public decimal EstimatedTotal { get; set; }
    }

    public class OrderItemViewModel
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string ProductId { get; set; }
    }

    public class ShippingAddressViewModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
