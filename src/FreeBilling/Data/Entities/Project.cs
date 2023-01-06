using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Data.Entities;

public class Project
{
  public int Id { get; set; }
  [Required]
  [MaxLength(255)]
  public string? ProjectName { get; set; }
  [Required]
  public Customer? Customer { get; set; }
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }
}
