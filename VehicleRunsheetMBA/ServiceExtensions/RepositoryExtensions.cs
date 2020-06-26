using Microsoft.Extensions.DependencyInjection;
using VehicleRunsheetMBAProj.Data.Repositories;

namespace VehicleRunsheetMBAProj.ServiceExtensions
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// This is a custom <see cref="IServiceCollection"/> extension that encapsulates registration of
        /// <see cref="IRepository{T}"/> and <see cref="IUnitOfWork"/> services. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddTransient<ITripRepository, TripRepository>();
            services.AddTransient<IRunsheetRepository, RunsheetRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}