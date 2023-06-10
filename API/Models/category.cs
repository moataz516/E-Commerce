namespace E_Website.Models
{
    public class category
    {
        public string categoryId { get; set; }
        public string name { get; set; }
        public string? icon { get; set; }   
        public ICollection<product> product { get; set; }

    }
}
