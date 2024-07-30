using ApiAuth.Application.Abstractions.Data;
using ApiAuth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiAuth.Persistence.Data;

public class FintecDbContext : DbContext, IFintecDbContext, IUnitOfWork
{
    

    public FintecDbContext
    (
        DbContextOptions<FintecDbContext> options
    ) : base(options)
    {

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}