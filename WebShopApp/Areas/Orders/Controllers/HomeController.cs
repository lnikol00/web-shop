using Microsoft.AspNetCore.Mvc;
using WebShopApp.Controllers.Base;

namespace WebShopApp.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
