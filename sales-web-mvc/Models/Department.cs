namespace sales_web_mvc.Models;

public class Department
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public ICollection<Seller> Sellers { get; set; } = [];

  public Department()
  {

  }

  public Department(int id, string name)
  {
    Id = id;
    Name = name;
  }

  public void AddSeller(Seller seller)
  {
    Sellers.Add(seller);
  }

  public double TotalSales(DateTime initial, DateTime final)
  {
    return Sellers.Sum(seller => seller.TotalSales(initial, final));
  }
}