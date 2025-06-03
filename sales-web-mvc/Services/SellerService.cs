using sales_web_mvc.Models;

namespace sales_web_mvc.Services;

public class SellerService
{
  private readonly SalesWebMvcContext _context;

  public SellerService(SalesWebMvcContext context)
  {
    _context = context;
  }

  public List<Seller> FindAll()
  {
    return _context.Seller.ToList();
  }
}