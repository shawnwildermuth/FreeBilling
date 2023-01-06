using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreeBilling.Pages;

public class IndexPageModel : PageModel
{
  private readonly IBillingRepository _repository;

  public IndexPageModel(IBillingRepository repository)
	{
    _repository = repository;
  }

  public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();

  public async Task OnGetAsync() // Syntax matters!
  {
    Customers = await _repository.GetAllCustomers();
  }
}