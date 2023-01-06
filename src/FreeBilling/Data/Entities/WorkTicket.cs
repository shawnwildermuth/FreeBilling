using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Data.Entities;

public class WorkTicket
{
  public int Id { get; set; }
  public Employee? Employee { get; set; }
  [Range(.1, 12)]
  public double Hours { get; set; }
  [Range(10, 1000)]
  public double BillingRate { get; set; }
  [Required]
  public DateTime? Date { get; set; }
  [Required]
  public Project? Project { get; set; }
  [Required]
  [MaxLength(5000)]
  public string? WorkPerformed { get; set; }
}
