using ApiAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiAuth.Application.Abstractions.Data;

public interface IIntegracionDbContext
{
    public DbSet<AuthEntity> Auth { get; set; }
}