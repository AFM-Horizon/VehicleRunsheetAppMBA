using Microsoft.Extensions.DependencyInjection;
using VehicleRunsheetMBAProj.Data.Repositories;

namespace VehicleRunsheetMBAProj.ServiceExtensions
{
    public static class RepositoryExtensions
    {
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