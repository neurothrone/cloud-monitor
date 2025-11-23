using CloudMonitor.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudMonitor.Persistence.Data;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<PersonEntity> Persons => Set<PersonEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}