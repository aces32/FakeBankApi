using FakeBank.BankAPI.Application.Features.Account.Command.CreateAccount;
using FakeBank.BankAPI.Application.Features.Account.Queries.GetAccountBalance;
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
    public class AccountController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves balances and account type for a given account
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [HttpGet("{accountNumber}", Name = "GetAccountBalance")]
        public async Task<ActionResult<GetAccountBalanceQueryResponse>> GetAccountBalance(string accountNumber)
        {
            var getAccountBalanceQuery = new GetAccountBalanceQuery() { AccountNumber = accountNumber };
            var response = await _mediator.Send(getAccountBalanceQuery);
            return response.ValidationErrors != null
                ? BadRequest(response) 
                : Ok(await _mediator.Send(getAccountBalanceQuery));
        }

        /// <summary>
        /// Creates a new bank account for a customer, with an initial deposit amount. A single customer may have multiple bank accounts
        /// </summary>
        /// <param name="createAccountCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateAccount")]
        public async Task<ActionResult<CreateAccountCommandResponse>> Create([FromBody] CreateAccountCommand createAccountCommand)
        {
            var response = await _mediator.Send(createAccountCommand);
            return response.ValidationErrors != null
                ? BadRequest(response)
                : CreatedAtRoute("GetAccountBalance", new { response.Data.AccountNumber }, response);
        }

    }
}
