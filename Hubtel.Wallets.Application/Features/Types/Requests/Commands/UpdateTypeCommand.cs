using Hubtel.Wallets.Application.DTOs.Types;
using Hubtel.Wallets.Application.Models;
using MediatR;

namespace Hubtel.Wallets.Application.Features.Types.Requests.Commands
{
    public class UpdateTypeCommand : IRequest<BaseResponse>
    {
        public UpdateTypeDto dto { get; set; }
    }
}