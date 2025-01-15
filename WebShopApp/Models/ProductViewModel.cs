namespace WebShopApp.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string Availability { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}
