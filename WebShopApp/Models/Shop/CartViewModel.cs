namespace WebShopApp.Models.Shop
{
    public class CartViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
