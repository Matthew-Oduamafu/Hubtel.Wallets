using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using Hubtel.Wallets.Application.Models;
using MediatR;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Commands
{
    public class CreateAccountSchemeCommand : IRequest<BaseResponse>
    {
        public CreateAccountSchemeDto dto { get; set; }
    }
}