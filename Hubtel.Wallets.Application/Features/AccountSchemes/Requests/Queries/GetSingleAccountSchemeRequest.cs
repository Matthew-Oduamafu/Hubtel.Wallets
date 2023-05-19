using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using MediatR;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Queries
{
    public class GetSingleAccountSchemeRequest : IRequest<AccountSchemeGetDto>
    {
        public int Id { get; set; }
    }
}