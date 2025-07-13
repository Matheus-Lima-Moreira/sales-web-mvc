
namespace sales_web_mvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; } = null!;
        public ICollection<Department> Departments { get; set; } = new List<Department>();
    }
}