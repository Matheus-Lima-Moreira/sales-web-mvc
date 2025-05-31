using Microsoft.AspNetCore.Mvc;
using sales_web_mvc.Models;

namespace sales_web_mvc.Controllers;

public class DepartmentsController : Controller
{
  public IActionResult Index()
  {
    var departments = new List<Department>
    {
      new() { Id = 1, Name = "Electronics" },
      new() { Id = 3, Name = "Home & Kitchen" }
    };

    return View(departments);
  }
}