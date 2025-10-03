using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ApiAuth.Extensions;

public static class SwaggerExtension
{
    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v2.1.10",
                Title = "API Auth",
                Description = "REST API",
                Contact = new OpenApiContact()
                {
                    Name = "Guido Matos",
                    Email = "guido.matos@faast.cl"
                },
                Extensions = new Dictionary<string, IOpenApiExtension>
                {
                    {
                    "x-logo", new OpenApiObject
                        {
                            {"url", new OpenApiString("https://faast.cl/wp-content/uploads/2023/01/logo-faast-blanco-1.png")},
                            {"altText", new OpenApiString("FaaST S.A")}
                        }
                    }
                }
            });
        });
    }
}