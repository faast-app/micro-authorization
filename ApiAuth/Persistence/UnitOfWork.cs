using ApiAuth.Domain.Repositories;
using ApiAuth.Persistence.Data;

namespace ApiAuth.Persistence;

internal sealed class UnitOfWork: IUnitOfWork
{
    private readonly FintecDbContext _dbContext;

    public UnitOfWork(FintecDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}