namespace ApiAuth.Domain.Shared;

public sealed record Error(string Code, string? Message = null)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");
    public static implicit operator Result(Error error) => Result.Failure(error);
}