using Hubtel.Wallets.Api.Middleware.CustomHttpAttributes;
using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Command;
using Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Queries;
using Hubtel.Wallets.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Controllers.CreditAccounts
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    [SwaggerResponse(401, "Unauthorized. Access to the requested resource requires authentication.", typeof(string))]
    [SwaggerResponse(500, "Oops! Something went wrong on our end. We're working to fix the issue. Please try again later")]
    [ProducesResponseType(typeof(object), 429)]
    public class CreditAccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreditAccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Cached(600, 60)]
        [SwaggerOperation(
            Summary = "Get all the Wallets for a single user",
            Description = "If successful, returns a list of all the Wallets belonging to a user",
            OperationId = "GetAllCreditAccountsForASingleUser",
            Tags = new[] { "CreditAccounts" }
        )]
        [SwaggerResponse(200, "Success! Your request was processed successfully. Here is the data you requested.", typeof(IReadOnlyList<AllCreditAccountsForUserDto>))]
        public async Task<IActionResult> GetAllCreditAccountsForASingleUser([FromQuery] string userId, [FromQuery] string? email = null)
        {
            var response = await _mediator.Send(new GetAllCreditAccountsForASingleUserRequest { Email = email ?? string.Empty, UserId = userId });

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [SwaggerOperation(
            Summary = "Get all the Wallets available",
            Description = "Requires admin privileges</br>.If successful, returns a list of all the Wallets available",
            OperationId = "GetAllCreditAccounts",
            Tags = new[] { "CreditAccounts" }
        )]
        [SwaggerResponse(200, "Success! Your request was processed successfully. Here is the data you requested.", typeof(IReadOnlyList<AllCreditAccountsDto>))]
        [SwaggerResponse(403, "Restricted Access. Access denied, requires admin privileges.", typeof(string))]
        public async Task<IActionResult> GetAllCreditAccounts()
        {
            var response = await _mediator.Send(new GetAllCreditAccountsRequest());

            return Ok(response);
        }

        [HttpGet("{id:int}", Name = "GetSingleCreditAccountsForAUser")]
        [SwaggerOperation(
            Summary = "Get a single Wallet account",
            Description = "If successful returns a Wallet based on the ID passed",
            OperationId = "GetSingleCreditAccountsForAUser",
            Tags = new[] { "CreditAccounts" }
        )]
        [SwaggerResponse(200, "Success! Your request was processed successfully. Here is the data you requested.", typeof(AllCreditAccountsForUserDto))]
        [SwaggerResponse(204, "Your request was successful, but there is no content to return.")]
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
        [SwaggerOperation(
            Summary = "Add a new Wallet",
            Description = "Create a new Wallet for a user.</br>Make sure to pass the UserId, Id of payment type, and the corresponding payment scheme to the payload</br>If successful returns a response with <strong>Success=</strong><i>true</i>",
            OperationId = "CreateCreditAccount",
            Tags = new[] { "CreditAccounts" }
        )]
        [SwaggerResponse(201, "Created. Resource successfully created.</br>Please check the response for recently created Id.", typeof(BaseResponse))]
        [SwaggerResponse(400, "Bad Request. Oops! Your request is invalid or malformed. </br>Please review <strong>Schema</strong> for payload requirement and resend a valid request.", typeof(BaseResponse))]
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
        [SwaggerOperation(
            Summary = "Update a Wallet",
            Description = "If successful returns a response with <strong>Success=</strong><i>true</i>",
            OperationId = "UpdateCreditAccount",
            Tags = new[] { "CreditAccounts" }
        )]
        [SwaggerResponse(202, "Accepted. Your request has been accepted and is updated.", typeof(BaseResponse))]
        [SwaggerResponse(400, "Bad Request. Oops! Your request is invalid or malformed. </br>Please review <strong>Schema</strong> for payload requirement and resend a valid request.", typeof(BaseResponse))]
        [SwaggerResponse(404, "Not Found. Sorry, the requested resource could not be found. </br>Please review <strong>Schema</strong> for payload requirement and resend a valid request or try again with a different one.", typeof(BaseResponse))]
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
        [SwaggerOperation(
            Summary = "Remove a payment scheme",
            Description = "If successful returns a response with <strong>Success=</strong><i>true</i>",
            OperationId = "DeleteCreditAccount",
            Tags = new[] { "CreditAccounts" }
        )]
        [SwaggerResponse(204, "No Content. Your request was successful, but there is no content to return.")]
        [SwaggerResponse(400, "Bad Request. Oops! Your request is invalid or malformed. </br>Please review Please review and resend a valid request.", typeof(BaseResponse))]
        [SwaggerResponse(404, "Not Found. Sorry, the requested resource could not be found. </br>Please review Please review and resend a valid request or try again with a different one.", typeof(BaseResponse))]
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