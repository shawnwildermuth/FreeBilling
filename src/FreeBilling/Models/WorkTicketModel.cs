using FreeBilling.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Models;

public class WorkTicketModel
{
  public int Id { get; set; }
  public int EmployeeId { get; set; }
  [Range(.1, 12)]
  public double Hours { get; set; }
  [Range(10, 1000)]
  public double BillingRate { get; set; }
  [Required]
  public DateTime? Date { get; set; }
  [Required]
  public int ProjectId { get; set; }
  [Required]
  [MaxLength(5000)]
  public string? WorkPerformed { get; set; }

}