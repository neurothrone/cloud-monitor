using CloudMonitor.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudMonitor.Persistence.Data;

public class CloudMonitorDbContext(DbContextOptions<CloudMonitorDbContext> options) : DbContext(options)
{
    public DbSet<PersonEntity> Persons => Set<PersonEntity>();
}