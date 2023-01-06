using FreeBilling.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeBilling.Data;

public class BillingRepository : IBillingRepository
{
  private readonly BillingContext _context;

  public BillingRepository(BillingContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Customer>> GetAllCustomers()
  {
    return await _context.Customers
      .Include(c => c.Address)
      .OrderBy(c => c.CompanyName)
      .ToArrayAsync();
  }
}