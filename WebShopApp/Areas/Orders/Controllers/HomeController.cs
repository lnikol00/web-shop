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

        public async Task<IActionResult> Index()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartViewModel>>("CartItems") ?? new List<CartViewModel>();
            double totalPrice = cartItems.Sum(item => item.Price * item.Quantity);

            ViewBag.CartItems = cartItems;

            var userInfo = await repository.GetByIdAsync<ApplicationUser>(Korisnik.Id);

            var viewModel = new PlaceOrderViewModel()
            {
                User = userInfo,
                EstimatedTotal = totalPrice,
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<ActionResult> PlaceOrder(ShippingAddressViewModel placeOrder)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartViewModel>>("CartItems") ?? new List<CartViewModel>();
            double totalPrice = cartItems.Sum(item => item.Price * item.Quantity);

            var order = new Order
            {
                UserId = Korisnik.Id,
                EstimatedTotal = totalPrice,
            };

            repository.Create<Order>(order);
            await repository.SaveAsync();

            var shippingAddress = new OrderShippingAddress
            {
                OrderId = order.Id,
                Address = placeOrder.Address,
                City = placeOrder.City,
                PostalCode = placeOrder.PostalCode,
                Country = placeOrder.Country
            };

            repository.Create<OrderShippingAddress>(shippingAddress);

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItems
                {
                    OrderId = order.Id,
                    Title = item.Title,
                    Image = item.Image,
                    Price = item.Price,
                    Qty = item.Quantity,
                };

                repository.Create<OrderItems>(orderItem);
            }

            await repository.SaveAsync();

            TempData["success"] = "Order completed!";

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
