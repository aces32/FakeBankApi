using FakeBank.BankAPI.Application.Features.Transfers.Command.IntraBankTransfers;
using FakeBank.BankAPI.Application.Features.Transfers.Queries.GetTransferHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FakeBank.BankAPI.Api.Controllers
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieve transfer history for a given account .
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="paymentStartDate"></param>
        /// <param name="paymentEndDate"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("/getTransactionHistory", Name = "GetTransactionHistory")]
        public async Task<ActionResult<GetTransferHistoryQueryResponse>> Get(string accountNumber, DateTimeOffset paymentStartDate,
            DateTimeOffset paymentEndDate, int page, int size)
        {
            var getTransferHistoryQuery = new GetTransferHistoryQuery
            {
                AccountNumber = accountNumber,
                PaymentStartDate = paymentStartDate,
                PaymentEndDate = paymentEndDate,
                Size = size,
                Page = page
            };

            var response = await _mediator.Send(getTransferHistoryQuery);
            return response.ValidationErrors != null
                ? BadRequest(response)
                : Ok(response);
        }

        /// <summary>
        /// Transfers amounts between any two accounts, including those owned by different customers.
        /// </summary>
        /// <param name="intraBankTransfersCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "IntraBankTransfer")]
        public async Task<ActionResult<IntraBankTransfersCommandResponse>> Create(IntraBankTransfersCommand intraBankTransfersCommand)
        {
            var response = await _mediator.Send(intraBankTransfersCommand);
            return response.ValidationErrors != null
                ? BadRequest(response)
                : Ok(response);
        }

    }
}
