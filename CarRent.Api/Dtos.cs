using System.ComponentModel.DataAnnotations;

namespace CarRent.Api.Dtos;

public record CarDto(
    int Id,
    string Title,
    string ImageUri,
    double Fuel,
    double Luggage,
    int Doors,
    int Seats,
    string Transmission,
    double FuelUsage,
    string CarType,
    string Description,
    decimal Price
);
public record CreateCarDto(
  [Required] string Title,
  [Required][Url][StringLength(100)] string ImageUri,
  [Required] double Fuel,
  [Required] double Luggage,
  [Required] int Doors,
  [Required] int Seats,
  [Required] string Transmission,
  [Required] double FuelUsage,
  [Required] string CarType,
  [Required] string Description,
  [Required] decimal Price
);
public record UpdateCarDto(
  [Required] string Title,
  [Required][Url][StringLength(100)] string ImageUri,
  [Required] double Fuel,
  [Required] double Luggage,
  [Required] int Doors,
  [Required] int Seats,
  [Required] string Transmission,
  [Required] double FuelUsage,
  [Required] string CarType,
  [Required] string Description,
  [Required] decimal Price
);