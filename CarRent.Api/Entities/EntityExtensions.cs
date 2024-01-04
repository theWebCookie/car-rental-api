using CarRent.Api.Dtos;

namespace CarRent.Api.Entities;
public static class EntityExtensions
{
    public static CarDto AsDto(this Car car)
    {
        return new CarDto(
            car.Id,
            car.Title,
            car.ImageUri,
            car.FuelType,
            car.Luggage,
            car.Doors,
            car.Seats,
            car.Transmission,
            car.FuelUsage,
            car.CarType,
            car.Description,
            car.Price
        );
    }
}