using Microsoft.EntityFrameworkCore;
using sales_web_mvc.Models;
using sales_web_mvc.Services.Exceptions;

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

  public void Insert(Seller obj)
  {
    _context.Add(obj);
    _context.SaveChanges();
  }

  public Seller? FindById(int id)
  {
    return _context.Seller
      .Include(obj => obj.Department)
      .FirstOrDefault(s => s.Id == id);
  }

  public void Remove(int id)
  {
    var obj = _context.Seller.Find(id);
    if (obj != null)
    {
      _context.Seller.Remove(obj);
      _context.SaveChanges();
    }
  }

  public void Update(Seller obj)
  {
    if (!_context.Seller.Any(s => s.Id == obj.Id))
    {
      throw new NotFoundException("Id not found");
    }

    try
    {
      _context.Update(obj);
      _context.SaveChanges();
    }
    catch (DbUpdateConcurrencyException e)
    {
      throw new DbConcurrencyException(e.Message);
    }
  }
}