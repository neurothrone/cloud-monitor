using CloudMonitor.Persistence.Data;
using CloudMonitor.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudMonitor.Persistence.Repositories;

public class PersonRepository(CloudMonitorDbContext context)
{
    public async Task<List<PersonEntity>> GetAllPersonsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Persons
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<PersonEntity?> GetPersonByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PersonEntity> CreatePersonAsync(
        PersonEntity person,
        CancellationToken cancellationToken = default)
    {
        context.Persons.Add(person);
        await context.SaveChangesAsync(cancellationToken);
        return person;
    }

    public async Task<bool> UpdatePersonAsync(
        PersonEntity person,
        CancellationToken cancellationToken = default)
    {
        context.Persons.Update(person);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeletePersonAsync(int id, CancellationToken cancellationToken = default)
    {
        var person = await GetPersonByIdAsync(id, cancellationToken);
        if (person is null)
            return false;

        context.Persons.Remove(person);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}