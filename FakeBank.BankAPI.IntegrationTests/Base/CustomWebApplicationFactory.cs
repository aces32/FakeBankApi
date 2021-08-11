using FakeBank.BankAPI.Persitence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http.Headers;
using FakeBank.BankAPI.IntegrationTests.Base.Token.Token;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                                        d => d.ServiceType ==
                                        typeof(DbContextOptions<FakeBankDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<FakeBankDbContext>(options =>
                {
                    options.UseInMemoryDatabase("FakeBankDbContextInMemoryTest");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<FakeBankDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                    }
                };
            });
        }

        public async Task<HttpClient> GetAnonymousClient()
        {
            var client = CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await Auth0TokenGenerator.GenerateAuth0Token()).access_token);
            return client;
        }
    }
}
