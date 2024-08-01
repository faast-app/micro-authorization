namespace ApiAuth.Domain.Repositories;

public interface IUnitOfWorkIntegracion
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}