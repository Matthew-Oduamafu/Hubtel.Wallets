using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Commands;
using Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Queries;
using Hubtel.Wallets.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Controllers.AccountSchemes
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class AccountSchemesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountSchemesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all the payment Schemes for a payment types",
            Description = "Requires admin privileges</br>. If successful returns a list of all the Payment Schemes avaliable for a particular Payment types",
            OperationId = "GetAllAccountSchemesByType",
            Tags = new[] { "AccountSchemes" }
        )]
        [SwaggerResponse(200, "Success! Your request was processed successfully. Here is the data you requested.", typeof(IReadOnlyList<AccountSchemeGetDto>))]
        [SwaggerResponse(401, "Unauthorized. Access to the requested resource requires authentication.", typeof(string))]
        [SwaggerResponse(403, "Restricted Access. Access denied, requires admin privileges.", typeof(string))]
        [SwaggerResponse(501, "Oops! Something went wrong on our end. We're working to fix the issue. Please try again later")]
        public async Task<IActionResult> GetAllAccountSchemesByType([FromQuery] int typeId)
        {
            var response = await _mediator.Send(new GetAllAccountSchemesByTypeRequest { TypeId = typeId });
            return Ok(response);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all the payment Schemes",
            Description = "Requires admin privileges</br>. If successful returns a list of all the Payment Schemes avaliable",
            OperationId = "GetAllAccountSchemes",
            Tags = new[] { "AccountSchemes" }
        )]
        [SwaggerResponse(200, "Success! Your request was processed successfully. Here is the data you requested.", typeof(IReadOnlyList<AccountSchemeGetDto>))]
        [SwaggerResponse(401, "Unauthorized. Access to the requested resource requires authentication.", typeof(string))]
        [SwaggerResponse(403, "Restricted Access. Access denied, requires admin privileges.", typeof(string))]
        [SwaggerResponse(501, "Oops! Something went wrong on our end. We're working to fix the issue. Please try again later")]
        public async Task<IActionResult> GetAllAccountSchemes()
        {
            var response = await _mediator.Send(new GetAllAccountSchemesRequest());
            return Ok(response);
        }

        [HttpGet("{id:int}", Name = "GetSingleAccountScheme")]
        [SwaggerOperation(
            Summary = "Get a single payment scheme",
            Description = "Requires admin privileges</br>. If successful returns a Payment scheme based on the ID passed",
            OperationId = "GetSingleAccountScheme",
            Tags = new[] { "AccountSchemes" }
        )]
        [SwaggerResponse(200, "Success! Your request was processed successfully. Here is the data you requested.", typeof(AccountSchemeGetDto))]
        [SwaggerResponse(204, "Your request was successful, but there is no content to return.")]
        [SwaggerResponse(401, "Unauthorized. Access to the requested resource requires authentication.", typeof(string))]
        [SwaggerResponse(403, "Restricted Access. Access denied, requires admin privileges.", typeof(string))]
        [SwaggerResponse(501, "Oops! Something went wrong on our end. We're working to fix the issue. Please try again later")]
        public async Task<IActionResult> GetSingleAccountScheme([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetSingleAccountSchemeRequest { Id = id });
            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all the payment types and corresponding payment Schemes",
            Description = "Requires admin privileges</br>. If successful returns a list of all the Payment Types and Payment Schemes avaliable",
            OperationId = "GetAllTypesAndSchemes",
            Tags = new[] { "AccountSchemes" }
        )]
        [SwaggerResponse(200, "Success! Your request was processed successfully. Here is the data you requested.", typeof(IReadOnlyList<VwTnsTypeAndSchemeGetDto>))]
        [SwaggerResponse(401, "Unauthorized. Access to the requested resource requires authentication.", typeof(string))]
        [SwaggerResponse(403, "Restricted Access. Access denied, requires admin privileges.", typeof(string))]
        [SwaggerResponse(501, "Oops! Something went wrong on our end. We're working to fix the issue. Please try again later")]
        public async Task<IActionResult> GetAllTypesAndSchemes()
        {
            var response = await _mediator.Send(new GetAllTypesAndSchemesRequest());
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Add a new payment scheme",
            Description = "Requires admin privileges</br>. If successful returns a response with <strong>Success=</strong><i>true</i>",
            OperationId = "CreateAccountScheme",
            Tags = new[] { "AccountSchemes" }
        )]
        [SwaggerResponse(201, "Created. Resource successfully created.</br>Please check the response for recently created Id.", typeof(BaseResponse))]
        [SwaggerResponse(400, "Bad Request. Oops! Your request is invalid or malformed. </br>Please review <strong>Schema</strong> for payload requirement and resend a valid request.", typeof(BaseResponse))]
        [SwaggerResponse(401, "Unauthorized. Access to the requested resource requires authentication.", typeof(string))]
        [SwaggerResponse(403, "Restricted Access. Access denied, requires admin privileges.", typeof(string))]
        [SwaggerResponse(501, "Oops! Something went wrong on our end. We're working to fix the issue. Please try again later")]
        public async Task<IActionResult> CreateAccountScheme([FromBody] CreateAccountSchemeDto dto)
        {
            var response = await _mediator.Send(new CreateAccountSchemeCommand { dto = dto });
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return CreatedAtRoute("GetSingleAccountScheme", new { id = response.Id }, response);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a payment scheme",
            Description = "Requires admin privileges</br>. If successful returns a response with <strong>Success=</strong><i>true</i>",
            OperationId = "UpdateAccountScheme",
            Tags = new[] { "AccountSchemes" }
        )]
        [SwaggerResponse(202, "Accepted. Your request has been accepted and is updated.", typeof(BaseResponse))]
        [SwaggerResponse(400, "Bad Request. Oops! Your request is invalid or malformed. </br>Please review <strong>Schema</strong> for payload requirement and resend a valid request.", typeof(BaseResponse))]
        [SwaggerResponse(404, "Not Found. Sorry, the requested resource could not be found. </br>Please review <strong>Schema</strong> for payload requirement and resend a valid request or try again with a different one.", typeof(BaseResponse))]
        [SwaggerResponse(401, "Unauthorized. Access to the requested resource requires authentication.", typeof(string))]
        [SwaggerResponse(403, "Restricted Access. Access denied, requires admin privileges.", typeof(string))]
        [SwaggerResponse(501, "Oops! Something went wrong on our end. We're working to fix the issue. Please try again later")]
        public async Task<IActionResult> UpdateAccountScheme([FromBody] UpdateAccountSchemeDto dto)
        {
            var response = await _mediator.Send(new UpdateAccountSchemeCommand { dto = dto });
            if ((int)response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else if (response.Success == false)
            {
                return BadRequest(response);
            }
            return AcceptedAtRoute("GetSingleAccountScheme", new { id = response.Id }, response);
        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Remove a payment scheme",
            Description = "Requires admin privileges</br>. If successful returns a response with <strong>Success=</strong><i>true</i>",
            OperationId = "DeleteAccountScheme",
            Tags = new[] { "AccountSchemes" }
        )]
        [SwaggerResponse(204, "No Content. Your request was successful, but there is no content to return.")]
        [SwaggerResponse(400, "Bad Request. Oops! Your request is invalid or malformed. </br>Please review Please review and resend a valid request.", typeof(BaseResponse))]
        [SwaggerResponse(404, "Not Found. Sorry, the requested resource could not be found. </br>Please review Please review and resend a valid request or try again with a different one.", typeof(BaseResponse))]
        [SwaggerResponse(401, "Unauthorized. Access to the requested resource requires authentication.", typeof(string))]
        [SwaggerResponse(403, "Restricted Access. Access denied, requires admin privileges.", typeof(string))]
        [SwaggerResponse(500, "Oops! Something went wrong on our end. We're working to fix the issue. Please try again later")]
        public async Task<IActionResult> DeleteAccountScheme([FromQuery] int Id)
        {
            var response = await _mediator.Send(new DeleteAccountSchemeCommand { Id = Id });
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