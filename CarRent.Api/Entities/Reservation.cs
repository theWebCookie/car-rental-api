using System.ComponentModel.DataAnnotations;

namespace CarRent.Api.Entities;

public class Reservation
{
    public int Id { get; set; }
    [Required]
    public required int Quantity { get; set; }
    [Required]
    public required DateTime StartDate { get; set; }
    [Required]
    public required DateTime EndDate { get; set; }

    [Required]
    public required int UserId { get; set; }
    [Required]
    public required User User { get; set; }
    [Required]
    public required int CarId { get; set; }
    [Required]
    public required Car Car { get; set; }
}