using DepartmentOfVeterans.Application.Contracts.Persistence;
using DepartmentOfVeterans.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentOfVeterans.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DepartmentOfVeteransDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DepartmentOfVeteransConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
