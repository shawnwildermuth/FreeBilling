using FreeBilling.Data.Entities;

namespace FreeBilling.Data
{
  public interface IBillingRepository
  {
    Task<IEnumerable<Customer>> GetAllCustomers();
    Task<Customer?> GetCustomer(int id);

    void Add<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
    Task<bool> SaveChanges();
    Task<IEnumerable<Project>> GetCustomerProjects(int custId);
  }
}