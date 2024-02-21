using FreeBilling.Data.Entities;

namespace FreeBilling.Api.Models;

public class ProjectModel
{
  public int Id { get; set; }
  public string? ProjectName { get; set; }
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }

  public double ProjectTotal { get; set; }
}
