using WebShopApp.DAL.Models;

namespace WebShopApp.Areas.Orders.Models
{
    public class PlaceOrderViewModel
    {
        public ApplicationUser User { get; set; }
        public ShippingAddressViewModel ShippingAddress { get; set; }
    }

    public class ShippingAddressViewModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
