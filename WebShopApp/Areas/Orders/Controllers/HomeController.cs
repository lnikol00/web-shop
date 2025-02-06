using Microsoft.AspNetCore.Mvc;
using WebShopApp.Areas.Orders.Models;
using WebShopApp.Controllers.Base;
using WebShopApp.DAL.Models;

namespace WebShopApp.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> PlaceOrder(PlaceOrderViewModel placeOrderViewModel)
        //{
        //    if (placeOrderViewModel.OrderItems == null || !placeOrderViewModel.OrderItems.Any())
        //    {
        //        ModelState.AddModelError("", "No order items");
        //        return View(placeOrderViewModel);  
        //    }

        //    // Stvaranje nove narudžbe
        //    var order = new Order
        //    {
        //        UserId = User.Identity.(), 
        //        ShippingAddress = new ShippingAddress
        //        {
        //            Address = placeOrderViewModel.ShippingAddress.Address,
        //            City = placeOrderViewModel.ShippingAddress.City,
        //            PostalCode = placeOrderViewModel.ShippingAddress.PostalCode
        //        },
        //        PaymentMethod = placeOrderViewModel.PaymentMethod,
        //        EstimatedTotal = placeOrderViewModel.EstimatedTotal,



        //        OrderItems = placeOrderViewModel.OrderItems.Select(oi => new OrderItem
        //        {
        //            Title = oi.Title,
        //            Image = oi.Image,
        //            Price = oi.Price,
        //            Qty = oi.Qty,
        //            ProductId = oi.ProductId
        //        }).ToList()
        //    };

        //    // Dodavanje narudžbe u bazu
        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();

        //    // Nakon što je narudžba stvorena, možeš preusmjeriti korisnika na detalje narudžbe ili na početnu stranicu
        //    return RedirectToAction("OrderDetails", new { id = order.Id });
        //}
    }
}
