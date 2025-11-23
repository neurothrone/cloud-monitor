using CloudMonitor.Api.Domain;
using CloudMonitor.Persistence.Entities;
using CloudMonitor.Persistence.Repositories;

namespace CloudMonitor.Api.Endpoints;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/persons")
            .WithTags("Persons");

        group.MapGet("", async Task<IResult> (
            PersonRepository repository,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var people = await repository.GetAllPersonsAsync(cancellationToken);
                return TypedResults.Ok(people);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapGet("/{id:int:min(0)}", async Task<IResult> (
            int id,
            PersonRepository repository,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var person = await repository.GetPersonByIdAsync(id, cancellationToken);
                return person is not null
                    ? TypedResults.Ok(person)
                    : TypedResults.NotFound();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapPost("", async Task<IResult> (
            PersonInputRequest request,
            PersonRepository repository,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var entity = new PersonEntity { Name = request.Name, Email = request.Email };
                var createdPerson = await repository.CreatePersonAsync(entity, cancellationToken);
                return TypedResults.Created($"/api/v1/persons/{createdPerson.Id}", createdPerson);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapPut("/{id:int:min(0)}", async Task<IResult> (
            int id,
            PersonInputRequest request,
            PersonRepository repository,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var existingPerson = await repository.GetPersonByIdAsync(id, cancellationToken);
                if (existingPerson is null)
                    return TypedResults.NotFound();

                existingPerson.Name = request.Name;
                existingPerson.Email = request.Email;

                var updated = await repository.UpdatePersonAsync(existingPerson, cancellationToken);
                return updated
                    ? TypedResults.NoContent()
                    : TypedResults.Problem("Failed to update person.",
                        statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        group.MapDelete("/{id:int:min(0)}", async Task<IResult> (
            int id,
            PersonRepository repository,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var deleted = await repository.DeletePersonAsync(id, cancellationToken);
                return deleted
                    ? TypedResults.NoContent()
                    : TypedResults.NotFound();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        });
    }
}