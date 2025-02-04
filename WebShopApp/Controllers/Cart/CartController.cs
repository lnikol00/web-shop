using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using WebShopApp.Controllers.Base;
using WebShopApp.DAL.Models;
using WebShopApp.Infrastructure.Services;
using WebShopApp.Models.Shop;

namespace WebShopApp.Controllers.Cart
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartViewModel>>("CartItems") ?? new List<CartViewModel>();

            decimal totalPrice = cartItems.Sum(item => item.Price * item.Quantity);

            ViewBag.TotalPrice = totalPrice;
            ViewBag.CartItems = cartItems;

            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int id, string title, decimal price, string image, int quantity)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartViewModel>>("CartItems") ?? new List<CartViewModel>();

            var existingItem = cartItems.FirstOrDefault(item => item.ProductId == id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new CartViewModel
                {
                    ProductId = id,
                    Title = title,
                    Price = price,
                    Image = image,
                    Quantity = quantity
                };
                cartItems.Add(newItem);
            }

            HttpContext.Session.SetObjectAsJson("CartItems", cartItems);

            TempData["info"] = "Item added to the cart.";

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartViewModel>>("CartItems") ?? new List<CartViewModel>();

            var itemToRemove = cartItems.FirstOrDefault(item => item.ProductId == id);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
            }

            HttpContext.Session.SetObjectAsJson("CartItems", cartItems);


            if (cartItems.Count > 0)
            {
                TempData["info"] = "Item removed from the cart.";

                return RedirectToAction("Index");
            }
            else
            {

                TempData["info"] = "Your cart has been cleared.";
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("CartItems");

            TempData["info"] = "Your cart has been cleared.";

            return RedirectToAction("Index", "Home");
        }
    }
}
