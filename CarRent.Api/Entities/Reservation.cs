using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.Api.Entities;

public class Reservation
{
    public int Id { get; set; }
    [Required]
    public required DateTime StartDate { get; set; }
    [Required]
    public required DateTime EndDate { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public required int UserId { get; set; }
    [Required]
    [ForeignKey("CarId")]
    public required int CarId { get; set; }
    public required decimal Price { get; set; }
}