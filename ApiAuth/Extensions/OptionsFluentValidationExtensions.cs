namespace ApiAuth.Extensions;

public static class OptionsFluentValidationExtensions
{
    public static IServiceCollection AddOptionsWithFluentValidation<TOptions>(
        this IServiceCollection services,
        string configurationSection
        )
        where TOptions : class
    {

        return services;
    }
}
