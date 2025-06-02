using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sales_web_mvc.Models;

public class SalesWebMvcContext : DbContext
{
    public SalesWebMvcContext(DbContextOptions<SalesWebMvcContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Department { get; set; } = default!;
    public DbSet<SalesRecord> SalesRecords { get; set; } = default!;
    public DbSet<Seller> Seller { get; set; } = default!;
}
