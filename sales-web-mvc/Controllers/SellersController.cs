using System.Diagnostics;
using System.Threading.Tasks;
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

  public async Task<IActionResult> Index()
  {
    var list = await _sellerService.FindAllAsync();

    return View(list);
  }

  public async Task<IActionResult> Create()
  {
    var departments = await _departmentService.FindAllAsync();

    var viewModel = new SellerFormViewModel
    {
      Departments = departments
    };

    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(Seller seller)
  {
    if (!ModelState.IsValid)
    {
      var departments = await _departmentService.FindAllAsync();
      var viewModel = new SellerFormViewModel
      {
        Seller = seller,
        Departments = departments
      };
      return View(viewModel);
    }

    await _sellerService.InsertAsync(seller);

    return RedirectToAction(nameof(Index));
  }

  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not provided" });
    }

    var seller = await _sellerService.FindByIdAsync(id.Value);
    if (seller == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not found" });
    }

    return View(seller);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Delete(int id)
  {
    await _sellerService.RemoveAsync(id);
    return RedirectToAction(nameof(Index));
  }

  public async Task<IActionResult> Details(int? id)
  {
    if (id == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not provided" });
    }

    var seller = await _sellerService.FindByIdAsync(id.Value);
    if (seller == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not found" });
    }

    return View(seller);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not provided" });
    }

    var seller = await _sellerService.FindByIdAsync(id.Value);
    if (seller == null)
    {
      return RedirectToAction(nameof(Error), new { message = "Id not found" });
    }

    List<Department> departments = await _departmentService.FindAllAsync();

    SellerFormViewModel viewModel = new()
    {
      Seller = seller,
      Departments = departments
    };

    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, Seller seller)
  {
    if (!ModelState.IsValid)
    {
      var departments = await _departmentService.FindAllAsync();
      var viewModel = new SellerFormViewModel
      {
        Seller = seller,
        Departments = departments
      };
      return View(viewModel);
    }

    if (id != seller.Id)
    {
      return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
    }

    try
    {
      await _sellerService.UpdateAsync(seller);
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