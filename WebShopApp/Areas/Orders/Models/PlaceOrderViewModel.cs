using WebShopApp.Models.Shop;

namespace WebShopApp.Areas.Orders.Models
{
    public class PlaceOrderViewModel
    {
        public string UserId { get; set; }
        public List<CartViewModel> OrderItems { get; set; }
        public ShippingAddressViewModel ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public double EstimatedTotal { get; set; }
    }

    public class ShippingAddressViewModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
