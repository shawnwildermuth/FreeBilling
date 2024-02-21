using FreeBilling.Api.Models;
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
    Task<CustomerDetailsModel?> GetCustomerDetails(int id);

    Task<IEnumerable<Project>> GetCustomerProjects(int custId);
    Task<Project?> GetProject(int id);

    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee?> GetEmployee(string username);
  }
}