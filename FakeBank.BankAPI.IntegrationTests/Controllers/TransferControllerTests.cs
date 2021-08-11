using FakeBank.BankAPI.Api;
using FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers;
using FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory;
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
    public class TransferControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TransferControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetTransactionHistoryReturnsSuccessResult()
        {
            var client = await _factory.GetAnonymousClient();
            var response = await client.GetAsync("/getTransactionHistory?accountNumber=2050000003&paymentStartDate=2021-08-08%2023%3A03%3A22.6072198%20%2B00%3A00&paymentEndDate=2021-08-10%2000%3A47%3A33.1267300%20%2B00%3A00&page=1&size=2");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GetTransferHistoryQueryResponse>(responseString);

            Assert.IsType<GetTransferHistoryQueryResponse>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task IntraBankTransferReturnsSuccessResult()
        {

            var client = await _factory.GetAnonymousClient();
            var payload = new IntraBankTransfersCommand
            {
                AccountNumberToDebit = "2050000003",
                BeneficiaryAccountName = "Tola Starr",
                BeneficiaryAccountNumber = "2050000001",
                PaymentReference = "TestingIntegrationTestNew",
                TransactionAmount = 2,
                TransactionRemarks = "Test IntraBankTransferReturnsSuccessResult Success"
            };

            var response = await client.PostAsync("/api/Transfer", new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IntraBankTransfersCommandResponse>(responseString);

            Assert.IsType<IntraBankTransfersCommandResponse>(result);
            Assert.True(result.Success);
        }
    }
}
