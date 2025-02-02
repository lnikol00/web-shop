using System.Security.Claims;

namespace WebShopApp.DAL.Models
{
    /// <summary>
    /// Klasa predstavlja Principal ApplicationUser klase
    /// Dodavanjem property-a se omogucava lagan dohvat podataka o korisniku koji je logiran
    /// Klasa se instancija u BaseController-u kroz property Korisnik
    /// </summary>
    public class CustomClaimsPrincipal : ClaimsPrincipal
    {
        #region Konstruktor poziva base kontruktor

        public CustomClaimsPrincipal(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        #endregion

        #region Propertiji

        public string Id { get { return FindFirst(ClaimTypes.NameIdentifier) != null ? FindFirst(ClaimTypes.NameIdentifier).Value : ""; } }
        public string Name { get { return FindFirst(ClaimTypes.Name) != null ? FindFirst(ClaimTypes.Name).Value : Email; } }
        public string FirstName { get { return FindFirst("FirstName") != null ? FindFirst("FirstName").Value : " "; } }
        public string LastName { get { return FindFirst("LastName") != null ? FindFirst("LastName").Value : " "; } }
        public string Email { get { return FindFirst(ClaimTypes.Email) != null ? FindFirst(ClaimTypes.Email).Value : ""; } }

        #endregion

    }
}
