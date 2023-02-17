using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Data.Entities;

public class WorkTicket
{
  public int Id { get; set; }
  public Employee? Employee { get; set; }
  public double Hours { get; set; }
  public double BillingRate { get; set; }
  public DateTime? Date { get; set; }
  public Project? Project { get; set; }
  public string? WorkPerformed { get; set; }
}
