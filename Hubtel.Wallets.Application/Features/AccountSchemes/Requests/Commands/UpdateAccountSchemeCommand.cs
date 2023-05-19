using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using Hubtel.Wallets.Application.Models;
using MediatR;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Commands
{
    public class UpdateAccountSchemeCommand : IRequest<BaseResponse>
    {
        public UpdateAccountSchemeDto dto { get; set; }
    }
}