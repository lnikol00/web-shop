using Microsoft.AspNetCore.Mvc;
using WebShopApp.Areas.Orders.Models;
using WebShopApp.Controllers.Base;
using WebShopApp.DAL.Models;
using WebShopApp.Infrastructure.Interface;
using WebShopApp.Infrastructure.Services;
using WebShopApp.Models.Shop;

namespace WebShopApp.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class HomeController : BaseController
    {
        private IRepository repository;

        public HomeController(
             IRepository repository,
             IWebHostEnvironment env) : base()
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder(PlaceOrderViewModel placeOrderViewModel)
        {

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartViewModel>>("CartItems") ?? new List<CartViewModel>();
            double totalPrice = cartItems.Sum(item => item.Price * item.Quantity);

            var order = new Order
            {
                UserId = Korisnik.Id,
                EstimatedTotal = totalPrice,
            };

            var shippingAddress = new ShippingAddress
            {
                Address = placeOrderViewModel.ShippingAddress.Address,
                City = placeOrderViewModel.ShippingAddress.City,
                PostalCode = placeOrderViewModel.ShippingAddress.PostalCode
            };

            List<OrderItems> orderItems = new List<OrderItems>();
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItems
                {
                    Title = item.Title,
                    Image = item.Image,
                    Price = item.Price,
                    Qty = item.Quantity,
                };

                orderItems.Add(orderItem);
            }

            return RedirectToAction("OrderDetails", new { id = order.Id });
        }
    }
}
