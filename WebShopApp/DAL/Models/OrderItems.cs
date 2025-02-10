using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopApp.DAL.Models
{
    [Table("tOrderItem")]
    public class OrderItems
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Qty { get; set; }
    }
}
