using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sales_web_mvc.Models;

namespace sales_web_mvc.Services;

public class DepartmentService
{
  private readonly SalesWebMvcContext _context;

  public DepartmentService(SalesWebMvcContext context)
  {
    _context = context;
  }

  public async Task<List<Department>> FindAllAsync()
  {
    return await _context.Department.OrderBy(o => o.Name).ToListAsync();
  }
}