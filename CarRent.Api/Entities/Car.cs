using System.ComponentModel.DataAnnotations;

namespace CarRent.Api.Entities
{
  public class Car
  {
    public int Id { get; set; }
    [Required]
    public required string Title { get; set; }
    [Url]
    [StringLength(100)]
    public required string ImageUri { get; set; }
    [Required]
    public required string FuelType { get; set; }
    [Required]
    public required double Luggage { get; set; }
    [Required]
    public required int Doors { get; set; }
    [Required]
    public required int Seats { get; set; }
    [Required]
    public required double FuelUsage { get; set; }
    [Required]
    public required string CarType { get; set; }
    [Required]
    public required string Transmission { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public required decimal Price { get; set; }
  }
}