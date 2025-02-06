using PetShop.Application.Services;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Base;
using PetShop.Core.Base.Interfaces;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Facade.Interfaces;
using PetShop.Facade.Services;

namespace PetShop.Api.ApiConfig
{
    public static class PetShopServiceLocator
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IBrasilApiHttpService, BrasilApiHttpService>();
            services.AddScoped<ICompaniesService, CompanyService>();
            services.AddScoped<IUsersService, UsersService>();
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