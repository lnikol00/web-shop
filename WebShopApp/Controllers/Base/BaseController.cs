using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShopApp.DAL.Models;

namespace WebShopApp.Controllers.Base
{
    [Authorize]
    public class BaseController : Controller
    {
        #region Korisnik

        /// <summary>
        /// Logirani korisnik 
        /// </summary>
        public CustomClaimsPrincipal Korisnik
        {
            get { return new CustomClaimsPrincipal(this.User as ClaimsPrincipal); }
        }

        #endregion
    }
}
