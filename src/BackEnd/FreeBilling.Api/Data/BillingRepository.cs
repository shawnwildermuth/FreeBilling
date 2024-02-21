using FreeBilling.Api.Models;
using FreeBilling.Data.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FreeBilling.Data;

public class BillingRepository : IBillingRepository
{
  private readonly BillingContext _context;

  public BillingRepository(BillingContext context)
  {
    _context = context;
  }

  public void Add<T>(T entity) where T : class
  {
    _context.Add(entity);
  }

  public async Task<IEnumerable<Customer>> GetAllCustomers()
  {
    return await _context.Customers
      .Include(c => c.Address)
      .OrderBy(c => c.CompanyName)
      .ToArrayAsync();
  }

  public async Task<Customer?> GetCustomer(int id)
  {
    return await _context.Customers
      .Include(c => c.Address)
      .Where(c => c.Id == id)
      .FirstOrDefaultAsync();
  }

  public void Remove<T>(T entity) where T : class
  {
    _context.Remove(entity);
  }

  public async Task<bool> SaveChanges()
  {
    var result = await _context.SaveChangesAsync();
    return result > 0;
  }

  public async Task<IEnumerable<Project>> GetCustomerProjects(int custId)
  {
    return await _context.Projects
      .Where(p => p.Customer != null && p.Customer.Id == custId)
      .OrderBy(p => p.ProjectName)
      .ToArrayAsync();
  }

  public async Task<IEnumerable<Employee>> GetAllEmployees()
  {
    return await _context.Employees
      .OrderBy(e => e.Name)
      .ToArrayAsync();
  }

  public async Task<Employee?> GetEmployee(string username)
  {
    return await _context.Employees
      .Where(u => u.UserId == username)
      .FirstOrDefaultAsync();
  }

  public async Task<Project?> GetProject(int id)
  {
    return await _context.Projects
      .Where(p => p.Id == id)
      .FirstOrDefaultAsync();
  }

  public async Task<CustomerDetailsModel?> GetCustomerDetails(int id)
  {
    var customer = await _context.Customers
      .Include(c => c.Address)
      .Where(c => c.Id == id)
      .FirstOrDefaultAsync();

    if (customer is null) return null;

    var details = customer.Adapt<CustomerDetailsModel>();

    var projects = await GetCustomerProjects(id);

    if (projects.Any())
    {
      details.Projects = projects.Adapt<ICollection<ProjectModel>>();
      
      // Calculate Totals
      foreach (var project in details.Projects)
      {
        project.ProjectTotal = await _context.WorkTickets
          .Where(t => t.Project != null && t.Project.Id == project.Id)
          .SumAsync(t => t.Hours * t.BillingRate);
      }

      details.TotalBalance = details.Projects
        .Sum(p => p.ProjectTotal);
    }

    return details;

  }
}