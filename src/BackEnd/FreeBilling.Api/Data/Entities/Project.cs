using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Data.Entities;

public class Project
{
  public int Id { get; set; }
  public string? ProjectName { get; set; }
  public Customer? Customer { get; set; }
  public int? CustomerId { get; set; }
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }
}
