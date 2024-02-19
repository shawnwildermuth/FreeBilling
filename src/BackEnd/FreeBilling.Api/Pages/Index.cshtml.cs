using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreeBilling.Pages;

public class IndexPageModel : PageModel
{
  private readonly IBillingRepository _repository;
  private readonly UserManager<Employee> _usrManager;

  public IndexPageModel(IBillingRepository repository, UserManager<Employee> usrManager)
	{
    _repository = repository;
    _usrManager = usrManager;
  }

  public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();

  public async Task OnGetAsync() // Syntax matters!
  {
    Customers = await _repository.GetAllCustomers();

  }
}