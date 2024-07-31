namespace ApiAuth.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IUnitOfWorkIntegracion
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}