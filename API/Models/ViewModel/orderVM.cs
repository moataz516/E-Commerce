namespace E_Website.Models.ViewModel
{
    public class orderVM

    {
        public string userId { get; set; }
        public string userName { get; set; }
        public decimal total { get; set; }
        public IEnumerable<cartVM> cart { get; set; }


    }
}
