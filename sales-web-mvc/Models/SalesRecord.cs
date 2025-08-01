using System.ComponentModel.DataAnnotations;
using sales_web_mvc.Models.Enums;

namespace sales_web_mvc.Models;

public class SalesRecord
{
  public int Id { get; set; }

  public DateTime Date { get; set; }

  [DisplayFormat(DataFormatString = "{0:F2}")]
  public double Amount { get; set; }

  public SaleStatus Status { get; set; }

  public Seller Seller { get; set; } = null!;

  public SalesRecord()
  {

  }

  public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
  {
    Id = id;
    Date = date;
    Amount = amount;
    Status = status;
    Seller = seller;
  }
}