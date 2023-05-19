using Hubtel.Wallets.Application.Features.Accounts.Requests;
using Hubtel.Wallets.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Controllers.Accounts
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            return Ok(await _mediator.Send(new LoginCommand { AuthRequest = request }));
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            return Ok(await _mediator.Send(new RegisterCommand { RegistrationRequest = request }));
        }
    }
}