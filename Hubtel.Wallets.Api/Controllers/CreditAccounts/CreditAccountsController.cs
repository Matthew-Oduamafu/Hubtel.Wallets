using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Command;
using Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Queries;
using Hubtel.Wallets.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Controllers.CreditAccounts
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CreditAccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreditAccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<AllCreditAccountsForUserDto>), 200)]
        public async Task<IActionResult> GetAllCreditAccountsForASingleUser([FromQuery] string userId, [FromQuery] string? email = null)
        {
            var response = await _mediator.Send(new GetAllCreditAccountsForASingleUserRequest { Email = email ?? string.Empty, UserId = userId });

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<AllCreditAccountsDto>), 200)]
        public async Task<IActionResult> GetAllCreditAccounts()
        {
            var response = await _mediator.Send(new GetAllCreditAccountsRequest());

            return Ok(response);
        }

        [HttpGet("{id:int}", Name = "GetSingleCreditAccountsForAUser")]
        [ProducesResponseType(typeof(AllCreditAccountsForUserDto), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetSingleCreditAccountsForAUser([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetSingleCreditAccountsForAUserRequest { Id = id });
            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), 201)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> CreateCreditAccount([FromBody] CreateCreditAccountDto dto)
        {
            var response = await _mediator.Send(new CreateCreditAccountCommand { dto = dto });
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return CreatedAtRoute("GetSingleCreditAccountsForAUser", new { id = response.Id }, response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> UpdateCreditAccount([FromBody] UpdateCreditAccountDto dto)
        {
            var response = await _mediator.Send(new UpdateCreditAccountCommand { dto = dto });
            if ((int)response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else if (response.Success == false)
            {
                return BadRequest(response);
            }
            return AcceptedAtRoute("GetSingleCreditAccountsForAUser", new { id = response.Id }, response);
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> DeleteCreditAccount([FromQuery] int Id)
        {
            var response = await _mediator.Send(new DeleteCreditAccountCommand { Id = Id });
            if ((int)response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else if (response.Success == false)
            {
                return BadRequest(response);
            }

            return NoContent();
        }
    }
}