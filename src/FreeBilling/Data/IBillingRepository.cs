using FreeBilling.Data.Entities;

namespace FreeBilling.Data
{
  public interface IBillingRepository
  {
    void Add<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
    Task<bool> SaveChanges();

    Task<IEnumerable<Customer>> GetAllCustomers();
    Task<Customer?> GetCustomer(int id);

    Task<IEnumerable<Project>> GetCustomerProjects(int custId);

    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<BillingUser?> GetUser(string username);
  }
}