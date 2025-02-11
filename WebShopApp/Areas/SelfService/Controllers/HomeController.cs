using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using WebShopApp.Controllers.Base;
using WebShopApp.DAL.Models;
using WebShopApp.Exceptions;
using WebShopApp.Infrastructure.Interface;

namespace WebShopApp.Areas.SelfService.Controllers
{
    [Area("SelfService")]
    public class HomeController : BaseController
    {
        private IRepository repository;
        private IWebHostEnvironment env;

        public HomeController(
             IRepository repository,
             IWebHostEnvironment env) : base()
        {
            this.repository = repository;
            this.env = env;
        }
        public async Task<IActionResult> Profile()
        {
            var userInfo = await repository.GetByIdAsync<ApplicationUser>(Korisnik.Id);

            return View(userInfo);
        }

        public async Task<IActionResult> OrderSummary()
        {
            var orders = await repository.GetAsync<Order>(o => o.UserId == Korisnik.Id);

            return View(orders);
        }

        public async Task<IActionResult> ViewOrder(string id)
        {
            Order order = await repository.GetByIdAsync<Order>(id);

            if (order == null)
                throw new ErrorMessage("No order with this id!");
            else
                return PartialView("_ViewOrder", order);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [AllowAnonymous]
        public async Task<IActionResult> GetProfilePicture(string korisnikId = null)
        {
            if (string.IsNullOrEmpty(korisnikId) == true)
                korisnikId = Korisnik.Id;

            var korisnik = await repository.GetByIdAsync<ApplicationUser>(korisnikId);

            if (korisnik == null || korisnik.AvatarImage == null)
            {
                byte[] profile = System.IO.File.ReadAllBytes(Path.Combine(env.WebRootPath, "img\\profile-pics\\profile.png"));
                Stream stream = new MemoryStream(profile);
                return File(stream, "image/jpeg");
            }
            else
            {
                Stream stream = new MemoryStream(korisnik.AvatarImage);
                return File(stream, korisnik.ContentType);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfileImage(IFormFile file)
        {
            var mimeType = MimeKit.MimeTypes.GetMimeType(file.FileName);

            if (mimeType.StartsWith("image") == false)
                throw new ErrorMessage("Format slike nije podržan.");

            var korisnik = await repository.GetByIdAsync<ApplicationUser>(Korisnik.Id);
            using (MemoryStream stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using var myImage = await Image.LoadAsync(stream);
                using var outStream = new MemoryStream();
                await myImage.SaveAsync(outStream, new WebpEncoder());

                korisnik.AvatarImage = outStream.ToArray();
                korisnik.FileName = file.FileName;
                korisnik.ContentType = "image/webp";

                korisnik.AvatarImage = stream.ToArray();
                korisnik.FileName = file.FileName;
                korisnik.ContentType = mimeType;
                repository.Update(korisnik, Korisnik.Name);
                await repository.SaveAsync();
            }

            TempData["success"] = "Image successfully added.";

            return base.Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProfleImage()
        {
            var korisnik = await repository.GetByIdAsync<ApplicationUser>(Korisnik.Id);

            korisnik.AvatarImage = null;
            korisnik.FileName = null;
            korisnik.ContentType = null;
            repository.Update(korisnik, Korisnik.Name);
            await repository.SaveAsync();

            TempData["success"] = "Successfully deleted!";

            return base.Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUser model)
        {
            var korisnik = await repository.GetByIdAsync<ApplicationUser>(Korisnik.Id);

            if (korisnik != null)
            {
                if (korisnik.Phone != model.Phone)
                {
                    korisnik.Phone = model.Phone;
                    repository.Update(korisnik, Korisnik.Name);
                    await repository.SaveAsync();
                }


                if (korisnik.Address != model.Address)
                {
                    korisnik.Address = model.Address;
                    repository.Update(korisnik, Korisnik.Name);
                    await repository.SaveAsync();
                }

                if (korisnik.City != model.City)
                {
                    korisnik.City = model.City;
                    repository.Update(korisnik, Korisnik.Name);
                    await repository.SaveAsync();
                }

                if (korisnik.PostalCode != model.PostalCode)
                {
                    korisnik.PostalCode = model.PostalCode;
                    repository.Update(korisnik, Korisnik.Name);
                    await repository.SaveAsync();
                }

                if (korisnik.Country != model.Country)
                {
                    korisnik.Country = model.Country;
                    repository.Update(korisnik, Korisnik.Name);
                    await repository.SaveAsync();
                }
            }

            TempData["success"] = "Profil updated successfully!";

            return base.Accepted();
        }
    }
}
