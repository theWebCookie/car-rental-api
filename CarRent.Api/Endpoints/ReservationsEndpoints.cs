using CarRent.Api.Authorization;
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

    group.MapGet("/", async (IReservationsRepository repository) => (await repository.GetAllAsync()).Select(reservation => reservation.AsDto()));

    group.MapGet("/{id}", async (IReservationsRepository repository, int id) =>
    {
      Reservation? reservation = await repository.GetAsync(id);
      return reservation is not null ? Results.Ok(reservation.AsDto()) : Results.NotFound();
    })
    .WithName(GetReservationEndpointName)
    .RequireAuthorization(Policies.ReadAccess);

    group.MapPost("/", async (IReservationsRepository repository, CreateReservationDto reservationDto) =>
    {
      Reservation reservation = new()
      {
        StartDate = reservationDto.StartDate,
        EndDate = reservationDto.EndDate,
        UserId = reservationDto.UserId,
        CarId = reservationDto.CarId
      };

      await repository.CreateAsync(reservation);
      return Results.CreatedAtRoute(GetReservationEndpointName, new { id = reservation.Id }, reservation);
    })
    .RequireAuthorization(policy =>
    {
      policy.RequireRole("Admin");
    });

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

      await repository.UpdateAsync(existingReservation);
      return Results.NoContent();
    })
    .RequireAuthorization(policy =>
    {
      policy.RequireRole("Admin");
    });

    group.MapDelete("/{id}", async (IReservationsRepository repository, int id) =>
    {
      Reservation? reservation = await repository.GetAsync(id);
      if (reservation is not null)
      {
        await repository.DeleteAsync(id);
      }

      return Results.NoContent();
    })
    .RequireAuthorization(policy =>
    {
      policy.RequireRole("Admin");
    });

    return group;
  }
}