using System.ComponentModel.DataAnnotations;

namespace CarRent.Api.Entities;
public class User
{
    public int Id { get; set; }
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string SeccondName { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }

    public ICollection<Reservation>? Reservations { get; set; }
}