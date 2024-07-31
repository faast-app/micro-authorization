using ApiAuth.Application.Abstractions.Data;
using ApiAuth.Domain.Entities;

namespace ApiAuth.Domain.Repositories;

public interface IAuthRepository: IIntegracionRepository<AuthEntity>
{
}