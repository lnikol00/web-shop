using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopApp.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [MaxLength(256)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public byte[]? AvatarImage { get; set; }


        [Display(Name = "Type")]
        public string? ContentType { get; set; }


        [Display(Name = "File Name")]
        public string? FileName { get; set; }

        [MaxLength(16)]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [MaxLength(128)]
        [Display(Name = "Adress")]
        public string? Adresa { get; set; }

        [Display(Name = "User activity")]
        public bool IsUserActive { get; set; }

        public string FirstNameLastName
        {
            get { return this.FirstName + " " + this.LastName; }
            private set { value = this.FirstName + " " + this.LastName; }
        }
    }
}
