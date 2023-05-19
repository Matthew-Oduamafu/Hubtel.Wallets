using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using MediatR;
using System.Collections.Generic;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Queries
{
    public class GetAllAccountSchemesByTypeRequest : IRequest<IReadOnlyList<AccountSchemeGetDto>>
    {
        public int TypeId { get; set; }
    }
}