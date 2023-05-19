using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Commands;
using Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Queries;
using Hubtel.Wallets.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Controllers.AccountSchemes
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountSchemesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountSchemesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<AccountSchemeGetDto>), 200)]
        public async Task<IActionResult> GetAllAccountSchemesByType([FromQuery] int typeId)
        {
            var response = await _mediator.Send(new GetAllAccountSchemesByTypeRequest { TypeId = typeId });
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<AccountSchemeGetDto>), 200)]
        public async Task<IActionResult> GetAllAccountSchemes()
        {
            var response = await _mediator.Send(new GetAllAccountSchemesRequest());
            return Ok(response);
        }

        [HttpGet("{id:int}", Name = "GetSingleAccountScheme")]
        [ProducesResponseType(typeof(AccountSchemeGetDto), 200)]
        [ProducesResponseType(204)]
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
        [ProducesResponseType(typeof(IReadOnlyList<VwTnsTypeAndSchemeGetDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllTypesAndSchemes()
        {
            var response = await _mediator.Send(new GetAllTypesAndSchemesRequest());
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), 201)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
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
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
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
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
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