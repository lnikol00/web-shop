using WebShopApp.DAL.Models;

namespace WebShopApp.Areas.Orders.Models
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public ApplicationUser User { get; set; }
    }
}
