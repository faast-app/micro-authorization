using ApiAuth.Domain.Primitives;

namespace ApiAuth.Application.Abstractions.Data;

public interface IFintecRepository<TEntity>
    where TEntity : Entity
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    IQueryable<TEntity> GetQueryable();
    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Remove(TEntity entity);
    Task<int> SaveChangesAsync();
}

public interface IIntegracionRepository<TEntity>
    where TEntity : Entity
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    IQueryable<TEntity> GetQueryable();
    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Remove(TEntity entity);
    Task<int> SaveChangesAsync();
}