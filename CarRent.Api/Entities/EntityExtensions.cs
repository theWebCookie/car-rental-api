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
            car.Price,
            car.City,
            car.AvailabilityStart,
            car.AvailabilityEnd
        );
    }

    public static ReservationDto AsDto(this Reservation reservation)
    {
        return new ReservationDto(
            reservation.Id,
            reservation.StartDate,
            reservation.EndDate,
            reservation.UserId,
            reservation.CarId
        );
    }

    public static UserDto AsDto(this User user)
    {
        return new UserDto(
            user.Id,
            user.FirstName,
            user.SeccondName,
            user.Email,
            user.Password
        );
    }
}