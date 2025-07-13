using sales_web_mvc.Models;

namespace sales_web_mvc.Services;

public class DepartmentService
{
  private readonly SalesWebMvcContext _context;

  public DepartmentService(SalesWebMvcContext context)
  {
    _context = context;
  }

  public List<Department> FindAll()
  {
    return _context.Department.OrderBy(o => o.Name).ToList();
  }
}