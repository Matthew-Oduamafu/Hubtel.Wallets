using Hubtel.Wallets.Application.Features.Accounts.Requests;
using Hubtel.Wallets.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Controllers.Accounts
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        [ProducesResponseType(typeof(AuthBadResponse), 404)]
        [ProducesResponseType(typeof(AuthBadResponse), 400)]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            var response = await _mediator.Send(new LoginCommand { AuthRequest = request });
            if (response is AuthBadResponse)
            {
                if (((AuthBadResponse)response).StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return BadRequest(response);
                }
                else
                {
                    return NotFound(response);
                }
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegistrationResponse), 200)]
        //[ProducesResponseType(typeof(AuthBadResponse), 404)]
        [ProducesResponseType(typeof(RegistrationBadResponse), 400)]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            var response = await _mediator.Send(new RegisterCommand { RegistrationRequest = request });
            if (response is RegistrationBadResponse)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}