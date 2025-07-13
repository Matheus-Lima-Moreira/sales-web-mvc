using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace sales_web_mvc.Models;

public class Seller
{
  public int Id { get; set; }

  [Required(ErrorMessage = "{0} is required.")]
  [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
  public string Name { get; set; } = "";

  [Required(ErrorMessage = "{0} is required.")]
  [EmailAddress(ErrorMessage = "Enter a valid {0}.")]
  [DataType(DataType.EmailAddress)]
  public string Email { get; set; } = "";

  [Required(ErrorMessage = "{0} is required.")]
  [Range(100.0, 50000.0, ErrorMessage = "{0} must be between {1} and {2}.")]
  [Display(Name = "Base Salary")]
  [DisplayFormat(DataFormatString = "{0:F2}")]
  public double BaseSalary { get; set; }

  [Required(ErrorMessage = "{0} is required.")]
  [Display(Name = "Birth Date")]
  [DataType(DataType.Date)]
  public DateTime BirthDate { get; set; }

  [ValidateNever]
  public Department Department { get; set; } = null!;

  [Display(Name = "Department")]
  public int DepartmentId { get; set; }

  public ICollection<SalesRecord> Sales { get; set; } = [];

  public Seller()
  {

  }

  public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Department department)
  {
    Id = id;
    Name = name;
    Email = email;
    BaseSalary = baseSalary;
    BirthDate = birthDate;
    Department = department;
  }

  public void AddSales(SalesRecord sale)
  {
    Sales.Add(sale);
  }

  public void RemoveSales(SalesRecord sale)
  {
    Sales.Remove(sale);
  }

  public double TotalSales(DateTime initial, DateTime final)
  {
    return Sales.Where(sale => sale.Date >= initial && sale.Date <= final).Sum(sale => sale.Amount);
  }
}