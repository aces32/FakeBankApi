using FakeBank.BankAPI.Api;
using FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount;
using FakeBank.BankAPI.Application.Features.Account.Queries.GetAccountBalance;
using FakeBank.BankAPI.IntegrationTests.Base;
using FakeBank.BankAPI.IntegrationTests.Base.Token.Token;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FakeBank.BankAPI.IntegrationTests.Controllers
{
    public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AccountControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAccountBalanceReturnsSuccessResult()
        {
            var client = await _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/Account/2050000001");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GetAccountBalanceQueryResponse>(responseString);

            Assert.IsType<GetAccountBalanceQueryResponse>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateAccountReturnsSuccessResult()
        {

            var client = await _factory.GetAnonymousClient();

            var payload = new CreateAccountCommand {
                CustomerID = 1,
                AccountType = "Savings",
                InitialDepAmount = 500
            };

            var response = await client.PostAsync("/api/Account", new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateAccountCommandResponse>(responseString);

            Assert.IsType<CreateAccountCommandResponse>(result);
            Assert.True(result.Success);
        }
    }
}
 