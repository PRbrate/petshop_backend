using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace PetShop.Api.ApiConfig
{
    public static class SwaggerConfig
    {
        public static WebApplicationBuilder AddSwaggerConfig(this WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.MapType<DateTime>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date", // Define apenas a data no Swagger
                    Example = new Microsoft.OpenApi.Any.OpenApiString(DateTime.Now.ToString())
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                {
                                    {
                                        new OpenApiSecurityScheme
                                        {
                                            Reference = new OpenApiReference
                                            {
                                                Type = ReferenceType.SecurityScheme,
                                                Id = "Bearer"
                                            }
                                        },
                                        new string[] {}
                                    }
                                });
            });


            return builder;
        }
    }
}
