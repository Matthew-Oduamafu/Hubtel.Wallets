using Hubtel.Wallets.Api.Middleware.CustomHttpAttributes;
using Hubtel.Wallets.Application.DTOs.Types;
using Hubtel.Wallets.Application.Features.Types.Requests.Commands;
using Hubtel.Wallets.Application.Features.Types.Requests.Queries;
using Hubtel.Wallets.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Controllers.Types
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    [SwaggerResponse(401, "Unauthorized. Access to the requested resource requires authentication.", typeof(string))]
    [SwaggerResponse(403, "Restricted Access. Access denied, requires admin privileges.", typeof(string))]
    [SwaggerResponse(500, "Oops! Something went wrong on our end. We're working to fix the issue. Please try again later")]
    [ProducesResponseType(typeof(object), 429)]
    public class TypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Cached(600, 60)]
        [SwaggerOperation(
            Summary = "Get all the payment types",
            Description = "Requires admin privileges</br>. If successful returns a list of all Payment types",
            OperationId = "GetAllTypes",
            Tags = new[] { "Types" }
        )]
        [SwaggerResponse(200, "Success! Your request was processed successfully. Here is the data you requested.", typeof(IReadOnlyList<TypeDto>))]
        public async Task<IActionResult> GetAllTypes()
        {
            var response = await _mediator.Send(new GetAllTypesRequest());
            return Ok(response);
        }

        [HttpGet("{id:int}", Name = "GetSingleType")]
        [Cached(600, 60)]
        [SwaggerOperation(
            Summary = "Get a single payment types",
            Description = "Requires admin privileges</br>. If successful returns a Payment type based on the ID passed",
            OperationId = "GetSingleType",
            Tags = new[] { "Types" }
        )]
        [SwaggerResponse(200, "Success! Your request was processed successfully. Here is the data you requested.", typeof(TypeDto))]
        [SwaggerResponse(204, "Your request was successful, but there is no content to return.")]
        public async Task<IActionResult> GetSingleType([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetSingleTypesRequest { Id = id });
            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Add a new payment types",
            Description = "Requires admin privileges</br>. If successful returns a response with <strong>Success=</strong><i>true</i>",
            OperationId = "CreateType",
            Tags = new[] { "Types" }
        )]
        [SwaggerResponse(201, "Created. Resource successfully created.</br>Please check the response for recently created Id.", typeof(BaseResponse))]
        [SwaggerResponse(400, "Bad Request. Oops! Your request is invalid or malformed. </br>Please review <strong>Schema</strong> for payload requirement and resend a valid request.", typeof(BaseResponse))]
        public async Task<IActionResult> CreateType([FromBody] CreateTypeDto dto)
        {
            var response = await _mediator.Send(new CreateTypeCommand { dto = dto });
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return CreatedAtRoute("GetSingleType", new { id = response.Id }, response);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a payment types",
            Description = "Requires admin privileges</br>. If successful returns a response with <strong>Success=</strong><i>true</i>",
            OperationId = "UpdateType",
            Tags = new[] { "Types" }
        )]
        [SwaggerResponse(202, "Accepted. Your request has been accepted and is been updated.", typeof(BaseResponse))]
        [SwaggerResponse(400, "Bad Request. Oops! Your request is invalid or malformed. </br>Please review <strong>Schema</strong> for payload requirement and resend a valid request.", typeof(BaseResponse))]
        [SwaggerResponse(404, "Not Found. Sorry, the requested resource could not be found. </br>Please review <strong>Schema</strong> for payload requirement and resend a valid request or try again with a different one.", typeof(BaseResponse))]
        public async Task<IActionResult> UpdateType([FromBody] UpdateTypeDto dto)
        {
            var response = await _mediator.Send(new UpdateTypeCommand { dto = dto });
            if ((int)response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else if (response.Success == false)
            {
                return BadRequest(response);
            }
            return AcceptedAtRoute("GetSingleType", new { id = response.Id }, response);
        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Remove a payment types",
            Description = "Requires admin privileges</br>. If successful returns a response with <strong>Success=</strong><i>true</i>",
            OperationId = "DeleteType",
            Tags = new[] { "Types" }
        )]
        [SwaggerResponse(204, "No Content. Your request was successful, but there is no content to return.")]
        [SwaggerResponse(400, "Bad Request. Oops! Your request is invalid or malformed. </br>Please review Please review and resend a valid request.", typeof(BaseResponse))]
        [SwaggerResponse(404, "Not Found. Sorry, the requested resource could not be found. </br>Please review Please review and resend a valid request or try again with a different one.", typeof(BaseResponse))]
        public async Task<IActionResult> DeleteType([FromQuery] int Id)
        {
            var response = await _mediator.Send(new DeleteTypeCommand { Id = Id });
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