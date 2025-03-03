using Appointment.Data.Repositories;
using Appointment.Data.Repositories.Interfaces;
using PetShop.Application.Services;
using PetShop.Application.Services.Interfaces;
using PetShop.Application.Services.OtherServices;
using PetShop.Core;
using PetShop.Core.Audit;
using PetShop.Core.Base;
using PetShop.Core.Base.Interfaces;
using PetShop.Core.Extensions.Security;
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
            services.AddScoped<IAuditHelper, AuditHelper>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuditService, AuditService>();

            services.AddScoped<IBrasilApiHttpService, BrasilApiHttpService>();
            services.AddScoped<ICompaniesService, CompanyService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IPetsService, PetsService>();
            services.AddSingleton<EmailService>();
            services.AddMemoryCache();
            services.AddScoped<IServicesService, ServicesServices>();
            services.AddSingleton<MemoryCacheService>();
            #endregion

            #region Repositories

            services.AddScoped<IAuditRepository, AuditRepository>();

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            services.AddScoped<ICompaniesRepository, CompaniesRepository>();
            services.AddScoped<IPetsRepository, PetsRepository>();
            services.AddScoped<IServiceGroupRepository, ServiceGroupRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            #endregion

        }
    }
}