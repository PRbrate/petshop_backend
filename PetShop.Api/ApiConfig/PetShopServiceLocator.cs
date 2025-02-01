using PetShop.Core.Base;
using PetShop.Core.Base.Interfaces;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.Interfaces;

namespace PetShop.Api.ApiConfig
{
    public static class PetShopServiceLocator
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Services

            #endregion

            #region Repositories
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            services.AddScoped<ICompaniesRepository, CompaniesRepository>();
            services.AddScoped<IPetsRepository, PetsRepository>();
            services.AddScoped<IServiceGroupRepository, ServiceGroupRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            #endregion

        }
    }
}