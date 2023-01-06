using FreeBilling.Data.Entities;

namespace FreeBilling.Data
{
  public interface IBillingRepository
  {
    Task<IEnumerable<Customer>> GetAllCustomers();
  }
}