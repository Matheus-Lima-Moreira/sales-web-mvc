using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sales_web_mvc.Models;
using sales_web_mvc.Models.ViewModels;
using sales_web_mvc.Services;
using sales_web_mvc.Services.Exceptions;

namespace sales_web_mvc.Controllers;

public class SellersController : Controller
{
  private readonly SellerService _sellerService;
  private readonly DepartmentService _departmentService;

  public SellersController(SellerService sellerService, DepartmentService departmentService)
  {
    _departmentService = departmentService;
    _sellerService = sellerService;
  }

  public IActionResult Index()
  {
    var list = _sellerService.FindAll();

    return View(list);
  }

  public IActionResult Create()
  {
    var departments = _departmentService.FindAll();

    var viewModel = new SellerFormViewModel
    {
      Departments = departments
    };

    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Create(Seller seller)
  {
    _sellerService.Insert(seller);
    return RedirectToAction(nameof(Index));
  }

  public IActionResult Delete(int? id)
  {
    if (id == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not provided" });
    }

    var seller = _sellerService.FindById(id.Value);
    if (seller == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not found" });
    }

    return View(seller);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Delete(int id)
  {
    _sellerService.Remove(id);
    return RedirectToAction(nameof(Index));
  }

  public IActionResult Details(int? id)
  {
    if (id == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not provided" });
    }

    var seller = _sellerService.FindById(id.Value);
    if (seller == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not found" });
    }

    return View(seller);
  }

  public IActionResult Edit(int? id)
  {
    if (id == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not provided" });
    }

    var seller = _sellerService.FindById(id.Value);
    if (seller == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not found" });
    }

    List<Department> departments = _departmentService.FindAll();

    SellerFormViewModel viewModel = new()
    {
      Seller = seller,
      Departments = departments
    };

    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Edit(int id, Seller seller)
  {
    if (id != seller.Id)
    {
      return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
    }

    try
    {
      _sellerService.Update(seller);
      return RedirectToAction(nameof(Index));
    }
    catch (ApplicationException ex)
    {
      return RedirectToAction(nameof(Error), new { message = ex.Message });
    }
  }

  public IActionResult Error(string message)
  {
    var viewModel = new ErrorViewModel
    {
      Message = message,
      RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
    };
    return View(viewModel);
  }
}