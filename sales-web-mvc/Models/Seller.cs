namespace sales_web_mvc.Models;

public class Seller
{
  public int Id { get; set; }
  public string Name { get; set; } = "";
  public string Email { get; set; } = "";
  public double BaseSalary { get; set; }
  public DateTime BirthDate { get; set; }
  public Department Department { get; set; } = null!;
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