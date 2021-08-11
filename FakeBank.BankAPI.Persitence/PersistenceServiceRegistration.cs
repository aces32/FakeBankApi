using FakeBank.BankAPI.Application.Contracts.Persistence;
using FakeBank.BankAPI.Persitence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Persitence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FakeBankDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FakeBankConnectionString")));

            services.AddDbContext<FakeBankDbContext>(options =>
            {
                options.UseInMemoryDatabase("FakeBankDbContextInMemoryTest");
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();

            return services;
        }
    }
}
