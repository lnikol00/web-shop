using Microsoft.AspNetCore.Mvc;
using WebShopApp.Areas.Orders.Models;
using WebShopApp.Controllers.Base;
using WebShopApp.DAL.Models;
using WebShopApp.Exceptions;
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

        public async Task<IActionResult> OrderDetails(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Order order = await repository.GetByIdAsync<Order>(id);

                return View(order);
            }
            else
            {
                throw new ErrorMessage("No order with this id!");
            }

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

            HttpContext.Session.Remove("CartItems");

            TempData["success"] = "Order completed!";

            //return RedirectToAction("OrderDetails", new { id = order.Id });
            return Json(new { redirectToUrl = Url.Action("OrderDetails", new { id = order.Id }) });
        }
    }
}
