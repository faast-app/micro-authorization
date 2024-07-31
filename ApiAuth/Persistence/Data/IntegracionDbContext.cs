using ApiAuth.Application.Abstractions.Data;
using ApiAuth.Domain.Entities;
using ApiAuth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiAuth.Persistence.Data;

public class IntegracionDbContext : DbContext, IIntegracionDbContext, IUnitOfWorkIntegracion
{
    public IntegracionDbContext
    (
        DbContextOptions<IntegracionDbContext> options
    ) : base(options)
    {

    }
    public DbSet<AuthEntity> Auth { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}