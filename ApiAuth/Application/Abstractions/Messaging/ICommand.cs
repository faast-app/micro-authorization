using ApiAuth.Domain.Shared;
using MediatR;

namespace ApiAuth.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{ }
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{ }