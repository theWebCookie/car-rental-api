using System.Security.Claims;
using CarRent.Api.Dtos;
using CarRent.Api.Entities;
using CarRent.Api.Repositories;

namespace CarRent.Api.Endpoints;

public static class ReservationsEndpoints
{
  const string GetReservationEndpointName = "GetReservationById";
  public static RouteGroupBuilder MapReservationsEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/reservations").WithParameterValidation();

    group.MapGet("/", async (IReservationsRepository repository) => (await repository.GetAllAsync()).Select(reservation => reservation.AsDto()))
    .RequireAuthorization()
    .RequireAuthorization("AdminPolicy");

    group.MapGet("/{id}", async (IReservationsRepository repository, int id) =>
    {
      Reservation? reservation = await repository.GetAsync(id);
      return reservation is not null ? Results.Ok(reservation.AsDto()) : Results.NotFound();
    })
    .WithName(GetReservationEndpointName)
    .RequireAuthorization()
    .RequireAuthorization("AdminPolicy");

    group.MapGet("/user/{userId}", async (IReservationsRepository repository, int userId, ClaimsPrincipal user) =>
    {
      if (user == null || user.Claims.All(c => c.Value != userId.ToString()))
      {
        return Results.Unauthorized();
      }

      IEnumerable<Reservation> reservations = await repository.GetReservationsByUserIdAsync(userId);

      return Results.Ok(reservations.Select(reservation => reservation.AsDto()));
    })
    .WithName("GetUserReservationsEndpoint")
    .RequireAuthorization();

    group.MapPost("/", async (IReservationsRepository repository, CreateReservationDto reservationDto) =>
    {
      Reservation reservation = new()
      {
        StartDate = reservationDto.StartDate,
        EndDate = reservationDto.EndDate,
        UserId = reservationDto.UserId,
        CarId = reservationDto.CarId,
        Price = reservationDto.Price,
        Options = reservationDto.Options
      };

      await repository.CreateAsync(reservation);
      return Results.CreatedAtRoute(GetReservationEndpointName, new { id = reservation.Id }, reservation);
    })
    .RequireAuthorization();

    group.MapPut("/{id}", async (IReservationsRepository repository, int id, UpdateReservationDto updatedReservationDto) =>
    {
      Reservation? existingReservation = await repository.GetAsync(id);
      if (existingReservation is null)
      {
        return Results.NotFound();
      }

      existingReservation.StartDate = updatedReservationDto.StartDate;
      existingReservation.EndDate = updatedReservationDto.EndDate;
      existingReservation.UserId = updatedReservationDto.UserId;
      existingReservation.CarId = updatedReservationDto.CarId;
      existingReservation.Price = updatedReservationDto.Price;
      existingReservation.Options = updatedReservationDto.Options;

      await repository.UpdateAsync(existingReservation);
      return Results.NoContent();
    })
    .RequireAuthorization()
    .RequireAuthorization("AdminPolicy");

    group.MapDelete("/{id}", async (IReservationsRepository repository, int id) =>
    {
      Reservation? reservation = await repository.GetAsync(id);
      if (reservation is not null)
      {
        await repository.DeleteAsync(id);
      }

      return Results.NoContent();
    })
    .RequireAuthorization()
    .RequireAuthorization("AdminPolicy");

    return group;
  }
}