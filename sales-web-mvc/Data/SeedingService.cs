using sales_web_mvc.Models.Enums;

namespace sales_web_mvc.Models;

public class SeedingService
{
  private readonly SalesWebMvcContext _context;

  public SeedingService(SalesWebMvcContext context)
  {
    _context = context;
  }

  public void Seed()
  {
    if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
    {
      return; // DB has been seeded
    }

    Department d1 = new Department(1, "Computers");
    Department d2 = new Department(2, "Electronics");
    Department d3 = new Department(3, "Fashion");
    Department d4 = new Department(4, "Books");

    _context.Department.AddRange(d1, d2, d3, d4);

    Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", 1000.0, new DateTime(1998, 4, 21), d1);
    Seller s2 = new Seller(2, "Maria Green", "maria@gmail.com", 3500.0, new DateTime(1997, 12, 31), d2);
    Seller s3 = new Seller(3, "Alex Gray", "alex@gmail.com", 2200.0, new DateTime(1995, 1, 15), d1);
    Seller s4 = new Seller(4, "Martha Red", "martha@gmail.com", 3000.0, new DateTime(1990, 6, 30), d3);
    Seller s5 = new Seller(5, "Donald Blue", "donald@gmail.com", 4000.0, new DateTime(1992, 3, 10), d4);
    Seller s6 = new Seller(6, "Alex Pink", "alexpink@gmail.com", 3000.0, new DateTime(1998, 2, 28), d2);

    _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);

    SalesRecord r1 = new SalesRecord(1, new DateTime(2023, 1, 25), 11000.0, SaleStatus.Billed, s1);
    SalesRecord r2 = new SalesRecord(2, new DateTime(2023, 2, 4), 7000.0, SaleStatus.Billed, s5);
    SalesRecord r3 = new SalesRecord(3, new DateTime(2023, 3, 13), 4000.0, SaleStatus.Canceled, s4);
    SalesRecord r4 = new SalesRecord(4, new DateTime(2023, 4, 1), 8000.0, SaleStatus.Billed, s3);
    SalesRecord r5 = new SalesRecord(5, new DateTime(2023, 4, 21), 3000.0, SaleStatus.Pending, s2);
    SalesRecord r6 = new SalesRecord(6, new DateTime(2023, 5, 15), 2000.0, SaleStatus.Billed, s1);
    SalesRecord r7 = new SalesRecord(7, new DateTime(2023, 6, 10), 9000.0, SaleStatus.Billed, s6);
    SalesRecord r8 = new SalesRecord(8, new DateTime(2023, 7, 8), 12000.0, SaleStatus.Pending, s2);
    SalesRecord r9 = new SalesRecord(9, new DateTime(2023, 8, 15), 15000.0, SaleStatus.Billed, s4);
    SalesRecord r10 = new SalesRecord(10, new DateTime(2023, 9, 25), 5000.0, SaleStatus.Canceled, s5);

    _context.SalesRecord.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10);

    _context.SaveChanges();
  }
}