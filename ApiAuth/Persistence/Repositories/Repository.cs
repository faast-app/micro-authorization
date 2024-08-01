using ApiAuth.Application.Abstractions.Data;
using ApiAuth.Domain.Primitives;
using ApiAuth.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiAuth.Persistence.Repositories;

internal abstract class IntegracionRepository<TEntity> : IIntegracionRepository<TEntity>
    where TEntity : Entity
{
    protected readonly IntegracionDbContext DbContext;

    protected IntegracionRepository(IntegracionDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbContext.Set<TEntity>().ToListAsync();
    }
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }
    public IQueryable<TEntity> GetQueryable()
    {
        return DbContext.Set<TEntity>();
    }
    public void Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }
    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }
    public void Remove(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
    public Task<int> SaveChangesAsync()
    {
        return DbContext.SaveChangesAsync();
    }
}