using Microsoft.EntityFrameworkCore;
using PetShop.Data.Context;

namespace PetShop.Api.ApiConfig
{
    public static class DbContextConfig
    {
        public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
        {

            builder.Services.AddDbContext<PetShopContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));
            return builder;
        }
    }
}
