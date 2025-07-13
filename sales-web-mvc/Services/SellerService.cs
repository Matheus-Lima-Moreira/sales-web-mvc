using System.Threading.Tasks;
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

  public async Task<List<Seller>> FindAllAsync()
  {
    return await _context.Seller.ToListAsync();
  }

  public async Task InsertAsync(Seller obj)
  {
    await _context.AddAsync(obj);
    await _context.SaveChangesAsync();
  }

  public async Task<Seller?> FindByIdAsync(int id)
  {
    return await _context.Seller
      .Include(obj => obj.Department)
      .FirstOrDefaultAsync(s => s.Id == id);
  }

  public async Task RemoveAsync(int id)
  {
    var obj = await _context.Seller.FindAsync(id);
    if (obj != null)
    {
      _context.Seller.Remove(obj);
      await _context.SaveChangesAsync();
    }
  }

  public async Task UpdateAsync(Seller obj)
  {
    bool exists = await _context.Seller.AnyAsync(s => s.Id == obj.Id);
    if (!exists)
    {
      throw new NotFoundException("Id not found");
    }

    try
    {
      _context.Update(obj);
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException e)
    {
      throw new DbConcurrencyException(e.Message);
    }
  }
}