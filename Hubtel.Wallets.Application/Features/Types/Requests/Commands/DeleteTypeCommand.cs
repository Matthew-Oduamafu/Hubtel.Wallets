using Hubtel.Wallets.Application.Models;
using MediatR;

namespace Hubtel.Wallets.Application.Features.Types.Requests.Commands
{
    public class DeleteTypeCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}