using System.ComponentModel.DataAnnotations;
using CarRent.Api.Entities;

namespace CarRent.Api.Dtos;

public record CarDto(
    int Id,
    string Title,
    string ImageUri,
    string FuelType,
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
  [Required] string FuelType,
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
  [Required] string FuelType,
  [Required] double Luggage,
  [Required] int Doors,
  [Required] int Seats,
  [Required] string Transmission,
  [Required] double FuelUsage,
  [Required] string CarType,
  [Required] string Description,
  [Required] decimal Price
);

public record ReservationDto(
    int Id,
    DateTime StartDate,
    DateTime EndDate,
    int UserId,
    int CarId
);

public record CreateReservationDto(
  [Required] DateTime StartDate,
  [Required] DateTime EndDate,
  [Required] int UserId,
  [Required] int CarId
);

public record UpdateReservationDto(
  [Required] DateTime StartDate,
  [Required] DateTime EndDate,
  [Required] int UserId,
  [Required] int CarId
);

public record UserDto(
    int Id,
    string FirstName,
    string SeccondName,
    string Email,
    string Password
);

public record CreateUserDto(
  [Required] string FirstName,
  [Required] string SeccondName,
  [Required] string Email,
  [Required] string Password
);

public record UpdateUserDto(
  [Required] string FirstName,
  [Required] string SeccondName,
  [Required] string Email,
  [Required] string Password
);