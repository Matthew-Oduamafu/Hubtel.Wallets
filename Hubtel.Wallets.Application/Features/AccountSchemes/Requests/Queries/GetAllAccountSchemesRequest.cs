using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using MediatR;
using System.Collections.Generic;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Queries
{
    public class GetAllAccountSchemesRequest : IRequest<IReadOnlyList<AccountSchemeGetDto>>
    {
    }
}