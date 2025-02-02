using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using WebShopApp.Controllers.Base;
using WebShopApp.DAL.Enums;
using WebShopApp.DAL.Models;
using WebShopApp.Exceptions;
using WebShopApp.Models;
using WebShopApp.Models.Shop;

namespace WebShopApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IOptions<ConnectionStrings> _connectionApi;

        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IOptions<ConnectionStrings> connectionApi, HttpClient httpClient)
        {
            _logger = logger;
            _connectionApi = connectionApi;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(Category category, int pageNumber = 1, int pageSize = 30)
        {
            var categories = Enum.GetValues(typeof(Category))
             .Cast<Category>()
             .Select(e => new CategoryViewModel
             {
                 Value = e.ToString(),
                 Name = GetEnumDisplayName(e),
                 Description = GetEnumDescription(e)
             })
             .ToList();

            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = category.ToString();

            string categoryString = category.ToString().Replace('_', '-');

            string url = _connectionApi.Value.External;

            if (!string.IsNullOrEmpty(url))
            {
                if (categoryString == "none")
                {
                    url += "/products?limit=194";
                }
                else
                {
                    url += $"/products/category/{categoryString}";
                }
            }

            List<ProductViewModel> request = new List<ProductViewModel>();
            var results = await GetApiResponse<Products>(url);

            if (results != null && results.products.Count > 0)
            {
                var products = results.products.Select(x => new ProductViewModel
                {
                    Id = x.id,
                    Image = x.images[0],
                    Title = x.title,
                    Price = x.price
                }).ToList();

                int totalProducts = products.Count;
                int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

                var productsPerPage = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                request.AddRange(productsPerPage);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = totalPages;
            }
            else
            {
                ViewBag.ErrorMessage = "No products available!";
            }

            return View(request);
        }

        public async Task<IActionResult> Details(int id)
        {
            string url = _connectionApi.Value.External;

            if (!string.IsNullOrEmpty(url))
            {
                url += $"/products/{id}";
            }

            ProductViewModel request = null;
            var product = await GetApiResponse<Product>(url);

            if (product != null)
            {
                request = new ProductViewModel
                {
                    Id = product.id,
                    Image = product.images[0],
                    Title = product.title,
                    Description = product.description,
                    Price = product.price,
                    Stock = product.stock,
                    Availability = product.availabilityStatus,

                    Reviews = product.reviews.Select(r => new ReviewViewModel
                    {
                        Rating = r.rating,
                        Comment = r.comment,
                        Date = r.date,
                        ReviewerName = r.reviewerName
                    }).ToList()
                };
            }
            else
            {
                ViewBag.ErrorMessage = "Product with specified ID doesn't exist!";
            }

            return View(request);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region PRIVATE 
        private async Task<T> GetApiResponse<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        return JsonSerializer.Deserialize<T>(responseContent);
                    }
                    else
                    {
                        throw new ErrorMessage("Invalid response context!");
                    }
                }
                else
                {
                    throw new ErrorMessage("No response!");
                }
            }
            catch (Exception ex)
            {
                throw new ErrorMessage($"An error occurred: {ex.Message}");
            }
        }

        private string GetEnumDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
            return attribute?.Name ?? value.ToString();
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
            return attribute?.Description ?? string.Empty;
        }

        #endregion
    }
}
