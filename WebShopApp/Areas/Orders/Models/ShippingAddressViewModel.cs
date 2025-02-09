using WebShopApp.Models.Shop;

namespace WebShopApp.Areas.Orders.Models
{
    public class ShippingAddressViewModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
