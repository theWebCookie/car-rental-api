namespace CarRent.Api.Entities
{
  public class Car
  {
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string ImageUri { get; set; }
    public required double Fuel { get; set; }
    public required double Luggage { get; set; }
    public required int Doors { get; set; }
    public required int Seats { get; set; }
    public required double FuelUsage { get; set; }
    public required string CarType { get; set; }
    public required string Transmission { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
  }
}