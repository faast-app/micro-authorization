using ApiAuth.Domain.Entities;
using ApiAuth.Domain.Repositories;
using ApiAuth.Persistence.Data;

namespace ApiAuth.Persistence.Repositories;

internal sealed class AuthRepository : IntegracionRepository<AuthEntity>, IAuthRepository
{
    public AuthRepository(IntegracionDbContext context)
            : base(context)
    {
    }
}