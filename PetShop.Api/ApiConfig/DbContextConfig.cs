using Microsoft.EntityFrameworkCore;
using PetShop.Data.Context;
using System.Collections;

namespace PetShop.Api.ApiConfig
{
    public static class DbContextConfig
    {
        public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
        {
            var connectionStr = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            builder.Services.AddDbContext<PetShopContext>(opt => opt.UseNpgsql((connectionStr)));
            return builder;
        }
    }
}
