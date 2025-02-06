using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopApp.DAL.Models
{
    [Table("tUserOrder")]
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public double EstimatedTotal { get; set; }

        public bool isPaid { get; set; } = false;

        public DateTime PaidAt { get; set; }

        public bool isDelivered { get; set; } = false;

        public DateTime? DeliveredAt { get; set; }   
    }
}
