namespace WebShopApp.Models
{
    public class ReviewViewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string ReviewerName { get; set; }
    }
}
