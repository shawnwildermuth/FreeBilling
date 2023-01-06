using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Data.Entities;

public class Address
{
  public int Id { get; set; }
  [Required]
  [MaxLength(255)]
  public string? AddressLine1 { get; set; }
  [MaxLength(255)]
  public string? AddressLine2 { get; set; }
  [MaxLength(255)]
  public string? AddressLine3 { get; set; }
  [Required]
  [MinLength(3)]
  [MaxLength(255)]
  public string? City { get; set; }
  [Required]
  [MaxLength(25)]
  public string? StateProvince{ get; set; }
  [Required]
  [MaxLength(15)]
  public string? PostalCode { get; set; }
  public string? Country { get; set; }
}