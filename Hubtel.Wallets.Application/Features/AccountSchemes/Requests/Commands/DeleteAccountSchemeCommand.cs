using Hubtel.Wallets.Application.Models;
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Commands
{
    public class DeleteAccountSchemeCommand : IRequest<BaseResponse>
    {
        [Required]
        [Range(1, int.MaxValue)]
        [DefaultValue(10)]
        public int Id { get; set; }
    }
}